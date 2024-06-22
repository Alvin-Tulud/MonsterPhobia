using System.Runtime.InteropServices;

public struct MonsterAttributes
{
    public MonsterType MType;
    public MonsterPatrol[] MPatrols;
    public MonsterChase[] MChases;
    public MonsterAttack[] MAttacks;

    public MonsterAttributes(MonsterType type)
    {
        MType = type;

        switch (type)
        {
            case MonsterType.Axylotyl:
                MPatrols = new[] { MonsterPatrol.Wander };
                MChases = new[] { MonsterChase.Rush };
                MAttacks = new[] { MonsterAttack.Contact };
                break;
            case MonsterType.Bill:
                MPatrols = new[] { MonsterPatrol.Corner };
                MChases = new[] { MonsterChase.Stalk };
                MAttacks = new[] { MonsterAttack.Contact };
                break;
            case MonsterType.Garry:
                MPatrols = new[] { MonsterPatrol.Sprint };
                MChases = new[] { MonsterChase.Rush };
                MAttacks = new[] { MonsterAttack.Contact };
                break;
            case MonsterType.None:
            default:
                MPatrols = new[] { MonsterPatrol.None };
                MChases = new[] { MonsterChase.None };
                MAttacks = new[] { MonsterAttack.None };
                break;
        }
    }
}
