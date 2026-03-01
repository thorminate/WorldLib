using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WorldLib.Utils;

/// <summary>
///     Represents a live, transmutable view over an underlying array of serialized items.
///     Each element is transformed via a codec between the storage representation
///     (<typeparamref name="TStorage" />) and the exposed runtime representation
///     (<typeparamref name="TExposed" />).
/// </summary>
/// <remarks>
///     All operations read and write directly to the underlying storage array.
///     This ensures that external modifications (e.g., other mods) are always reflected.
/// </remarks>
public sealed class TransmutableList<TStorage, TExposed> : IList<TExposed>
{
    private readonly Func<TStorage, TExposed> _decode;
    private readonly Func<TExposed, TStorage> _encode;
    private readonly Func<TStorage[]> _getter;
    private readonly Action<TStorage[]> _setter;

    /// <inheritdoc cref="TransmutableList{TStorage,TExposed}" />
    /// <param name="getter">
    ///     A delegate returning the underlying array of serialized items. Cannot be <c>null</c>.
    /// </param>
    /// <param name="setter">
    ///     A delegate that writes a new array back to the underlying storage. Cannot be <c>null</c>.
    /// </param>
    /// <param name="decode">
    ///     A delegate that converts a storage element into its exposed runtime form. Cannot be <c>null</c>.
    /// </param>
    /// <param name="encode">
    ///     A delegate that converts an exposed element back into its storage form. Cannot be <c>null</c>.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if any of the delegates are <c>null</c>.
    /// </exception>
    public TransmutableList(
        Func<TStorage[]> getter,
        Action<TStorage[]> setter,
        Func<TStorage, TExposed> decode,
        Func<TExposed, TStorage> encode)
    {
        _getter = getter;
        _setter = setter;
        _decode = decode;
        _encode = encode;
    }

    private TStorage[] Raw => _getter() ?? [];

    /// <inheritdoc />
    public int Count => Raw.Length;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public TExposed this[int index]
    {
        get => _decode(Raw[index]);
        set
        {
            TStorage[] raw = Raw.ToArray();
            raw[index] = _encode(value);
            _setter(raw);
        }
    }

    /// <inheritdoc />
    public void Add(TExposed item)
    {
        List<TStorage> raw = Raw.ToList();
        raw.Add(_encode(item));
        _setter(raw.ToArray());
    }

    /// <inheritdoc />
    public bool Remove(TExposed item)
    {
        List<TStorage> raw = Raw.ToList();
        bool removed = raw.Remove(_encode(item));
        if (removed)
            _setter(raw.ToArray());
        return removed;
    }

    /// <inheritdoc />
    public void Clear()
    {
        _setter([]);
    }

    /// <inheritdoc />
    public bool Contains(TExposed item)
    {
        return Array.IndexOf(Raw, _encode(item)) >= 0;
    }

    /// <inheritdoc />
    public int IndexOf(TExposed item)
    {
        return Array.IndexOf(Raw, _encode(item));
    }

    /// <inheritdoc />
    public void Insert(int index, TExposed item)
    {
        List<TStorage> raw = Raw.ToList();
        raw.Insert(index, _encode(item));
        _setter(raw.ToArray());
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        List<TStorage> raw = Raw.ToList();
        raw.RemoveAt(index);
        _setter(raw.ToArray());
    }

    /// <inheritdoc />
    public void CopyTo(TExposed[] array, int arrayIndex)
    {
        for (int i = 0; i < Raw.Length; i++)
            array[arrayIndex + i] = _decode(Raw[i]);
    }

    /// <inheritdoc />
    public IEnumerator<TExposed> GetEnumerator()
    {
        return Raw.Select(item => _decode(item)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}