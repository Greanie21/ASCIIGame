public class FightClass
{
    Person player;
    Person enemy;
    int speedToAct = 1000;
    int boxHudSize = 40;
    bool isPlayerFighting = true;
    bool runnedAway = false;
    Random randomNumber = new Random();

    //Scanner scan = new Scanner(System.in);

    public bool Fight(Person user, Person enemy, bool isPlayerFighting)
    {
        this.player = user;
        this.enemy = enemy;
        this.isPlayerFighting = isPlayerFighting;

        bool playerLost = PersonLost(player);
        bool enemyLost = PersonLost(enemy);

        if (this.isPlayerFighting)
        {
            PrintInitialHud();
        }

        while (playerLost == false && enemyLost == false && runnedAway == false)
        {
            if (CheckIfNeedToChangeSpirits(enemy))
            {
                ChangeNpcSpirit(enemy);
            }
            if (CheckIfNeedToChangeSpirits(player))
            {
                if (isPlayerFighting)
                {
                    ChangePlayerSpirit(false);
                }
                else
                {
                    ChangeNpcSpirit(player);
                }
            }

            if (NextTurnIsPlayers())
            {
                if (isPlayerFighting)
                {
                    PlayerTurn();
                }
                else
                {
                    NpcTurn("player");
                }
            }
            else
            {
                NpcTurn("enemy");
            }

            playerLost = PersonLost(player);
            enemyLost = PersonLost(enemy);
        }

        ResetStacks(player);
        ResetStacks(enemy);

        if (playerLost)
        {
            if (isPlayerFighting)
            {
                player.mapName = "Pewter";
                player.mapPosition[0] = 26;
                player.mapPosition[1] = 19;
                Bed.Sleep(player, isPlayerFighting);
            }
            return false;
        }
        return true;
    }

    //Check if the person lost the battle
    private bool PersonLost(Person person)
    {
        for (int i = 0; i < person.GetSpiritsCount(); i++)
        {
            try
            {
                if (person.spirits[i].getHP()[0] > 0)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }
        return true;
    }

    private bool CheckIfNeedToChangeSpirits(Person person)
    {
        if (person.spirits[0].getHP()[0] <= 0)
        {
            return true;
        }
        return false;
    }

    //pass the time while neither player nor enemy can act. 
    //returns TRUE if it is Player's turn and FALSE if it is Enemy's turn 
    private bool NextTurnIsPlayers()
    {
        bool playerCanAct = false;
        bool enemyCanAct = false;
        while (playerCanAct == false && enemyCanAct == false)
        {
            TimePasses();
            playerCanAct = player.spirits[0].getSpeed()[0] > speedToAct;
            enemyCanAct = enemy.spirits[0].getSpeed()[0] > speedToAct;
        }

        if (playerCanAct)
        {
            player.spirits[0].getSpeed()[0] = player.spirits[0].getSpeed()[1];
            return true;
        }
        else if (enemyCanAct)
        {
            enemy.spirits[0].getSpeed()[0] = enemy.spirits[0].getSpeed()[1];
            return false;
        }

        //throw new Exception("Logic error in the 'IsUserTurn' Method");
        return true;
    }

    private void TimePasses()
    {
        player.spirits[0].getSpeed()[0]++;
        enemy.spirits[0].getSpeed()[0]++;

        //check if they have statues 
        //for exemple: if they are burning reduce their HP
    }

    //Player region
    private void PlayerTurn()
    {
        bool alreadyActed = false;

        while (alreadyActed == false)
        {
            Util.ClearBoard();

            PrintMainCombatHud();

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    alreadyActed = CheckIfPlayerMadeAnAttack();
                    break;
                case '2':
                    ChangePlayerSpirit(true);
                    alreadyActed = true;
                    break;
                case '3':
                    InventoryManager.UseInventory(player, enemy.spirits[0], boxHudSize, false);
                    alreadyActed = true;
                    break;
                default:
                    TryToRun(player, enemy.spirits[0]);
                    alreadyActed = true;
                    break;
            }
        }
    }

    private void PrintInitialHud()
    {
        Util.ClearBoard();

        PrintInfCombatHud();

        String secondLine = "| ";
        String thirdLine = "| ";
        if (enemy.spirits[0].wild)
        {
            secondLine += "An wild ";
            secondLine += enemy.spirits[0].name;
            secondLine += " appeared!";
        }
        else
        {
            secondLine += "Foe ";
            secondLine += enemy.name;
            secondLine += " challanged you to an battle!";


            thirdLine += enemy.name;
            thirdLine += " sent out ";
            thirdLine += enemy.spirits[0].name;
        }

        while (secondLine.Length < boxHudSize)
        {
            secondLine += " ";
        }
        secondLine += "|";

        while (thirdLine.Length < boxHudSize)
        {
            thirdLine += " ";
        }
        thirdLine += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(secondLine);
        Console.WriteLine(thirdLine);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetLastLine());

        Console.Write("");
        Console.Write("Continue...");

        if (isPlayerFighting)
        {
            Console.ReadLine();
        }
    }

    private void PrintInfCombatHud()
    {
        String line2 = " ";
        while (line2.Length < boxHudSize / 2 - 2)
        {
            line2 += "_";
        }
        line2 += "     ";
        while (line2.Length < boxHudSize)
        {
            line2 += "_";
        }
        line2 += " ";

        String eLvl = "|Lvl:" + enemy.spirits[0].getLevel();
        while (eLvl.Length < boxHudSize / 2 - 2)
        {
            eLvl += " ";
        }
        eLvl += "|";

        String enemyHp = "|HP:" + enemy.spirits[0].getHP()[0];
        enemyHp += "/" + enemy.spirits[0].getHP()[1];
        while (enemyHp.Length < boxHudSize / 2 - 2)
        {
            enemyHp += " ";
        }
        enemyHp += "|";

        String enemyEleme = "|Element:";
        enemyEleme += enemy.spirits[0].element;
        while (enemyEleme.Length < boxHudSize / 2 - 2)
        {
            enemyEleme += " ";
        }
        enemyEleme += "|";

        String pLvl = "|Lvl:" + player.spirits[0].getLevel();
        while (pLvl.Length < boxHudSize / 2 - 2)
        {
            pLvl += " ";
        }
        pLvl += "|";

        String playeHp = "|HP:" + player.spirits[0].getHP()[0];
        playeHp += "/" + player.spirits[0].getHP()[1];
        while (playeHp.Length < boxHudSize / 2 - 2)
        {
            playeHp += " ";
        }
        playeHp += "|";

        String playerElem = "|Element:";
        playerElem += player.spirits[0].element;
        while (playerElem.Length < boxHudSize / 2 - 2)
        {
            playerElem += " ";
        }
        playerElem += "|";

        String line6 = "|";
        while (line6.Length < boxHudSize / 2 - 2)
        {
            line6 += "_";
        }
        line6 += "|   |";
        while (line6.Length < boxHudSize)
        {
            line6 += "_";
        }
        line6 += "|";

        String line7 = " ";
        while (line7.Length < boxHudSize)
        {
            line7 += " ";
        }
        line7 += " ";


        Console.WriteLine("    Enemy:               You:    ");
        Console.WriteLine(line2);
        Console.WriteLine(eLvl + "   " + pLvl);
        Console.WriteLine(enemyEleme + "   " + playerElem);
        Console.WriteLine(enemyHp + "   " + playeHp);
        Console.WriteLine(line6);
        Console.WriteLine(line7);
    }

    private void PrintMainCombatHud()
    {
        PrintInfCombatHud();

        String line2 = "| 1 - Fight";
        while (line2.Length < boxHudSize)
        {
            line2 += " ";
        }
        line2 += "|";

        String line3 = "| 2 - Change Spirits";
        while (line3.Length < boxHudSize)
        {
            line3 += " ";
        }
        line3 += "|";

        String line4 = "| 3 - Item";
        while (line4.Length < boxHudSize)
        {
            line4 += " ";
        }
        line4 += "|";

        String line5 = "| 4 - Run";
        while (line5.Length < boxHudSize)
        {
            line5 += " ";
        }
        line5 += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(line2);
        Console.WriteLine(line3);
        Console.WriteLine(line4);
        Console.WriteLine(line5);
        Console.WriteLine(GetLastLine());

        //         Console.WriteLine(" _______________________________ ");
        //         Console.WriteLine("|             |                 |");
        //         Console.WriteLine("|             |1-Fight 2-Spirits|");
        //         Console.WriteLine("|             |                 |");
        //         Console.WriteLine("|             |3-Item  4-Run    |");
        //         Console.WriteLine("|_____________|_________________|");

        Console.Write("");
        Console.Write("Continue...");

    }

    private void PrintAtkChoiceHud()
    {
        PrintInfCombatHud();

        String[] atks = new String[4];
        for (int i = 0; i < atks.Length; i++)
        {
            int atkNumberPrint = i + 1;
            atks[i] = "| " + atkNumberPrint + "-";
            atks[i] += player.spirits[0].attacks[i].name;
            while (atks[i].Length < boxHudSize - 21)
            {
                atks[i] += " ";
            }

            atks[i] += "|Pw:" + player.spirits[0].attacks[i].power;
            while (atks[i].Length < boxHudSize - 14)
            {
                atks[i] += " ";
            }

            atks[i] += "Acc:" + player.spirits[0].attacks[i].atkAccuracy;
            while (atks[i].Length < boxHudSize - 6)
            {
                atks[i] += " ";
            }

            atks[i] += "Stk:" + player.spirits[0].attacks[i].stacks;
            while (atks[i].Length < boxHudSize)
            {
                atks[i] += " ";
            }
            atks[i] += "|";
        }

        String lastLine = "|";
        while (lastLine.Length < boxHudSize - 21)
        {
            lastLine += "_";
        }
        lastLine += "|";
        while (lastLine.Length < boxHudSize)
        {
            lastLine += "_";
        }
        lastLine += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(atks[0]);
        Console.WriteLine(atks[1]);
        Console.WriteLine(atks[2]);
        Console.WriteLine(atks[3]);
        Console.WriteLine(lastLine);
    }

    private bool CheckIfPlayerMadeAnAttack()
    {
        Util.ClearBoard();
        PrintAtkChoiceHud();

        char atkChoiceChar = Console.ReadKey().KeyChar;

        if (atkChoiceChar != '1' && atkChoiceChar != '2' && atkChoiceChar != '3' && atkChoiceChar != '4')
        {
            return false;
        }
        int atkChoice = int.Parse(atkChoiceChar.ToString());

        //to get the atk number to the array position
        atkChoice--;

        Util.ClearBoard();

        ResolveAttack(player, enemy.spirits[0], atkChoice);

        return true;
    }

    private void ChangePlayerSpirit(bool canCancel)
    {
        do
        {
            PrintChangeSpiritOptions();

            try
            {
                int choice = Util.ReadUserNextKey() - 1;
                bool validNumber = choice < player.GetSpiritsCount();
                if (choice >= 0 && validNumber)
                {
                    if (player.spirits[choice].getHP()[0] > 0)
                    {
                        Spirit temp = player.spirits[0];
                        player.spirits[0] = player.spirits[choice];
                        player.spirits[choice] = temp;
                        canCancel = true;
                    }
                }
                else
                {
                    //could be a missclick
                    canCancel = false;
                }
            }
            catch (Exception e)
            {

            }
        } while (canCancel == false);
    }

  


    private void PrintChangeSpiritOptions()
    {
        Util.ClearBoard();
        Console.WriteLine("       Choose a new spirit       ");

        Console.WriteLine(GetFirstLine());

        int count;
        for (count = 0; count < player.GetSpiritsCount(); count++)
        {
            String line = "|";

            int spiritIndex = count + 1;
            line += spiritIndex;
            line += "-";
            line += player.spirits[count].name;

            line += " - ";
            line += player.spirits[count].element;

            line += " - HP:";
            line += player.spirits[count].getHP()[0];
            line += "/";
            line += player.spirits[count].getHP()[1];

            while (line.Length < boxHudSize)
            {
                line += " ";
            }
            line += "|";
            Console.WriteLine(line);
        }

        //make sure the box always have 6 lines
        for (int i = count; i < 6; i++)
        {
            Console.WriteLine(GetEmptyLine());
        }

        Console.WriteLine(GetLastLine());
    }

    private void PrintAttackNameResultHud(Person attackerPerson, int atkNumber)
    {
        Spirit attacker = attackerPerson.spirits[0];
        String attackerNameUsedAttackName = "| ";
        if (attackerPerson == player)
        {
            attackerNameUsedAttackName += "Your ";
        }
        else if (attackerPerson.spirits[0].wild == false)
        {
            attackerNameUsedAttackName += "Foe's ";
        }
        else
        {
            attackerNameUsedAttackName += "Wild ";
        }

        attackerNameUsedAttackName += attacker.name;
        attackerNameUsedAttackName += " used ";
        attackerNameUsedAttackName += attacker.attacks[atkNumber].name;
        attackerNameUsedAttackName += "!";

        while (attackerNameUsedAttackName.Length < boxHudSize)
        {
            attackerNameUsedAttackName += " ";
        }
        attackerNameUsedAttackName += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(attackerNameUsedAttackName);
    }

    private void PrintAttackDamageResultHud(float effective, Spirit defender, float damage, Person attackerPerson)
    {
        String lineEffective = "| ";
        if (effective == 0.5f)
        {
            lineEffective += "It was not very effective.";
        }
        else if (effective == 1.0f)
        {
            lineEffective += "It was effective.";
        }
        else
        {
            lineEffective += "It was super effective.";
        }
        while (lineEffective.Length < boxHudSize)
        {
            lineEffective += " ";
        }
        lineEffective += "|";
        Console.WriteLine(lineEffective);

        String line;
        if (attackerPerson == player)
        {
            line = "| The enemy ";
            line += defender.name;
        }
        else
        {
            line = "| Your ";
            line += defender.name;
        }
        while (line.Length < boxHudSize)
        {
            line += " ";
        }
        line += "|";

        String secondLine = "| suffered ";
        secondLine += (int)damage;
        secondLine += " damage!";
        while (secondLine.Length < boxHudSize)
        {
            secondLine += " ";
        }
        secondLine += "|";

        Console.WriteLine(line);
        Console.WriteLine(secondLine);
        Console.WriteLine(GetLastLine());

        if (isPlayerFighting)
        {
            Console.Write("Continue...");
            Console.ReadLine();
        }
    }

    private void PrintSpiritFaintedHud(Spirit defender, Person attackerPerson)
    {
        PrintInfCombatHud();

        String line = "| ";

        if (attackerPerson != player)
        {
            line += "Your ";
        }
        else if (defender.wild == false)
        {
            line += "Foe's ";
        }
        else
        {
            line += "Wild ";
        }

        line += defender.name;
        line += " fainted.";

        while (line.Length < boxHudSize)
        {
            line += " ";
        }
        line += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(line);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetLastLine());

        if (isPlayerFighting)
        {
            Console.ReadLine();
        }
    }

    private void PrintXpWonHud(int xp)
    {
        Util.ClearBoard();

        PrintInfCombatHud();

        String line = "| Your ";
        line += player.spirits[0].name;
        line += " won";
        while (line.Length < boxHudSize)
        {
            line += " ";
        }
        line += "|";

        String line2 = "| ";
        line2 += xp;
        line2 += " experience points!";
        while (line2.Length < boxHudSize)
        {
            line2 += " ";
        }
        line2 += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(line);
        Console.WriteLine(line2);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetLastLine());

        if (isPlayerFighting)
        {
            Console.ReadLine();
        }
    }

    private void PrintLvlUp()
    {
        Util.ClearBoard();

        PrintInfCombatHud();

        String line = "| Your ";
        line += player.spirits[0].name;
        line += " leveled up!";
        while (line.Length < boxHudSize)
        {
            line += " ";
        }
        line += "|";

        String line2 = "| It is now level ";
        line2 += player.spirits[0].getLevel();
        line2 += ".";
        while (line2.Length < boxHudSize)
        {
            line2 += " ";
        }
        line2 += "|";

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(line);
        Console.WriteLine(line2);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetLastLine());

        Console.ReadLine();
    }

    private void PrintBattleOver(Person attackerPerson)
    {
        String line = "| ";
        bool battleOver;

        if (attackerPerson == player && enemy.spirits[0].wild == false)
        {
            line += enemy.name;
            battleOver = PersonLost(enemy);
        }
        else
        {
            line += player.name;
            battleOver = PersonLost(player);
        }

        if (battleOver)
        {
            line += " lost the battle!";

            while (line.Length < boxHudSize)
            {
                line += " ";
            }
            line += "|";

            Util.ClearBoard();

            PrintInfCombatHud();

            Console.WriteLine(GetFirstLine());
            Console.WriteLine(GetEmptyLine());
            Console.WriteLine(GetEmptyLine());
            Console.WriteLine(line);
            Console.WriteLine(GetEmptyLine());
            Console.WriteLine(GetLastLine());

            if (isPlayerFighting)
            {
                Console.ReadLine();
            }
        }
    }

    private void PrintTryToRunAway(Person runner, bool wasAbleToRunAway)
    {
        String start = "|";

        if (runner == player)
        {
            start += " You";
        }
        else if (runner.spirits[0].wild)
        {
            start += " Wild ";
            start += runner.spirits[0].name;
        }
        else
        {
            start += " Foe";
        }

        String line = start;
        line += " tried to run away!";

        while (line.Length < boxHudSize)
        {
            line += " ";
        }
        line += "|";

        String line2 = start;
        if (wasAbleToRunAway)
        {
            line2 += " got away safely!";
        }
        else
        {
            line2 += " can't escape!";
        }

        while (line.Length < boxHudSize)
        {
            line2 += " ";
        }
        line2 += "|";

        Util.ClearBoard();

        PrintInfCombatHud();

        Console.WriteLine(GetFirstLine());
        Console.WriteLine(line);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(line2);
        Console.WriteLine(GetEmptyLine());
        Console.WriteLine(GetLastLine());

        Console.ReadLine();
    }

    private String GetFirstLine()
    {
        String firstLine = " ";
        while (firstLine.Length < boxHudSize)
        {
            firstLine += "_";
        }
        firstLine += " ";
        return firstLine;
    }

    private String GetEmptyLine()
    {
        String emptyLines = "|";
        while (emptyLines.Length < boxHudSize)
        {
            emptyLines += " ";
        }
        emptyLines += "|";
        return emptyLines;
    }

    private String GetLastLine()
    {
        String lastLine = "|";
        while (lastLine.Length < boxHudSize)
        {
            lastLine += "_";
        }
        lastLine += "|";
        return lastLine;
    }

    //NPC Region
    private void NpcTurn(String attackerName)
    {
        Util.ClearBoard();

        Person me;
        Spirit foe;
        if (attackerName == "player")
        {
            me = player;
            foe = enemy.spirits[0];
        }
        else
        {
            me = enemy;
            foe = player.spirits[0];
        }
        int whatGonnaDoInItsTurn = randomNumber.Next(1, 101);
        if (whatGonnaDoInItsTurn <= 85)
        {
            int atkChoice = randomNumber.Next(0, 4);
            ResolveAttack(me, foe, atkChoice);
        }
        else if (whatGonnaDoInItsTurn <= 90)
        {
            ChangeNpcSpirit(me);
        }
        else if (whatGonnaDoInItsTurn <= 95)
        {
            InventoryManager.UseInventory(me, foe, boxHudSize, true);
        }
        else if (foe.wild)
        {
            TryToRun(me, foe);
        }
        else
        {
            //redo
            NpcTurn(attackerName);
        }
    }

    private void ChangeNpcSpirit(Person person)
    {
        for (int i = 1; i < person.GetSpiritsCount(); i++)
        {
            if (person.spirits[i].getHP()[0] > 0)
            {
                Spirit temp = person.spirits[i];
                person.spirits[i] = person.spirits[0];
                person.spirits[0] = temp;
            }
        }
    }

    //Both Person's Region
    private void ResolveAttack(Person personAttacker, Spirit defender, int atkNumber)
    {
        Spirit attacker = personAttacker.spirits[0];

        //Spirit accuracy * atk accuracy
        int chanceToHit = attacker.accuracy[0];
        chanceToHit *= attacker.attacks[atkNumber].atkAccuracy;

        //Spirit Dodge * base of atk accuracy (to see the diference)
        int chanceToDodge = defender.dodge[0];
        chanceToDodge *= 100;

        int difference = chanceToHit - chanceToDodge;
        difference /= 100;

        int random = randomNumber.Next(1, 101);

        //if diference is negative you can miss the atk
        if (random + difference > 0)//atk hit
        {
            DealDamage(personAttacker, atkNumber, defender);
        }
        else//atk missed
        {
            PrintInfCombatHud();
            Console.WriteLine("Attack missed!");
        }
    }

    private void DealDamage(Person attackerPerson, int atkNumber, Spirit defender)
    {
        Spirit attacker = attackerPerson.spirits[0];
        SpiritAttack spiritAtk = attacker.attacks[atkNumber];
        float damage = spiritAtk.power / 25.0f;
        damage *= spiritAtk.stacks;

        if (spiritAtk.isMagical)
        {
            damage *= attacker.getMagicAttack()[0];
            damage /= defender.getMagicDefense()[0];
        }
        else
        {
            damage *= attacker.getAttack()[0];
            damage /= defender.getDefense()[0];
        }

        //randomize a little bit of the damage (* from 0.9 to 1.1)
        float damageRandomizer = randomNumber.Next(90, 111) / 100.0f;
        damage *= damageRandomizer;

        float resAndWeak = CheckResistenceAndWeaknesses(spiritAtk, defender);
        damage *= resAndWeak;

        int damageRound = (int)Math.Round(damage);
        if (damageRound < 1)
        {
            damageRound = 1;
        }

        //deal the damage
        defender.getHP()[0] -= damageRound;

        rechargeAttackStack(attacker, atkNumber);

        PrintInfCombatHud();
        PrintAttackNameResultHud(attackerPerson, atkNumber);
        PrintAttackDamageResultHud(resAndWeak, defender, damageRound, attackerPerson);

        CheckIfDefensorIsStillAlive(defender, attackerPerson);
    }

    private float CheckResistenceAndWeaknesses(SpiritAttack attack, Spirit defender)
    {
        switch (attack.element)
        {
            case Element.Type.Water:
                switch (defender.element)
                {
                    case Element.Type.Water:
                    case Element.Type.Earth:
                        return 0.5f;
                    case Element.Type.Fire:
                        return 2.0f;
                }
                break;
            case Element.Type.Earth:
                switch (defender.element)
                {
                    case Element.Type.Water:
                        return 2.0f;
                    case Element.Type.Earth:
                    case Element.Type.Fire:
                        return 0.5f;
                }
                break;
            case Element.Type.Fire:
                switch (defender.element)
                {
                    case Element.Type.Water:
                    case Element.Type.Fire:
                        return 0.5f;
                    case Element.Type.Earth:
                        return 2.0f;
                }
                break;
            case Element.Type.Air:
                if (defender.element == Element.Type.Air)
                {
                    return 2.0f;
                }
                break;
        }
        return 1;
    }

    private void rechargeAttackStack(Spirit attacker, int atkNumber)
    {
        if (attacker.attacks[atkNumber].stacks > 1)
        {
            attacker.attacks[atkNumber].stacks -= 2;
        }

        for (int i = 0; i < attacker.attacks.Length; i++)
        {
            if (i != atkNumber && attacker.attacks[i].stacks < 5)
            {
                attacker.attacks[i].stacks++;
            }
        }
    }

    private void CheckIfDefensorIsStillAlive(Spirit defender, Person attackerPerson)
    {
        if (defender.getHP()[0] <= 0)
        {
            defender.setHP(0, 0);

            Util.ClearBoard();

            PrintSpiritFaintedHud(defender, attackerPerson);

            AddXp(defender, attackerPerson);

            PrintBattleOver(attackerPerson);
        }
    }

    private void AddXp(Spirit defender, Person attackerPerson)
    {
        Spirit attacker = attackerPerson.spirits[0];

        //Add xp to the spirit that won (100% of the loser lvl)
        int lvl = defender.getLevel();
        int xpToAddWin = (14 * (lvl + 2)) + (((3 * lvl) * (3 * lvl)) / 5);
        attacker.experience += xpToAddWin;

        //Add xp to the spirit that lost (5% of the winner lvl)
        lvl = attacker.getLevel();
        float xpToAddLoss = (14 * (lvl + 2)) + (((3 * lvl) * (3 * lvl)) / 5);
        xpToAddLoss *= 0.05f;
        int xpToAddLossInt = (int)Math.Round(xpToAddLoss);
        defender.experience += xpToAddLossInt;

        if (attackerPerson == player)
        {
            PrintXpWonHud(xpToAddWin);
        }
        else
        {
            PrintXpWonHud(xpToAddLossInt);
        }

        enemy.spirits[0].CheckIfSpiritLeveledUp();

        if (player.spirits[0].CheckIfSpiritLeveledUp() && isPlayerFighting)
        {
            PrintLvlUp();
        }
    }

    private void ResetStacks(Person person)
    {
        for (int i = 0; i < person.GetSpiritsCount(); i++)
        {
            Spirit spirit = person.spirits[i];

            foreach (SpiritAttack attack in spirit.attacks)
            {
                attack.stacks = 5;
            }
        }
    }

    private void TryToRun(Person runner, Spirit enemy)
    {
        float chanceToRun = runner.spirits[0].getLevel();
        chanceToRun *= (randomNumber.Next(1, 11)) / 5.0f;

        if (chanceToRun > enemy.getLevel())
        {
            runnedAway = true;
        }

        if (isPlayerFighting)
        {
            if (runnedAway)
            {
                PrintTryToRunAway(runner, true);
            }
            else
            {
                PrintTryToRunAway(runner, false);
            }
        }
    }

}
