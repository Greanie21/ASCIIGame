
public class InventoryManager
{

    public static void UseInventory(Person person, Spirit foe, int hudSize, bool isNpc)
    {
        int choice;
        Random randomNumber = new Random();

        do
        {
            InventoryManager.PrintInventory(person, hudSize);

            if (isNpc)
            {
                choice = randomNumber.Next(1, 7);
            }
            else
            {
                choice = Util.ReadUserNextKey();
            }

        } while (InventoryManager.InventoryChoiceIsValid(choice, person, foe, isNpc) == false);

        UseItem(person, choice, foe);
    }

    private static void PrintInventory(Person person, int hudSize)
    {
        Util.ClearBoard();

        String firstLine = " ";
        while (firstLine.Length < hudSize)
        {
            firstLine += "_";
        }
        firstLine += " ";
        Console.WriteLine(firstLine);

        String line2 = "| 1 - Soulstone: X ";
        line2 += person.soulStones.ToString();
        while (line2.Length < hudSize)
        {
            line2 += " ";
        }
        line2 += "|";
        Console.WriteLine(line2);

        String line3 = "| 2 - Lesser Hp Restore: X ";
        line3 += person.lesserHpRestores;
        while (line3.Length < hudSize)
        {
            line3 += " ";
        }
        line3 += "|";
        Console.WriteLine(line3);

        String line4 = "| 3 - Hp Restore: X ";
        line4 += person.hpRestores;
        while (line4.Length < hudSize)
        {
            line4 += " ";
        }
        line4 += "|";
        Console.WriteLine(line4);

        String line5 = "| 4 - Greater Hp Restore: X ";
        line5 += person.greaterHpRestores;
        while (line5.Length < hudSize)
        {
            line5 += " ";
        }
        line5 += "|";
        Console.WriteLine(line5);

        String line6 = "| 5 - Ultimate Hp Restore: X ";
        line6 += person.ultimateHpRestores;
        while (line6.Length < hudSize)
        {
            line6 += " ";
        }
        line6 += "|";
        Console.WriteLine(line6);

        String line7 = "| 6 - Cancel";
        while (line7.Length < hudSize)
        {
            line7 += " ";
        }
        line7 += "|";
        Console.WriteLine(line7);

        String lastLine = "|";
        while (lastLine.Length < hudSize)
        {
            lastLine += "_";
        }
        lastLine += "|";
        Console.WriteLine(lastLine);
    }

    private static bool InventoryChoiceIsValid(int choice, Person person, Spirit foe, bool isNpc)
    {
        switch (choice)
        {
            case 1:
                if (person.soulStones > 0)
                {
                    if (foe == null)
                    {
                        PrintFail("You can only use this in battles", isNpc);
                        return false;
                    }
                    else if (foe.wild == false)
                    {
                        PrintFail("You can only capture Wild Spirits", isNpc);
                        return false;
                    }
                    bool lowLevel = foe.getLevel() > person.spirits[0].getLevel();
                    bool lowHp = foe.getHP()[0] <= (foe.getHP()[1] / 2);
                    bool itIsWeaker = (lowLevel || lowHp);

                    if (itIsWeaker && person.GetSpiritsCount() < 6)
                    {
                        return true;
                    }
                    PrintFail("You need to weaken the Wild Spirit before you can capture it", isNpc);
                }
                PrintFail("Not enough in bag", isNpc);
                return false;
            case 2:
                if (person.lesserHpRestores > 0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag", isNpc);
                    return false;
                }
            case 3:
                if (person.hpRestores > 0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag", isNpc);
                    return false;
                }
            case 4:
                if (person.greaterHpRestores > 0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag", isNpc);
                    return false;
                }
            case 5:
                if (person.ultimateHpRestores > 0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag", isNpc);
                    return false;
                }
            case 6:
                return true;
            default:
                PrintFail("Please select an valid option", isNpc);
                return false;

        }
    }

    private static void UseItem(Person person, int choice, Spirit foe)
    {
        int restoreQuantity;
        int hpCurrent = person.spirits[0].getHP()[0];
        int hpMax = person.spirits[0].getHP()[1];
        switch (choice)
        {
            case 1:
                person.soulStones--;
                int newSpiritPosition = person.GetSpiritsCount();
                foe.setHP(0, 0);
                foe.wild = false;
                person.spirits[newSpiritPosition] = foe;
                break;
            case 2                :
                person.lesserHpRestores--;
                restoreQuantity = 100;
                person.spirits[0].setHP(0, hpCurrent + restoreQuantity);
                if (hpCurrent > hpMax)
                {
                    person.spirits[0].setHP(0, hpMax);
                }
                break;
            case 3:
                person.hpRestores--;
                restoreQuantity = 250;
                person.spirits[0].setHP(0, hpCurrent + restoreQuantity);
                if (hpCurrent > hpMax)
                {
                    person.spirits[0].setHP(0, hpMax);
                }
                break;
            case 4:
                person.greaterHpRestores--;
                restoreQuantity = 500;
                person.spirits[0].setHP(0, hpCurrent + restoreQuantity);
                if (hpCurrent > hpMax)
                {
                    person.spirits[0].setHP(0, hpMax);
                }
                break;
            case 5:
                person.ultimateHpRestores--;
                person.spirits[0].setHP(0, hpMax);
                break;
        }
    }

    private static void PrintFail(String message, bool isNpc)
    {
        if (isNpc == false)
        {
            Util.PrintAndWait(message);
        }
    }
}
