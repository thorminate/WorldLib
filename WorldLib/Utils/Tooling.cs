using System;
using System.Collections.Concurrent;
using System.Threading;

namespace WorldLib.Utils;

public static class Tooling
{
    private static readonly ConcurrentDictionary<Type,
        ConcurrentDictionary<string, Lazy<object?>>> Memos = new();

    public static T? Memoized<T>(
        string id,
        Func<T?> getter,
        bool cacheNull = true)
    {
        ConcurrentDictionary<string, Lazy<object?>> typeDict = Memos.GetOrAdd(
            typeof(T),
            _ => new ConcurrentDictionary<string, Lazy<object?>>());

        if (cacheNull)
        {
            Lazy<object?>? lazy = typeDict.GetOrAdd(
                id,
                _ => new Lazy<object?>(
                    () => getter(),
                    LazyThreadSafetyMode.ExecutionAndPublication));

            return (T?)lazy.Value;
        }

        Lazy<object?>? lazyNonNullOnly = typeDict.GetOrAdd(
            id,
            _ => new Lazy<object?>(
                () => getter(),
                LazyThreadSafetyMode.ExecutionAndPublication));

        object? value = lazyNonNullOnly.Value;

        if (value == null)
            typeDict.TryRemove(id, out _);

        return (T?)value;
    }

    public static bool MemoClear<T>(string id)
    {
        return Memos.TryGetValue(typeof(T), out ConcurrentDictionary<string, Lazy<object?>>? typeDict) &&
               typeDict.TryRemove(id, out _);
    }

    public static bool MemoClear<T>()
    {
        return Memos.TryRemove(typeof(T), out _);
    }

    public static void MemoClearAll()
    {
        Memos.Clear();
    }

    public static bool TryRun<T>(Func<T> getter, out T result)
    {
        result = getter();
        return result != null;
    }
}