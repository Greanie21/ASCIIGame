public class Spirit
{
    public String name;
    public Element.Type element;
    private int level;
    public int experience = 1;
    public Archtype.Type archtype;

    private int[] healthPoints = new int[2];
    private int[] attack = new int[2];
    private int[] magicAttack = new int[2];
    private int[] defense = new int[2];
    private int[] magicDefense = new int[2];
    private int[] speed = new int[2];
    public int[] accuracy = new int[2];
    public int[] dodge = new int[2];

    public SpiritAttack[] attacks = new SpiritAttack[4];

    public bool wild = false;

    public Spirit(int lvl, int xp,
            int currentHp, int maxHp,
            int maxAtk, int maxMagicAtk,
            int maxDef, int maxMagicDef,
            int maxSpd,
            int maxAccuracy, int maxDodge,
            String spiritName, Element.Type type, Archtype.Type archtype,
            SpiritAttack[] atks)
    {
        level = lvl;
        experience = xp;
        healthPoints[0] = currentHp;
        healthPoints[1] = maxHp;
        attack[0] = maxAtk;
        attack[1] = maxAtk;
        magicAttack[0] = maxMagicAtk;
        magicAttack[1] = maxMagicAtk;
        defense[0] = maxDef;
        defense[1] = maxDef;
        magicDefense[0] = maxMagicDef;
        magicDefense[1] = maxMagicDef;
        speed[1] = maxSpd;
        accuracy[0] = maxAccuracy;
        accuracy[1] = maxAccuracy;
        dodge[0] = maxDodge;
        dodge[1] = maxDodge;

        name = spiritName;
        attacks = atks;


        element = type;
        this.archtype = archtype;
    }

    public Spirit(int[] stats, String spiritName, Element.Type element, Archtype.Type archtype, SpiritAttack[] atks)
    {

        level = stats[0];
        experience = stats[1];
        healthPoints[0] = stats[2];
        healthPoints[1] = stats[3];
        attack[0] = stats[4];
        attack[1] = stats[4];
        magicAttack[0] = stats[5];
        magicAttack[1] = stats[5];
        defense[0] = stats[6];
        defense[1] = stats[6];
        magicDefense[0] = stats[7];
        magicDefense[1] = stats[7];
        speed[1] = stats[8];
        accuracy[0] = stats[9];
        accuracy[1] = stats[9];
        dodge[0] = stats[10];
        dodge[1] = stats[10];

        name = spiritName;
        attacks = atks;


        this.element = element;
        this.archtype = archtype;
    }


    /**
     * @return the level
     */
    public int getLevel()
    {
        return level;
    }

    /**
     * @param level the level to set
     */
    public void setLevel(int level)
    {
        this.level = level;
    }

    /**
     * @return the healthPoints
     */
    public int[] getHP()
    {
        return healthPoints;
    }

    /**
     * @param healthPoints the healthPoints to set
     */
    public void setHP(int index, int healthPoints)
    {
        this.healthPoints[index] = healthPoints;
    }

    /**
     * @return the attack
     */
    public int[] getAttack()
    {
        return attack;
    }

    /**
     * @param attack the attack to set
     */
    public void setAttack(int index, int attack)
    {
        this.attack[index] = attack;
    }

    /**
     * @return the magicAttack
     */
    public int[] getMagicAttack()
    {
        return magicAttack;
    }

    /**
     * @param magicAttack the magicAttack to set
     */
    public void setMagicAttack(int index, int magicAttack)
    {
        this.magicAttack[index] = magicAttack;
    }

    /**
     * @return the defense
     */
    public int[] getDefense()
    {
        return defense;
    }

    /**
     * @param defense the defense to set
     */
    public void setDefense(int index, int defense)
    {
        this.defense[index] = defense;
    }

    /**
     * @return the magicDefense
     */
    public int[] getMagicDefense()
    {
        return magicDefense;
    }

    /**
     * @param magicDefense the magicDefence to set
     */
    public void setMagicDefense(int index, int magicDefence)
    {
        this.magicDefense[index] = magicDefence;
    }

    /**
     * @return the speed
     */
    public int[] getSpeed()
    {
        return speed;
    }

    /**
     * @param speed the speed to set
     */
    public void setSpeed(int index, int speed)
    {
        this.speed[index] = speed;
    }

    public void LevelUp()
    {
        setLevel(getLevel() + 1);

        GrowthRate rate = SpiritGrowthRate();

        float newAttack1 = getAttack()[0] * rate.attack / 100.0f;
        float newAttack2 = getAttack()[1] * rate.attack / 100.0f;
        setAttack(0, (int)MathF.Round(newAttack1));
        setAttack(1, (int)MathF.Round(newAttack2));

        float newDefense1 = getDefense()[0] * rate.defense / 100.0f;
        float newDefense2 = getDefense()[1] * rate.defense / 100.0f;
        setDefense(0, (int)MathF.Round(newDefense1));
        setDefense(1, (int)MathF.Round(newDefense2));

        float newHP1 = getHP()[0] * rate.healthPoints / 100.0f;
        float newHP2 = getHP()[1] * rate.healthPoints / 100.0f;
        if (getHP()[0] > 0)
        {
            setHP(0, (int)MathF.Round(newHP1));
        }
        setHP(1, (int)MathF.Round(newHP2));

        float newMagicAttack1 = getMagicAttack()[0] * rate.magicAttack / 100.0f;
        float newMagicAttack2 = getMagicAttack()[1] * rate.magicAttack / 100.0f;
        setMagicAttack(0, (int)MathF.Round(newMagicAttack1));
        setMagicAttack(1, (int)MathF.Round(newMagicAttack2));

        float newMagicDefense1 = getMagicDefense()[0] * rate.magicDefense / 100.0f;
        float newMagicDefense2 = getMagicDefense()[1] * rate.magicDefense / 100.0f;
        setMagicDefense(0, (int)MathF.Round(newMagicDefense1));
        setMagicDefense(1, (int)MathF.Round(newMagicDefense2));

        float newSpeed1 = getSpeed()[0] * rate.speed / 100.0f;
        float newSpeed2 = getSpeed()[1] * rate.speed / 100.0f;
        setSpeed(0, (int)MathF.Round(newSpeed1));
        setSpeed(1, (int)MathF.Round(newSpeed2));

        float newAccuracy1 = accuracy[0] * rate.accuracy / 100.0f;
        float newAccuracy2 = accuracy[1] * rate.accuracy / 100.0f;
        accuracy[0] = (int)MathF.Round(newAccuracy1);
        accuracy[1] = (int)MathF.Round(newAccuracy2);

        float newDodge1 = dodge[0] * rate.dodge / 100.0f;
        float newDodge2 = dodge[1] * rate.dodge / 100.0f;
        dodge[0] = (int)MathF.Round(newDodge1);
        dodge[1] = (int)MathF.Round(newDodge2);

    }

    public bool CheckIfSpiritLeveledUp()
    {
        int lvl = getLevel();
        if (lvl == 100)
        {
            return false;
        }

        int xpToUp = (lvl * 150) + (lvl ^ 4) / (225 - lvl / 2);
        if (experience >= xpToUp)
        {
            experience -= xpToUp;
            LevelUp();

            while (CheckIfSpiritLeveledUp())
            {
                //continue checking untill you dont level up anymore
            }

            return true;
        }
        return false;
    }

    private GrowthRate SpiritGrowthRate()
    {
        GrowthRate rate = new GrowthRate();
        GrowthRate rate1 = BaseGrowtRate();
        GrowthRate rate2 = ElementGrowtRate(this.element);
        GrowthRate rate3 = ArchtypeGrowtRate(this.archtype);

        rate.healthPoints = rate1.healthPoints + rate2.healthPoints + rate3.healthPoints;
        rate.attack = rate1.attack + rate2.attack + rate3.attack;
        rate.magicAttack = rate1.magicAttack + rate2.magicAttack + rate3.magicAttack;
        rate.defense = rate1.defense + rate2.defense + rate3.defense;
        rate.magicDefense = rate1.magicDefense + rate2.magicDefense + rate3.magicDefense;
        rate.speed = rate1.speed + rate2.speed + rate3.speed;
        rate.accuracy = rate1.accuracy + rate2.accuracy + rate3.accuracy;
        rate.dodge = rate1.dodge + rate2.dodge + rate3.dodge;

        return rate;
    }

    private GrowthRate BaseGrowtRate()
    {
        GrowthRate rate = new GrowthRate();

        rate.healthPoints = 15;
        rate.attack = 12;
        rate.magicAttack = 12;
        rate.defense = 8;
        rate.magicDefense = 8;
        rate.speed = 5;
        rate.accuracy = 5;
        rate.dodge = 5;

        return rate;
    }

    private GrowthRate ElementGrowtRate(Element.Type type)
    {
        GrowthRate rate = new GrowthRate();

        switch (type)
        {
            case Element.Type.Water:
                rate.magicAttack = 10;
                rate.magicDefense = 10;
                rate.dodge = 10;
                rate.defense = -10;
                break;
            case Element.Type.Fire:
                rate.attack = 10;
                rate.magicAttack = 10;
                rate.accuracy = 10;
                rate.magicDefense = -10;
                break;
            case Element.Type.Earth:
                rate.healthPoints = 10;
                rate.defense = 10;
                rate.magicDefense = 10;
                rate.speed = -10;
                break;
            case Element.Type.Air:
            default:
                rate.speed = 10;
                rate.accuracy = 10;
                rate.dodge = 10;
                rate.healthPoints = -10;
                break;
        }

        return rate;
    }

    private GrowthRate ArchtypeGrowtRate(Archtype.Type type)
    {
        GrowthRate rate = new GrowthRate();
        switch (type)
        {
            case Archtype.Type.GlassCannon:
                rate.attack = 12;
                rate.magicAttack = 12;
                rate.speed = -8;
                rate.accuracy = -8;
                rate.dodge = -8;
                break;
            case Archtype.Type.Tank:
                rate.healthPoints = 8;
                rate.defense = 6;
                rate.magicDefense = 6;
                rate.speed = -10;
                rate.magicAttack = -6;
                rate.attack = -4;
                break;
            case Archtype.Type.Speedster:
                rate.speed = 15;
                rate.healthPoints = -3;
                rate.attack = -3;
                rate.magicAttack = -3;
                rate.defense = -3;
                rate.magicDefense = -3;
                break;
            case Archtype.Type.Powerhouse:
                rate.attack = 15;
                rate.magicAttack = 15;
                rate.accuracy = -30;
                break;
            case Archtype.Type.Balanced:
            default:
                break;
        }

        return rate;
    }
}
