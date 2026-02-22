using System;

namespace WorldLib.Utils;

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

    internal TStore Base => _baseGetter();
}