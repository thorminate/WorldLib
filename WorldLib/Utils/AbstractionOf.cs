using System;
using System.Runtime.CompilerServices;

namespace WorldLib.Utils;

/// <summary>
///     Provides a lightweight abstraction layer over an underlying store instance,
///     exposing controlled access to the wrapped object.
/// </summary>
/// <typeparam name="TStore">
///     The type of the underlying object being abstracted.
/// </typeparam>
/// <remarks>
///     The underlying store is resolved through a delegate and is recomputed
///     on every access to <c>Base</c>. This allows the abstraction to reflect
///     dynamic or externally replaced instances instead of capturing a fixed reference.
/// </remarks>
public abstract class AbstractionOf<TStore>
{
    private readonly Func<TStore> _baseGetter;

    internal AbstractionOf(TStore store)
    {
        _baseGetter = () => store;
    }

    internal AbstractionOf(Func<TStore> getter)
    {
        _baseGetter = getter;
    }

    /// <summary>
    ///     <para>
    ///         Raw access to the internal class that this is representing.
    ///     </para>
    ///     <para>
    ///         For advanced users who need complete access to the game domain.
    ///     </para>
    /// </summary>
    public TStore Raw
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _baseGetter();
    }
}