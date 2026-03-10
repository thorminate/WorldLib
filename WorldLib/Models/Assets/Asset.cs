extern alias GameAsm;
using WorldLib.Utils;

namespace WorldLib.Models.Assets;

/// <summary>
///     Represents the base asset in the game, is used for a variety of things like traits and status effects.
/// </summary>
/// <typeparam name="TAbstraction">The type of Raw, is used for parent classes</typeparam>
public class Asset<TAbstraction> : AbstractionOf<TAbstraction> where TAbstraction : GameAsm::Asset
{
    internal Asset(TAbstraction store) : base(store)
    {
    }

    /// <summary>
    ///     ID of the asset. Is used as the primary identifier in internal libraries.
    /// </summary>
    public string Id => Raw.id;

    /// <summary>
    ///     Hash code of the asset. Is used for equality checks with other assets.
    /// </summary>
    public int Hash
    {
        get => Raw.GetHashCode();
        set => Raw.setHash(value);
    }
}