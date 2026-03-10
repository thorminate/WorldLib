using UnityEngine;

namespace WorldLib.Models.Assets;

extern alias GameAsm;

/// <summary>
///     Represents a category of things like traits.
/// </summary>
public class CategoryAsset : Asset<GameAsm::BaseCategoryAsset>, ILocalizedAsset
{
    internal CategoryAsset(GameAsm::BaseCategoryAsset store) : base(store)
    {
    }

    /// <summary>
    ///     The color this category uses.
    /// </summary>
    public Color Color
    {
        get => Raw.getColor();
        set
        {
            Raw._color = value;
            Raw.color = GameAsm::Toolbox.colorToHex(value);
        }
    }

    /// <inheritdoc />
    public string Localized => GameAsm::StringExtension.Localize(Raw.getLocaleID());
}