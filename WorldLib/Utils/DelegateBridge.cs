using System;
using System.Collections.Generic;
using System.Linq;

namespace WorldLib.Utils;

/// <summary>
///     Wraps a delegate to allow adding extra calls while also wrapping inner types for convenience.
/// </summary>
/// <param name="wrapToGame">Function for converting the public-facing wrapper to a game-compatible format.</param>
/// <param name="gameAdd">Function to add a new invocation to the original delegate.</param>
/// <param name="gameRemove">Function to remove an invocation from the original delegate.</param>
/// <typeparam name="TPublic">Type of the public-facing wrapper.</typeparam>
/// <typeparam name="TGame">Type of the game-compatible internal delegate.</typeparam>
public sealed class DelegateBridge<TPublic, TGame>(
    Func<TPublic, TGame> wrapToGame,
    Action<TGame> gameAdd,
    Action<TGame> gameRemove)
    where TPublic : Delegate
    where TGame : Delegate
{
    private readonly Dictionary<TPublic, TGame> _map = new();

    /// <summary>
    ///     Adds a new invocation to the invocation list of the underlying delegate.
    /// </summary>
    /// <param name="handler">The new handler to add to the delegate.</param>
    public void Add(TPublic? handler)
    {
        if (handler == null || _map.ContainsKey(handler))
            return;

        var wrapped = wrapToGame(handler);
        _map[handler] = wrapped;
        gameAdd(wrapped);
    }

    /// <summary>
    ///     Removes an invocation from the invocation list of the underlying delegate.
    /// </summary>
    /// <param name="handler">The handler to remove from the delegate.</param>
    public void Remove(TPublic? handler)
    {
        if (handler == null)
            return;

        if (!_map.TryGetValue(handler, out var wrapped))
            return;

        gameRemove(wrapped);
        _map.Remove(handler);
    }

    /// <summary>
    ///     Clears all delegates applied through this delegate bridge.
    /// </summary>
    public void Clear()
    {
        foreach (var wrapped in _map.Values.ToArray())
            gameRemove(wrapped);

        _map.Clear();
    }
}