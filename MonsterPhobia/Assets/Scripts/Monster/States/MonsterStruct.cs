using System.Runtime.InteropServices;

public struct MonsterAttributes
{
    public MonsterType MType;
    public MonsterPassive MPassive;
    public MonsterAggro MAggro;

    public MonsterAttributes(MonsterType type)
    {
        MType = type;
        MAggro = MonsterAggro.Rush;

        switch (type)
        {
            case MonsterType.Axylotyl:
                MPassive = MonsterPassive.Wander;
                break;
            case MonsterType.Bill:
                MPassive = MonsterPassive.Stalk;
                break;
            case MonsterType.Garry:
                MPassive = MonsterPassive.Guard;
                break;
            case MonsterType.None:
            default:
                MPassive = MonsterPassive.None;
                break;
        }
    }
}
