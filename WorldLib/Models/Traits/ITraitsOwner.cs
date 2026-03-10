using WorldLib.Utils;

namespace WorldLib.Models.Traits;

extern alias GameAsm;

/// <summary>
///     Represents an owner of traits.
/// </summary>
/// <typeparam name="TPublic">What public-facing trait this contains.</typeparam>
/// <typeparam name="TGame">What game-facing trait this contains.</typeparam>
public interface ITraitsOwner<TPublic, TGame>
    where TPublic : Trait<TPublic, TGame>
    where TGame : GameAsm::BaseTrait<TGame>
{
    //TODO: Abstract the inner contents of this.
    /// <summary>
    ///     A <see cref="TransmutableHashSet{TStorage,TExposed}" /> of all the traits this trait owner has.
    /// </summary>
    /// <seealso cref="HasTrait" />
    /// <seealso cref="HasAnyTrait" />
    /// <seealso cref="AddTrait" />
    /// <seealso cref="RemoveTrait" />
    public TransmutableHashSet<TGame, TPublic> Traits { get; }

    /// <summary>
    ///     Whether this trait owner has a specific trait.
    /// </summary>
    /// <param name="trait">The trait to check for.</param>
    /// <returns>Whether the trait exists.</returns>
    public bool HasTrait(TPublic trait);

    /// <summary>
    ///     Adds a trait to the <see cref="Traits" /> list.
    /// </summary>
    /// <param name="trait">The trait to add.</param>
    /// <param name="removeOpposites">Whether to remove the opposite traits upon applying this trait.</param>
    /// <returns>If the method succeeded.</returns>
    public bool AddTrait(TPublic trait, bool removeOpposites = false);

    /// <summary>
    ///     Removes a trait from the <see cref="Traits" /> list.
    /// </summary>
    /// <param name="trait">The trait to remove.</param>
    /// <returns>If the method succeeded.</returns>
    public bool RemoveTrait(TPublic trait);

    /// <summary>
    ///     Whether the <see cref="Traits" /> list has any trait.
    /// </summary>
    /// <returns>If the <see cref="Traits" /> list has any trait.</returns>
    public bool HasAnyTrait();
}