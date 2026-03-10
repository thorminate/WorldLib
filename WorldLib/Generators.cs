extern alias GameAsm;
using GameAsm::strings;
using WorldLib.Models.Ages;
using WorldLib.Models.Statuses;
using WorldLib.SourceGen;

[assembly: LibraryGen<string, Age>(
    Docs = "gen-docs/ages.json",
    IdClass = typeof(Ages.S_Ages),
    Static = false,
    TargetClass = "WorldLib.Models.Ages.Ages",
    Get = "Ages.GetAge(key)")]

[assembly: LibraryGen<string, bool>(
    IdClass = typeof(S_WorldLaw),
    Docs = "gen-docs/world-laws.json",
    Strip = "world_law_",
    Static = false,
    StaticMembers = false,
    TargetClass = "WorldLib.Models.Laws.Laws",
    Get = "Get(key)",
    Set = "Set(key, value)")]

[assembly: LibraryGen<string, StatusAsset>(
    Docs = "gen-docs/statuses.json",
    IdClass = typeof(S_Status),
    Static = false,
    TargetClass = "WorldLib.Models.Statuses.Statuses",
    Get = "Get(key)",
    Set = "Set(key, value)"
)]