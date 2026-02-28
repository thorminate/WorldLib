extern alias GameAsm;
using System.Collections.Generic;
using System.Linq;
using GameAsm::db;
using WorldLib.Utils;

namespace WorldLib.Models.History;

extern alias GameAsm;

/// <summary>
///     Provides a high-level abstraction over the game's WorldLog system,
///     allowing for reading recent world events and logging new events such as kings, clans, cities,
///     wars, alliances, and disasters.
/// </summary>
public class WorldHistory : AbstractionOf<GameAsm::WorldLog>
{
    internal WorldHistory() : base(GameAsm::WorldLog.instance)
    {
    }

    /// <summary>
    ///     Provides the last 2000 world logs in the form of a <see cref="IReadOnlyList{WorldLogMessage}" />.
    /// </summary>
    /// <remarks>
    ///     <para>Fetching world logs involves querying the SQL database, so this operation can be <see cref="Slow" />!</para>
    ///     <para>Only the last 2000 entries are retrieved at a time.</para>
    /// </remarks>
    /// <returns>
    ///     A <see cref="IReadOnlyList{WorldLogMessage}" /> containing the last 2000 world logs.
    /// </returns>
    public IReadOnlyList<WorldHistoryEntry> Messages()
    {
        return DBGetter
            .getWorldLogMessages()
            .Select(entry => new WorldHistoryEntry(entry))
            .ToList();
    }

    #region Creating Logs

    /// <summary>
    ///     Logs the creation of a new king in the world.
    /// </summary>
    /// <param name="kingdom">The kingdom the new king belongs to.</param>
    //TODO: Abstract Kingdom
    public void LogNewKing(GameAsm::Kingdom kingdom)
    {
        GameAsm::WorldLog.logNewKing(kingdom);
    }

    /// <summary>
    ///     Logs the creation of a new royal clan in a kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom where the clan belongs.</param>
    /// <param name="clan">The new royal clan.</param>
    //TODO: Abstract Kingdom and Clan
    public void LogNewRoyalClan(GameAsm::Kingdom kingdom, GameAsm::Clan clan)
    {
        GameAsm::WorldLog.logRoyalClanNew(kingdom, clan);
    }

    /// <summary>
    ///     Logs a royal clan changing within a kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom where the clan change occurs.</param>
    /// <param name="oldClan">The previous clan.</param>
    /// <param name="newClan">The new clan replacing it.</param>
    //TODO: Abstract Kingdom and Clan
    public void LogChangedRoyalClan(GameAsm::Kingdom kingdom, GameAsm::Clan oldClan, GameAsm::Clan newClan)
    {
        GameAsm::WorldLog.logRoyalClanChanged(kingdom, oldClan, newClan);
    }

    /// <summary>
    ///     Logs that a royal clan has been destroyed in a kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom affected.</param>
    /// <param name="clan">The clan being destroyed.</param>
    //TODO: Abstract Kingdom and Clan
    public void LogRoyalClanDestroyed(GameAsm::Kingdom kingdom, GameAsm::Clan clan)
    {
        GameAsm::WorldLog.logRoyalClanNoMore(kingdom, clan);
    }

    /// <summary>
    ///     Logs that a king has fled their city.
    /// </summary>
    /// <param name="kingdom">The kingdom of the king.</param>
    /// <param name="king">The actor who fled.</param>
    //TODO: Abstract Kingdom and Actor
    public void LogKingFledCity(GameAsm::Kingdom kingdom, GameAsm::Actor king)
    {
        GameAsm::WorldLog.logKingFledCity(kingdom, king);
    }

    /// <summary>
    ///     Logs that a king has fled the capital.
    /// </summary>
    /// <param name="kingdom">The kingdom of the king.</param>
    /// <param name="king">The actor who fled.</param>
    //TODO: Abstract Kingdom and Actor
    public void LogKingFledCapital(GameAsm::Kingdom kingdom, GameAsm::Actor king)
    {
        GameAsm::WorldLog.logKingFledCapital(kingdom, king);
    }

    /// <summary>
    ///     Logs that a king has left the kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom of the king.</param>
    /// <param name="king">The actor who left.</param>
    //TODO: Abstract Kingdom and Actor
    public void LogKingLeft(GameAsm::Kingdom kingdom, GameAsm::Actor king)
    {
        GameAsm::WorldLog.logKingLeft(kingdom, king);
    }

    /// <summary>
    ///     Logs that a king has died naturally.
    /// </summary>
    /// <param name="kingdom">The kingdom of the king.</param>
    /// <param name="king">The actor who died.</param>
    //TODO: Abstract Kingdom and Actor
    public void LogKingDead(GameAsm::Kingdom kingdom, GameAsm::Actor king)
    {
        GameAsm::WorldLog.logKingDead(kingdom, king);
    }

    /// <summary>
    ///     Logs that a king was murdered.
    /// </summary>
    /// <param name="kingdom">The kingdom of the king.</param>
    /// <param name="king">The actor who was killed.</param>
    /// <param name="attacker">The actor who performed the murder.</param>
    //TODO: Abstract Kingdom and Actor
    public void LogKingMurder(GameAsm::Kingdom kingdom, GameAsm::Actor king, GameAsm::Actor attacker)
    {
        GameAsm::WorldLog.logKingMurder(kingdom, king, attacker);
    }

    /// <summary>
    ///     Logs that a favorite actor has died.
    /// </summary>
    /// <param name="favourite">The actor who died.</param>
    //TODO: Abstract Actor
    public void LogFavouriteDead(GameAsm::Actor favourite)
    {
        GameAsm::WorldLog.logFavDead(favourite);
    }

    /// <summary>
    ///     Logs that a favorite actor was murdered.
    /// </summary>
    /// <param name="favourite">The actor who was killed.</param>
    /// <param name="attacker">The actor who performed the murder.</param>
    //TODO: Abstract Actor
    public void LogFavouriteMurder(GameAsm::Actor favourite, GameAsm::Actor attacker)
    {
        GameAsm::WorldLog.logFavMurder(favourite, attacker);
    }

    /// <summary>
    ///     Logs the creation of a new city.
    /// </summary>
    /// <param name="city">The city being created.</param>
    //TODO: Abstract City
    public void LogNewCity(GameAsm::City city)
    {
        GameAsm::WorldLog.logNewCity(city);
    }

    /// <summary>
    ///     Logs a city revolt event.
    /// </summary>
    /// <param name="city">The city undergoing revolt.</param>
    //TODO: Abstract City
    public void LogCityRevolt(GameAsm::City city)
    {
        GameAsm::WorldLog.logCityRevolt(city);
    }

    /// <summary>
    ///     Logs the end of a war.
    /// </summary>
    /// <param name="war">The war that ended.</param>
    //TODO: Abstract War
    public void LogWarEnded(GameAsm::War war)
    {
        GameAsm::WorldLog.logWarEnded(war);
    }

    /// <summary>
    ///     Logs the start of a new war between two kingdoms.
    /// </summary>
    /// <param name="kingdom1">The first kingdom.</param>
    /// <param name="kingdom2">The second kingdom.</param>
    //TODO: Abstract Kingdom
    public void LogNewWar(GameAsm::Kingdom kingdom1, GameAsm::Kingdom kingdom2)
    {
        GameAsm::WorldLog.logNewWar(kingdom1, kingdom2);
    }

    /// <summary>
    ///     Logs the declaration of a total war for a kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom declaring total war.</param>
    //TODO: Abstract Kingdom
    public void LogNewTotalWar(GameAsm::Kingdom kingdom)
    {
        GameAsm::WorldLog.logNewTotalWar(kingdom);
    }

    /// <summary>
    ///     Logs the creation of an alliance.
    /// </summary>
    /// <param name="alliance">The alliance being created.</param>
    //TODO: Abstract Alliance
    public void LogAllianceCreated(GameAsm::Alliance alliance)
    {
        GameAsm::WorldLog.logAllianceCreated(alliance);
    }

    /// <summary>
    ///     Logs the dissolution of an alliance.
    /// </summary>
    /// <param name="alliance">The alliance being dissolved.</param>
    //TODO: Abstract Alliance
    public void LogAllianceDissolved(GameAsm::Alliance alliance)
    {
        GameAsm::WorldLog.logAllianceDisolved(alliance);
    }

    /// <summary>
    ///     Logs the creation of a new kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom being created.</param>
    //TODO: Abstract Kingdom
    public void LogNewKingdom(GameAsm::Kingdom kingdom)
    {
        GameAsm::WorldLog.logNewKingdom(kingdom);
    }

    /// <summary>
    ///     Logs the destruction of a kingdom.
    /// </summary>
    /// <param name="kingdom">The kingdom being destroyed.</param>
    //TODO: Abstract Kingdom
    public void LogKingdomDestroyed(GameAsm::Kingdom kingdom)
    {
        GameAsm::WorldLog.logKingdomDestroyed(kingdom);
    }

    /// <summary>
    ///     Logs the destruction of a city.
    /// </summary>
    /// <param name="city">The city being destroyed.</param>
    //TODO: Abstract City
    public void LogCityDestroyed(GameAsm::City city)
    {
        GameAsm::WorldLog.logCityDestroyed(city);
    }

    /// <summary>
    ///     Logs a disaster event occurring at a world tile, optionally linked to a city or unit.
    /// </summary>
    /// <param name="asset">The disaster asset responsible for the event.</param>
    /// <param name="tile">The tile where the disaster occurs.</param>
    /// <param name="name">Optional custom name for the disaster.</param>
    /// <param name="city">Optional city affected by the disaster.</param>
    /// <param name="unit">Optional unit affected by the disaster.</param>
    //TODO: Abstract DisasterAsset, WorldTile, City and Actor
    public void LogDisaster(GameAsm::DisasterAsset asset, GameAsm::WorldTile tile, string? name,
        GameAsm::City? city, GameAsm::Actor? unit)
    {
        GameAsm::WorldLog.logDisaster(asset, tile, name, city, unit);
    }

    #endregion
}