using System;
using System.Linq;

public class AttacksVault
{

    public static SpiritAttack GetAttack(string name)
    {
        var attack = Attacks.FirstOrDefault(a => a.name == name);

        if (attack != null)
        {
            return attack;
        }
        else
        {
            return Attacks[Attacks.Count - 1];
        }
    }


    public static SpiritAttack GetAttack(String element, bool isPhysical, int power)
    {
        switch (element)
        {
            case "Water":
                if (isPhysical)
                {
                    if (power == 1)
                    {
                        return GetAttack("Water Spray");
                    }
                    else if (power == 2)
                    {
                        return GetAttack("Water Whip");
                    }
                    else
                    {
                        return GetAttack("");
                    }
                }
                else
                {
                    if (power == 1)
                    {
                        return GetAttack("High Tide");
                    }
                    else
                    {
                        return GetAttack("Flood");
                    }
                }
            case "Earth":
                if (isPhysical)
                {
                    if (power == 1)
                    {
                        return GetAttack("Pebble Toss");
                    }
                    else if (power == 2)
                    {
                        return GetAttack("Rock Toss");
                    }
                    else
                    {
                        return GetAttack("Boulder Throw");
                    }
                }
                else
                {
                    if (power == 1)
                    {
                        return GetAttack("Earth Tremor");
                    }
                    else
                    {
                        return GetAttack("Tectonic Shake");
                    }
                }
            case "Fire":
                if (isPhysical)
                {
                    if (power == 1)
                    {
                        return GetAttack("Ember Slap");
                    }
                    else if (power == 2)
                    {
                        return GetAttack("Flame Punch");
                    }
                    else
                    {
                        return GetAttack("Fire Kick");
                    }
                }
                else
                {
                    if (power == 1)
                    {
                        return GetAttack("Flame Breath");
                    }
                    else
                    {
                        return GetAttack("Blaze Bomb");
                    }
                }
            default:
                if (isPhysical)
                {
                    if (power == 1)
                    {
                        return GetAttack("");
                    }
                    else if (power == 2)
                    {
                        return GetAttack("Pressure Punch");
                    }
                    else
                    {
                        return GetAttack("Air Blade");
                    }
                }
                else
                {
                    if (power == 1)
                    {
                        return GetAttack("Wind Ball");
                    }
                    else
                    {
                        return GetAttack("");
                    }
                }
        }

    }


    private static readonly List<SpiritAttack> Attacks = new()
{
    // Water Attacks
    new SpiritAttack { name = "Water Spray", power = 25, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Water },
    new SpiritAttack { name = "Water Whip", power = 50, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Water },
    new SpiritAttack { name = "High Tide", power = 60, stacks = 5, atkAccuracy = 85, isMagical = true, element = Element.Type.Water },
    new SpiritAttack { name = "Flood", power = 125, stacks = 5, atkAccuracy = 75, isMagical = true, element = Element.Type.Water },

    // Earth Attacks
    new SpiritAttack { name = "Pebble Toss", power = 25, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Earth },
    new SpiritAttack { name = "Rock Toss", power = 50, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Earth },
    new SpiritAttack { name = "Boulder Throw", power = 75, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Earth },
    new SpiritAttack { name = "Earth Tremor", power = 60, stacks = 5, atkAccuracy = 85, isMagical = true, element = Element.Type.Earth },
    new SpiritAttack { name = "Tectonic Shake", power = 125, stacks = 5, atkAccuracy = 75, isMagical = true, element = Element.Type.Earth },

    // Fire Attacks
    new SpiritAttack { name = "Ember Slap", power = 25, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Fire },
    new SpiritAttack { name = "Flame Punch", power = 25, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Fire },
    new SpiritAttack { name = "Fire Kick", power = 25, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Fire },
    new SpiritAttack { name = "Flame Breath", power = 60, stacks = 5, atkAccuracy = 85, isMagical = true, element = Element.Type.Fire },
    new SpiritAttack { name = "Blaze Bomb", power = 125, stacks = 5, atkAccuracy = 75, isMagical = true, element = Element.Type.Fire },

    // Air Attacks
    new SpiritAttack { name = "Pressure Punch", power = 50, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Air },
    new SpiritAttack { name = "Air Blade", power = 75, stacks = 5, atkAccuracy = 100, isMagical = false, element = Element.Type.Air },
    new SpiritAttack { name = "Wind Ball", power = 60, stacks = 5, atkAccuracy = 85, isMagical = true, element = Element.Type.Air },

    // Default/Fallback Attack
    new SpiritAttack { name = "Soul Attack", power = 15, stacks = 5, atkAccuracy = 125, isMagical = true, element = Element.Type.Air }
};
}

