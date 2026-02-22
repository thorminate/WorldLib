using System;
using System.Collections.Concurrent;
using System.Threading;

namespace WorldLib.Utils;

public static class Tooling
{
    private static readonly ConcurrentDictionary<Type,
        ConcurrentDictionary<string, Lazy<object>>> Memos = new();

    public static T Memoized<T>(string id, Func<T> getter)
    {
        var typeDict = Memos.GetOrAdd(
            typeof(T),
            _ => new ConcurrentDictionary<string, Lazy<object>>());

        var lazy = typeDict.GetOrAdd(
            id,
            _ => new Lazy<object>(() => getter()!,
                LazyThreadSafetyMode.ExecutionAndPublication));

        return (T)lazy.Value;
    }

    public static bool MemoClear<T>(string id)
    {
        return Memos.TryGetValue(typeof(T), out var typeDict) && typeDict.TryRemove(id, out _);
    }

    public static bool MemoClear<T>()
    {
        return Memos.TryRemove(typeof(T), out _);
    }

    public static void MemoClearAll()
    {
        Memos.Clear();
    }
}