using System;
using System.Collections;
using System.Collections.Generic;

namespace WorldLib.Utils;

/// <inheritdoc />
/// Every mutation runs the onChanged event.
public sealed class MonitoredList<T> : IList<T>
{
    private readonly List<T> _inner;
    private readonly Action _onChanged;

    /// <summary>
    ///     Wraps an existing <see cref="List{T}" /> and invokes a callback
    ///     whenever the collection is structurally modified.
    /// </summary>
    /// <param name="inner">
    ///     The underlying list to wrap. Mutations performed through this wrapper
    ///     operate directly on this instance.
    /// </param>
    /// <param name="onChanged">
    ///     The callback invoked after any successful structural modification.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="onChanged" /> or <paramref name="inner" /> is <see langword="null" />.
    /// </exception>
    public MonitoredList(List<T> inner, Action onChanged)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        _onChanged = onChanged ?? throw new ArgumentNullException(nameof(onChanged));
    }

    /// <inheritdoc />
    public void Add(T item)
    {
        _inner.Add(item);
        _onChanged();
    }

    /// <inheritdoc />
    public bool Remove(T item)
    {
        bool result = _inner.Remove(item);
        if (result) _onChanged();
        return result;
    }

    /// <inheritdoc />
    public T this[int index]
    {
        get => _inner[index];
        set
        {
            _inner[index] = value;
            _onChanged();
        }
    }

    /// <inheritdoc />
    public int Count => _inner.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public void Clear()
    {
        _inner.Clear();
        _onChanged();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
        _inner.RemoveAt(index);
        _onChanged();
    }

    /// <inheritdoc cref="List{T}.Contains(T)" />
    public bool Contains(T item)
    {
        return _inner.Contains(item);
    }

    /// <inheritdoc cref="List{T}.CopyTo(T[], int)" />
    public void CopyTo(T[] array, int index)
    {
        _inner.CopyTo(array, index);
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return _inner.GetEnumerator();
    }

    /// <inheritdoc />
    public int IndexOf(T item)
    {
        return _inner.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, T item)
    {
        _inner.Insert(index, item);
        _onChanged();
    }
}