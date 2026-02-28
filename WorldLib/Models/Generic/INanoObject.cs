namespace WorldLib.Models.Generic;

extern alias GameAsm;

public interface INanoObject
{
    bool Alive { get; set; }
    bool Exists { get; set; }
    string Name { get; set; }
    int Hash { get; set; }

    GameAsm::ColorAsset Color { get; }

    //TODO: Abstract MetaType (using MetaTypeAsset)
    GameAsm::MetaType Type { get; }

    void SetDefaults();
    bool HasDied();
}