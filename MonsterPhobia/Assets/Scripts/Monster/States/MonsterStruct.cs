using System.Runtime.InteropServices;

public struct MonsterAttributes
{
    public MonsterType MType;
    public MonsterPassive MPassive;
    public MonsterAggro MAggro;

    public MonsterAttributes(MonsterType type)
    {
        MType = type;
        MAggro = MonsterAggro.Chaser;

        switch (type)
        {
            case MonsterType.Axylotyl:
                MPassive = MonsterPassive.Wanderer;
                break;
            case MonsterType.Bill:
                MPassive = MonsterPassive.Stalker;
                break;
            case MonsterType.Garry:
                MPassive = MonsterPassive.Territorial;
                break;
            case MonsterType.None:
            default:
                MPassive = MonsterPassive.None;
                break;
        }
    }
}
