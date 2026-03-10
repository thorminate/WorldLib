using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace WorldLib.Utils;

extern alias GameAsm;

/// <summary>
///     Neat utils that I use to simplify development.
/// </summary>
public static class Tooling
{
    private static readonly ConcurrentDictionary<Type,
        ConcurrentDictionary<string, Lazy<object?>>> Memos = new();

    /// <summary>
    ///     Runs the <paramref name="getter" /> and memoizes the result, all future calls instantly hook into that cache.
    /// </summary>
    /// <param name="id">ID of the item to insert or lookup</param>
    /// <param name="getter">Getter function if the cache does not hit.</param>
    /// <param name="cacheNull">Whether to allow caching a null return value, defaults to true.</param>
    /// <typeparam name="T">The return type of <paramref name="getter" /></typeparam>
    /// <returns>A nullable value representing the cache-hit or evaluated result.</returns>
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

    /// <summary>
    ///     Clears a specific id from the memoization cache.
    /// </summary>
    /// <param name="id">ID of the item to remove.</param>
    /// <typeparam name="T">Type of the item.</typeparam>
    /// <returns>Whether this process succeeded in deleting something.</returns>
    public static bool MemoClear<T>(string id)
    {
        return Memos.TryGetValue(typeof(T), out ConcurrentDictionary<string, Lazy<object?>>? typeDict) &&
               typeDict.TryRemove(id, out _);
    }

    /// <summary>
    ///     Clears all memoized values of a specific type.
    /// </summary>
    /// <typeparam name="T">The type to wipe out.</typeparam>
    /// <returns>Whether this process succeeded in deleting something.</returns>
    public static bool MemoClear<T>()
    {
        return Memos.TryRemove(typeof(T), out _);
    }

    /// <summary>
    ///     Clears the entire memoization cache, top-to-bottom.
    /// </summary>
    public static void MemoClearAll()
    {
        Memos.Clear();
    }

    /// <summary>
    ///     Converts a method to use a TryGet styled behavior.
    /// </summary>
    /// <param name="getter">The function to run to get T.</param>
    /// <param name="result">The output of the getter.</param>
    /// <typeparam name="T">The return type of the getter.</typeparam>
    /// <returns>Whether <paramref name="result" /> is not null.</returns>
    public static bool TryRun<T>(Func<T> getter, out T result)
    {
        result = getter();
        return result != null;
    }

    /// <summary>
    ///     Converts a hex code to a unity color object.
    /// </summary>
    /// <param name="hex">The stringified hex to use. May be without a #.</param>
    /// <returns>A unity color object representing that hex color.</returns>
    public static Color ParseHex(string hex)
    {
        if (!hex.StartsWith("#"))
            hex = "#" + hex;
        return GameAsm::Toolbox.makeColor(hex);
    }
}