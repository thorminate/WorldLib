using UnityEngine;
using WorldLib.Registries;

namespace WorldLib.Models.Assets;

extern alias GameAsm;

/// <summary>
///     Represents an asset that is unlockable, like traits that have to be discovered.
/// </summary>
/// <typeparam name="TAbstraction">The type of <see cref="UnlockableAsset{TAbstraction}.Raw" /></typeparam>
public class UnlockableAsset<TAbstraction> : Asset<TAbstraction>, ILocalizedAsset
    where TAbstraction : GameAsm::BaseUnlockableAsset
{
    internal UnlockableAsset(TAbstraction store) : base(store)
    {
    }

    //TODO: Abstract Achievement
    /// <summary>
    ///     An achievement that you need to get to unlock this asset.
    /// </summary>
    public GameAsm::Achievement Achievement
    {
        get => Raw.getAchievement();
        set => Raw.achievement_id = value.id;
    }

    /// <summary>
    ///     The icon of the asset.
    /// </summary>
    public Sprite Icon
    {
        get => Raw.getSprite();
        set
        {
            Raw.cached_sprite = value;
            Raw.path_icon = value.name;
            Sprites.Register(value);
        }
    }

    /// <summary>
    ///     Stats that will be applied to the actor upon associating this asset (traits, cultures, etc.)
    /// </summary>
    //TODO: Abstract BaseStats
    public GameAsm::BaseStats Stats
    {
        get => Raw.base_stats;
        set => Raw.base_stats = value;
    }

    /// <summary>
    ///     The localized name of this asset.
    /// </summary>
    public string Localized => GameAsm::StringExtension.Localize(Raw.getLocaleID());

    /// <summary>
    ///     Whether this asset is only unlockable through getting an achievement first.
    /// </summary>
    /// <param name="achievement">The achievement to lock this asset behind.</param>
    //TODO: Abstract Achievement
    public void SetUnlockedWithAchievement(GameAsm::Achievement achievement)
    {
        Raw.setUnlockedWithAchievement(achievement.id);
    }

    /// <summary>
    ///     Unlocks this asset.
    /// </summary>
    /// <param name="saveData"></param>
    /// <returns>Whether the operation succeeded.</returns>
    public bool Unlock(bool saveData = true)
    {
        return Raw.unlock(saveData);
    }

    /// <summary>
    ///     Whether this asset has been explicitly unlocked via progression.
    ///     Does not account for cursed world or debug overrides, use <see cref="IsAvailable" /> for that.
    /// </summary>
    public bool IsUnlocked()
    {
        return Raw.isUnlocked();
    }

    /// <summary>
    ///     Whether this asset is accessible to the player by any means, including cursed world, debug,
    ///     achievements, or explicit unlocking via <see cref="IsUnlocked" />.
    /// </summary>
    public bool IsAvailable()
    {
        return Raw.isAvailable();
    }

    /// <summary>
    ///     Whether this asset has been unlocked specifically through player action,
    ///     either via achievement or exploration. Unlike <see cref="IsAvailable" />,
    ///     this ignores debug and cheat overrides.
    /// </summary>
    public bool IsUnlockedByPlayer()
    {
        return Raw.isUnlockedByPlayer();
    }
}