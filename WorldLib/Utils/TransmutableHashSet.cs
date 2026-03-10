using System;
using System.Collections.Generic;

namespace WorldLib.Utils;

/// <summary>
///     Represents a live, transmutable view over an underlying unordered collection of serialized items
///     with set semantics — no ordering, no duplicates.
///     Each element is transformed via a codec between the storage representation
///     (<typeparamref name="TStorage" />) and the exposed runtime representation
///     (<typeparamref name="TExposed" />).
/// </summary>
/// <remarks>
///     <para>
///         This type acts as a projection layer between an underlying set-backed storage container
///         and a higher-level runtime representation.
///     </para>
///     <para>
///         Unlike typical adapter collections, this set does not maintain its own backing storage.
///         Instead, all operations delegate directly to externally supplied accessors.
///         This ensures that the collection always reflects the current state of the
///         underlying storage, even if modified externally (for example by other mods).
///     </para>
///     <para>
///         Each element is encoded and decoded on demand using the supplied codec delegates.
///         No caching is performed by this class.
///     </para>
///     <para>
///         Implementations using hash-set-backed storage typically achieve the following
///         complexity characteristics:
///         <list type="bullet">
///             <item>
///                 <description>Add: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Remove: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Contains: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Clear: <c>O(n)</c></description>
///             </item>
///         </list>
///     </para>
/// </remarks>
/// <typeparam name="TStorage">
///     The serialized storage representation used by the underlying data source.
///     This is typically a primitive or identifier type (for example <see cref="string" />).
/// </typeparam>
/// <typeparam name="TExposed">
///     The runtime representation exposed by the set interface.
///     This is commonly a higher-level object resolved from the storage representation.
/// </typeparam>
public sealed class TransmutableHashSet<TStorage, TExposed>
{
    private readonly Action<TStorage> _add;
    private readonly Action _clear;
    private readonly Func<int> _count;
    private readonly Func<TStorage, TExposed> _decode;
    private readonly Func<TExposed, TStorage> _encode;
    private readonly Func<IEnumerable<TStorage>> _getAll;
    private readonly Func<TStorage, bool> _remove;

    /// <inheritdoc cref="TransmutableHashSet{TStorage,TExposed}" />
    /// <param name="count">
    ///     A delegate returning the number of elements currently present in the
    ///     underlying storage collection. Cannot be <c>null</c>.
    /// </param>
    /// <param name="getAll">
    ///     A delegate returning an enumerable over all serialized elements currently
    ///     present in the underlying storage. Used internally for iteration and containment
    ///     checks. Cannot be <c>null</c>.
    /// </param>
    /// <param name="add">
    ///     A delegate that inserts a serialized element into the underlying storage.
    ///     Cannot be <c>null</c>.
    /// </param>
    /// <param name="remove">
    ///     A delegate that removes a serialized element from the underlying storage,
    ///     returning <c>true</c> if the element was present and removed. Cannot be <c>null</c>.
    /// </param>
    /// <param name="clear">
    ///     A delegate that removes all elements from the underlying storage.
    ///     Cannot be <c>null</c>.
    /// </param>
    /// <param name="decode">
    ///     A delegate that converts a storage element into its exposed runtime form.
    ///     Cannot be <c>null</c>.
    /// </param>
    /// <param name="encode">
    ///     A delegate that converts an exposed element back into its storage form.
    ///     Cannot be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if any of the supplied delegates are <c>null</c>.
    /// </exception>
    public TransmutableHashSet(
        Func<int> count,
        Func<IEnumerable<TStorage>> getAll,
        Action<TStorage> add,
        Func<TStorage, bool> remove,
        Action clear,
        Func<TStorage, TExposed> decode,
        Func<TExposed, TStorage> encode)
    {
        _count = count ?? throw new ArgumentNullException(nameof(count));
        _getAll = getAll ?? throw new ArgumentNullException(nameof(getAll));
        _add = add ?? throw new ArgumentNullException(nameof(add));
        _remove = remove ?? throw new ArgumentNullException(nameof(remove));
        _clear = clear ?? throw new ArgumentNullException(nameof(clear));
        _decode = decode ?? throw new ArgumentNullException(nameof(decode));
        _encode = encode ?? throw new ArgumentNullException(nameof(encode));
    }

    /// <inheritdoc cref="ICollection{T}.Count" />
    public int Count => _count();

    /// <inheritdoc cref="ICollection{T}.Add" />
    public void Add(TExposed item)
    {
        _add(_encode(item));
    }

    /// <inheritdoc cref="ICollection{T}.Remove" />
    public bool Remove(TExposed item)
    {
        return _remove(_encode(item));
    }

    /// <inheritdoc cref="ICollection{T}.Clear" />
    public void Clear()
    {
        _clear();
    }

    /// <inheritdoc cref="ICollection{T}.Contains" />
    public bool Contains(TExposed item)
    {
        var encoded = _encode(item);
        foreach (var stored in _getAll())
            if (EqualityComparer<TStorage>.Default.Equals(stored, encoded))
                return true;
        return false;
    }

    /// <inheritdoc cref="ICollection{T}.CopyTo" />
    public void CopyTo(TExposed[] array, int arrayIndex)
    {
        int i = arrayIndex;
        foreach (var item in _getAll())
            array[i++] = _decode(item);
    }
}