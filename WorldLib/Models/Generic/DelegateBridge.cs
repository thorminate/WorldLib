using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldLib.Models.Generic;

public sealed class DelegateBridge<TPublic, TGame>(
    Func<TPublic, TGame> wrapToGame,
    Action<TGame> gameAdd,
    Action<TGame> gameRemove)
    where TPublic : Delegate
    where TGame : Delegate
{
    private readonly Dictionary<TPublic, TGame> _map = new();

    public void Add(TPublic? handler)
    {
        if (handler == null || _map.ContainsKey(handler))
            return;

        var wrapped = wrapToGame(handler);
        _map[handler] = wrapped;
        gameAdd(wrapped);
    }

    public void Remove(TPublic? handler)
    {
        if (handler == null)
            return;

        if (!_map.TryGetValue(handler, out var wrapped))
            return;

        gameRemove(wrapped);
        _map.Remove(handler);
    }

    public void Clear()
    {
        foreach (var wrapped in _map.Values.ToArray())
            gameRemove(wrapped);

        _map.Clear();
    }
}