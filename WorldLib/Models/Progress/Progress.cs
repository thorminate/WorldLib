using WorldLib.Utils;

namespace WorldLib.Models.Progress;

extern alias GameAsm;

public class Progress : AbstractionOf<GameAsm::GameProgress>
{
    internal Progress() : base(GameAsm::GameProgress.instance)
    {
    }

    //TODO: Extract contents and put into here.
    public GameAsm::GameProgressData Data
    {
        get => Raw.data;
        set => Raw.data = value;
    }
}