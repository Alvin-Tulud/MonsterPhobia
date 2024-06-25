using System.Runtime.InteropServices;

public struct MonsterAttributes
{
    public MonsterType MType;
    public MonsterPassive[] MPassives;
    public MonsterAggro[] MAggros;

    public MonsterAttributes(MonsterType type)
    {
        MType = type;

        switch (type)
        {
            case MonsterType.Axylotyl:
                MPassives = new[] { MonsterPassive.Wander };
                MAggros = new[] { MonsterAggro.Rush };
                break;
            case MonsterType.Bill:
                MPassives = new[] { MonsterPassive.Stalk };
                MAggros = new[] { MonsterAggro.Stalk };
                break;
            case MonsterType.Garry:
                MPassives = new[] { MonsterPassive.Guard };
                MAggros = new[] { MonsterAggro.Rush };
                break;
            case MonsterType.None:
            default:
                MPassives = new[] { MonsterPassive.None };
                MAggros = new[] { MonsterAggro.None };
                break;
        }
    }
}
