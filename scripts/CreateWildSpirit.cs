public class CreateWildSpirit
{
    //stage=1...10
    public static Person WildEncounter(int stage)
    {
        int lvl;
        if (stage != 0)
        {
            lvl = (stage - 1) * 5;
            lvl += new Random().Next(1, 6);
        }
        else
        {
            lvl = 1;
        }

        int elementNr = new Random().Next(1, 3);
        Element.Type element = GetElement(elementNr);
        String name = GetName(elementNr, stage);

        SpiritAttack[] atks = GetAttacks(stage, element);

        Spirit spirit = new Spirit(1, 0, 100, 100, 50, 50, 50, 50, 10, 100, 100, name, element, Archtype.Type.Balanced, atks);

        while (spirit.getLevel() < lvl)
        {
            spirit.LevelUp();
        }

        spirit.wild = true;

        Person p = new Person();
        p.spirits[0] = spirit;
        return p;
    }

    private static Element.Type GetElement(int element)
    {
        switch (element)
        {
            case 1:
                return Element.Type.Water;
            case 2:
                return Element.Type.Earth;
            case 3:
                return Element.Type.Fire;
            case 4:
            default:
                return Element.Type.Air;
        }
    }

    private static String GetName(int element, int stage)
    {
        String name = "";
        switch (element)
        {
            case 1:
                if (stage <= 3)
                {
                    name = "Naiad";
                }
                else if (stage <= 7)
                {
                    name = "Nereid";
                }
                else
                {
                    name = "Hydra";
                }
                break;
            case 2:
                if (stage <= 3)
                {
                    name = "Gnome";
                }
                else if (stage <= 7)
                {
                    name = "Oread";
                }
                else
                {
                    name = "Golem";
                }
                break;
            case 3:
                if (stage <= 3)
                {
                    name = "Salamander";
                }
                else if (stage <= 7)
                {
                    name = "Hellhound";
                }
                else
                {
                    name = "Phoenix";
                }
                break;
            case 4:
                if (stage <= 3)
                {
                    name = "Sylph";
                }
                else if (stage <= 7)
                {
                    name = "Harpy";
                }
                else
                {
                    name = "Roc";
                }
                break;
        }
        return name;
    }

    private static SpiritAttack[] GetAttacks(int stage, Element.Type element)
    {
        SpiritAttack[] atks = new SpiritAttack[4];

        switch (stage)
        {
            case 0:
            case 1:
                atks[0] = AttacksVault.GetAttack(element, true, 1);
                atks[1] = AttacksVault.GetAttack("Soul Attack");
                atks[2] = AttacksVault.GetAttack("Soul Attack");
                atks[3] = AttacksVault.GetAttack("Soul Attack");
                break;
            case 2:
                atks[0] = AttacksVault.GetAttack(element, true, 1);
                atks[1] = AttacksVault.GetAttack("Soul Attack");
                atks[2] = AttacksVault.GetAttack(element, false, 1);
                atks[3] = AttacksVault.GetAttack("Soul Attack");
                break;
            case 3:
                atks[0] = AttacksVault.GetAttack(element, true, 1);
                atks[1] = AttacksVault.GetAttack(element, true, 1);
                atks[2] = AttacksVault.GetAttack(element, false, 1);
                atks[3] = AttacksVault.GetAttack("Soul Attack");
                break;
            case 4:
                atks[0] = AttacksVault.GetAttack(element, true, 1);
                atks[1] = AttacksVault.GetAttack(element, true, 1);
                atks[2] = AttacksVault.GetAttack(element, false, 1);
                atks[3] = AttacksVault.GetAttack(element, false, 1);
                break;
            case 5:
                atks[0] = AttacksVault.GetAttack(element, true, 2);
                atks[1] = AttacksVault.GetAttack(element, true, 1);
                atks[2] = AttacksVault.GetAttack(element, false, 1);
                atks[3] = AttacksVault.GetAttack(element, false, 1);
                break;
            case 6:
                atks[0] = AttacksVault.GetAttack(element, true, 2);
                atks[1] = AttacksVault.GetAttack(element, true, 1);
                atks[2] = AttacksVault.GetAttack(element, false, 2);
                atks[3] = AttacksVault.GetAttack(element, false, 1);
                break;
            case 7:
                atks[0] = AttacksVault.GetAttack(element, true, 3);
                atks[1] = AttacksVault.GetAttack(element, true, 1);
                atks[2] = AttacksVault.GetAttack(element, false, 2);
                atks[3] = AttacksVault.GetAttack(element, false, 1);
                break;
            case 8:
                atks[0] = AttacksVault.GetAttack(element, true, 3);
                atks[1] = AttacksVault.GetAttack(element, true, 2);
                atks[2] = AttacksVault.GetAttack(element, false, 2);
                atks[3] = AttacksVault.GetAttack(element, false, 1);
                break;
            case 9:
                atks[0] = AttacksVault.GetAttack(element, true, 3);
                atks[1] = AttacksVault.GetAttack(element, true, 2);
                atks[2] = AttacksVault.GetAttack(element, false, 2);
                atks[3] = AttacksVault.GetAttack(element, false, 2);
                break;
            default:
                atks[0] = AttacksVault.GetAttack(element, true, 3);
                atks[1] = AttacksVault.GetAttack(element, true, 3);
                atks[2] = AttacksVault.GetAttack(element, false, 2);
                atks[3] = AttacksVault.GetAttack(element, false, 2);
                break;
        }

        return atks;
    }
}
