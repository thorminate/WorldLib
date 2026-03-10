using System;
using System.Collections.Generic;

namespace WorldLib.Utils;

/// <summary>
///     Represents a live, transmutable view over an underlying collection of serialized items.
///     Each element is transformed via a codec between the storage representation
///     (<typeparamref name="TStorage" />) and the exposed runtime representation
///     (<typeparamref name="TExposed" />).
/// </summary>
/// <remarks>
///     <para>
///         This type acts as a projection layer between an underlying storage container
///         and a higher-level runtime representation.
///     </para>
///     <para>
///         Unlike typical adapter collections, this list does not maintain its own backing storage.
///         Instead, all operations delegate directly to externally supplied accessors.
///         This ensures that the collection always reflects the current state of the
///         underlying storage, even if modified externally (for example by other mods).
///     </para>
///     <para>
///         Each element is encoded and decoded on demand using the supplied codec delegates.
///         No caching is performed by this class.
///     </para>
///     <para>
///         Implementations using mutable list-backed storage typically achieve the following
///         complexity characteristics:
///         <list type="bullet">
///             <item>
///                 <description>Index access: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Index assignment: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Add: <c>O(1)</c></description>
///             </item>
///             <item>
///                 <description>Insert / Remove: <c>O(n)</c></description>
///             </item>
///         </list>
///     </para>
/// </remarks>
/// <typeparam name="TStorage">
///     The serialized storage representation used by the underlying data source.
///     This is typically a primitive or identifier type (for example <see cref="string" />).
/// </typeparam>
/// <typeparam name="TExposed">
///     The runtime representation exposed by the list interface.
///     This is commonly a higher-level object resolved from the storage representation.
/// </typeparam>
public sealed class TransmutableList<TStorage, TExposed>
{
    private readonly Action _clear;
    private readonly Func<int> _count;

    private readonly Func<TStorage, TExposed> _decode;
    private readonly Func<TExposed, TStorage> _encode;
    private readonly Func<int, TStorage> _get;
    private readonly Action<int, TStorage> _insert;
    private readonly Action<int> _remove;
    private readonly Action<int, TStorage> _set;

    /// <inheritdoc cref="TransmutableList{TStorage,TExposed}" />
    /// <param name="count">
    ///     A delegate returning the number of elements currently present in the
    ///     underlying storage collection. Cannot be <c>null</c>.
    /// </param>
    /// <param name="get">
    ///     A delegate that retrieves a serialized element at the specified index
    ///     from the underlying storage. Cannot be <c>null</c>.
    /// </param>
    /// <param name="set">
    ///     A delegate that replaces the serialized element at the specified index
    ///     in the underlying storage. Cannot be <c>null</c>.
    /// </param>
    /// <param name="insert">
    ///     A delegate that inserts a serialized element into the underlying storage
    ///     at the specified index. Cannot be <c>null</c>.
    /// </param>
    /// <param name="remove">
    ///     A delegate that removes the serialized element at the specified index
    ///     from the underlying storage. Cannot be <c>null</c>.
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
    public TransmutableList(
        Func<int> count,
        Func<int, TStorage> get,
        Action<int, TStorage> set,
        Action<int, TStorage> insert,
        Action<int> remove,
        Action clear,
        Func<TStorage, TExposed> decode,
        Func<TExposed, TStorage> encode)
    {
        _count = count ?? throw new ArgumentNullException(nameof(count));
        _get = get ?? throw new ArgumentNullException(nameof(get));
        _set = set ?? throw new ArgumentNullException(nameof(set));
        _insert = insert ?? throw new ArgumentNullException(nameof(insert));
        _remove = remove ?? throw new ArgumentNullException(nameof(remove));
        _clear = clear ?? throw new ArgumentNullException(nameof(clear));
        _decode = decode ?? throw new ArgumentNullException(nameof(decode));
        _encode = encode ?? throw new ArgumentNullException(nameof(encode));
    }

    /// <inheritdoc cref="ICollection{T}.Count" />
    public int Count => _count();

    /// <inheritdoc cref="IList{T}.this[int]" />
    public TExposed this[int index]
    {
        get => _decode(_get(index));
        set => _set(index, _encode(value));
    }

    /// <inheritdoc cref="ICollection{T}.Add" />
    public void Add(TExposed item)
    {
        _insert(_count(), _encode(item));
    }

    /// <inheritdoc cref="ICollection{T}.Clear" />
    public void Clear()
    {
        _clear();
    }

    /// <inheritdoc cref="ICollection{T}.Contains" />
    public bool Contains(TExposed item)
    {
        return IndexOf(item) >= 0;
    }

    /// <inheritdoc cref="IList{T}.IndexOf" />
    public int IndexOf(TExposed item)
    {
        var encoded = _encode(item);

        for (int i = 0; i < _count(); i++)
            if (EqualityComparer<TStorage>.Default.Equals(_get(i), encoded))
                return i;

        return -1;
    }

    /// <inheritdoc cref="IList{T}.Insert" />
    public void Insert(int index, TExposed item)
    {
        _insert(index, _encode(item));
    }

    /// <inheritdoc cref="ICollection{T}.Remove" />
    public bool Remove(TExposed item)
    {
        int idx = IndexOf(item);

        if (idx < 0)
            return false;

        _remove(idx);
        return true;
    }

    /// <inheritdoc cref="IList{T}.RemoveAt" />
    public void RemoveAt(int index)
    {
        _remove(index);
    }

    /// <inheritdoc cref="ICollection{T}.CopyTo" />
    public void CopyTo(TExposed[] array, int arrayIndex)
    {
        for (int i = 0; i < _count(); i++)
            array[arrayIndex + i] = _decode(_get(i));
    }
}