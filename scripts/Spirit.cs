public class Spirit
{
    public String name;
    public Element.Type element;
    private int level;
    public int experience = 1;

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
            String spiritName, String type,
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


        element = switchElement(type);
    }

    public Spirit(int[] stats, String spiritName, Element.Type element, SpiritAttack[] atks)
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


        element = element;
    }

    private Element.Type switchElement(string type)
    {
        switch (type)
        {
            case "Water":
                return Element.Type.Water;
            case "Earth":
                return Element.Type.Earth;
            case "Fire":
                return Element.Type.Fire;
            case "Air":
            default:
                return Element.Type.Air;
        }
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

        int[] statsInc;//atk,def,hp,atkM,defM,spd
        //base stats up per lvl for each element
        switch (element)
        {
            case Element.Type.Water:
                statsInc = new int[] { 5, 3, 1, 4, 6, 2 };
                break;
            case Element.Type.Fire:
                statsInc = new int[] { 4, 1, 2, 6, 5, 3 };
                break;
            case Element.Type.Earth:
                statsInc = new int[] { 3, 5, 6, 2, 4, 1 };
                break;
            case Element.Type.Air:
                statsInc = new int[] { 3, 3, 3, 3, 3, 6 };
                break;
            default:
                statsInc = new int[] { 6, 6, 6, 6, 6, 6 };
                break;
        }


        //bonus in one of the stats
        Random random = new Random();
        statsInc[random.Next(0, 7)]++;

        //set the stats increments
        setAttack(0, getAttack()[0] + statsInc[0]);
        setAttack(1, getAttack()[1] + statsInc[0]);

        setDefense(0, getDefense()[0] + statsInc[1]);
        setDefense(1, getDefense()[1] + statsInc[1]);

        //just increase the hp if the spirit is not fainted
        if (getHP()[0] > 0)
        {
            setHP(0, getHP()[0] + statsInc[2]);
        }
        setHP(1, getHP()[1] + statsInc[2]);

        setMagicAttack(0, getMagicAttack()[0] + statsInc[3]);
        setDefense(1, getDefense()[1] + statsInc[3]);

        setMagicDefense(0, getMagicDefense()[0] + statsInc[4]);
        setMagicDefense(1, getMagicDefense()[1] + statsInc[4]);

        setSpeed(0, getSpeed()[0] + statsInc[5]);
        setSpeed(1, getSpeed()[1] + statsInc[5]);
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
}
