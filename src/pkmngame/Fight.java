/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

import java.util.Scanner;

/**
 *
 * @author abol9
 */
public class Fight 
{
    Person player;
    Person enemy;
    int speedToAct=1000;
    int boxHudSize=40;
    boolean isPlayerFighting=true;
    boolean runnedAway=false;
    
    Scanner scan = new Scanner(System.in);
    
    public boolean Fight(Person user,Person enemy,boolean isPlayerFighting)
    {
        this.player=user;
        this.enemy=enemy;
        this.isPlayerFighting=isPlayerFighting;
        
        boolean playerLost=PersonLost(player);
        boolean enemyLost=PersonLost(enemy);
        
        if(this.isPlayerFighting)
        {
            PrintInitialHud();
        }
        
        while(playerLost==false &&  enemyLost==false && runnedAway==false)
        {
            if(CheckIfNeedToChangeSpirits(enemy))
            {
                ChangeNpcSpirit(enemy);
            }
            if(CheckIfNeedToChangeSpirits(player))
            {
                if(isPlayerFighting)
                {
                    ChangePlayerSpirit(false);
                }
                else
                {
                    ChangeNpcSpirit(player);
                }
            }
            
            if(NextTurnIsPlayers())
            {
                if(isPlayerFighting)
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
            
            playerLost=PersonLost(player);
            enemyLost=PersonLost(enemy);
        }
        
        ResetStacks(player);
        ResetStacks(enemy);
        
        if(playerLost)
        {
            if(isPlayerFighting)
            {
                player.mapName="Pewter";
                player.mapPosition[0]=26;
                player.mapPosition[1]=19;
                Bed.Sleep(player, isPlayerFighting);
            }
            return false;
        }
        return true;
    }
    
    //Check if the person lost the battle
    private boolean PersonLost(Person person)
    {
        for (int i=0;i<person.GetSpiritsCount();i++)
        {
            try
            {
                if(person.spirits[i].getHP()[0]>0)
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return true;
            }
        }
        return true;
    }
    
    private boolean CheckIfNeedToChangeSpirits(Person person)
    {
        if(person.spirits[0].getHP()[0]<=0)
        {
            return true;
        }
        return false;
    }
    
    //pass the time while neither player nor enemy can act. 
    //returns TRUE if it is Player's turn and FALSE if it is Enemy's turn 
    private boolean NextTurnIsPlayers()
    {
        boolean playerCanAct=false;
        boolean enemyCanAct=false;
        while(playerCanAct == false && enemyCanAct == false)
        {
            TimePasses();
            playerCanAct=player.spirits[0].getSpeed()[0]>speedToAct;
            enemyCanAct=enemy.spirits[0].getSpeed()[0]>speedToAct;
        }
        
        if(playerCanAct)
        {
            player.spirits[0].getSpeed()[0]=player.spirits[0].getSpeed()[1];
            return true;
        }
        else if(enemyCanAct)
        {
            enemy.spirits[0].getSpeed()[0]=enemy.spirits[0].getSpeed()[1];
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
        boolean alreadyActed=false;
        
        while (alreadyActed==false)
        {
            Util.ClearBoard();
                
            PrintMainCombatHud();

            switch(scan.next())
            {
                case "1":
                    alreadyActed=CheckIfPlayerMadeAnAttack();
                    break;
                case "2":
                    ChangePlayerSpirit(true);
                    alreadyActed=true;
                    break;
                case "3":
                    InventoryManager.UseInventory(player, enemy.spirits[0], boxHudSize, false);
                    alreadyActed=true;
                    break;
                default:
                    TryToRun(player,enemy.spirits[0]);
                    alreadyActed=true;
                    break;
            }
        }
    }
    
    private void PrintInitialHud()
    {
        Util.ClearBoard();
        
        PrintInfCombatHud();
        
        String secondLine="| ";
        String thirdLine="| ";
        if(enemy.spirits[0].wild)
        {
            secondLine+="An wild ";
            secondLine+=enemy.spirits[0].name;
            secondLine+=" appeared!";
        }
        else
        {
            secondLine+="Foe ";
            secondLine+=enemy.name;
            secondLine+=" challanged you to an battle!";
            
            thirdLine+=enemy.name;
            thirdLine+=" sent out ";
            thirdLine+=enemy.spirits[0].name;
        }
        
        while(secondLine.length()<boxHudSize)
        {
            secondLine+=" ";
        }
        secondLine+="|";
        
        while(thirdLine.length()<boxHudSize)
        {
            thirdLine+=" ";
        }
        thirdLine+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(GetEmptyLine());
        System.out.println(secondLine);
        System.out.println(thirdLine);
        System.out.println(GetEmptyLine());
        System.out.println(GetLastLine());
        
        System.out.print("");
        System.out.print("Continue...");
        
        if(isPlayerFighting)
        {
            scan.next();
        }
    }
    
    private void PrintInfCombatHud()
    { 
        String line2=" ";
        while(line2.length()<boxHudSize/2-2)
        {
            line2+="_";
        }
        line2+="     ";
        while(line2.length()<boxHudSize)
        {
            line2+="_";
        }
        line2+=" ";
        
        String eLvl="|Lvl:"+enemy.spirits[0].getLevel();
        while (eLvl.length()<boxHudSize/2-2)
        {
            eLvl+=" ";
        }
            eLvl+="|";
        
        String enemyHp="|HP:"+enemy.spirits[0].getHP()[0];
        enemyHp+="/"+enemy.spirits[0].getHP()[1];
        while(enemyHp.length()<boxHudSize/2-2)
        {
            enemyHp+=" ";
        }
        enemyHp+="|";
        
        String enemyEleme="|Element:";
        enemyEleme+=enemy.spirits[0].element;
        while(enemyEleme.length()<boxHudSize/2-2)
        {
            enemyEleme+=" ";
        }
        enemyEleme+="|";
            
        String pLvl="|Lvl:"+player.spirits[0].getLevel();
        while (pLvl.length()<boxHudSize/2-2)
        {
            pLvl+=" ";
        }
        pLvl+="|";
        
        String playeHp="|HP:"+player.spirits[0].getHP()[0];
        playeHp+="/"+player.spirits[0].getHP()[1];
        while(playeHp.length()<boxHudSize/2-2)
        {
            playeHp+=" ";
        }
        playeHp+="|";
        
        String playerElem="|Element:";
        playerElem+=player.spirits[0].element;
        while(playerElem.length()<boxHudSize/2-2)
        {
            playerElem+=" ";
        }
        playerElem+="|";
        
        String line6="|";
        while(line6.length()<boxHudSize/2-2)
        {
            line6+="_";
        }
        line6+="|   |";
        while(line6.length()<boxHudSize)
        {
            line6+="_";
        }
        line6+="|";
        
        String line7=" ";
        while(line7.length()<boxHudSize)
        {
            line7+=" ";
        }
        line7+=" ";
        

        System.out.println("    Enemy:               You:    ");
        System.out.println(line2);
        System.out.println(eLvl+"   "+pLvl);
        System.out.println(enemyEleme+"   "+playerElem);
        System.out.println(enemyHp+"   "+playeHp);
        System.out.println(line6);
        System.out.println(line7);
    }
    
    private void PrintMainCombatHud()
    {
        PrintInfCombatHud();
        
        String line2="| 1 - Fight";
        while(line2.length()<boxHudSize)
        {
            line2+=" ";
        }
        line2+="|";
        
        String line3="| 2 - Change Spirits";
        while(line3.length()<boxHudSize)
        {
            line3+=" ";
        }
        line3+="|";
        
        String line4="| 3 - Item";
        while(line4.length()<boxHudSize)
        {
            line4+=" ";
        }
        line4+="|";
        
        String line5="| 4 - Run";
        while(line5.length()<boxHudSize)
        {
            line5+=" ";
        }
        line5+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(line2);
        System.out.println(line3);
        System.out.println(line4);
        System.out.println(line5);
        System.out.println(GetLastLine());
        
//        System.out.println(" _______________________________ ");
//        System.out.println("|             |                 |");
//        System.out.println("|             |1-Fight 2-Spirits|");
//        System.out.println("|             |                 |");
//        System.out.println("|             |3-Item  4-Run    |");
//        System.out.println("|_____________|_________________|");
        
        System.out.print("");
        System.out.print("Continue...");

    }
    
    private void PrintAtkChoiceHud()
    {
        PrintInfCombatHud();
        
        String [] atks=new String[4];
        for (int i = 0; i < atks.length; i++) 
        {
            int atkNumberPrint=i+1;
            atks[i]="| "+atkNumberPrint+"-";
            atks[i]+=player.spirits[0].attacks[i].name;
            while (atks[i].length()<boxHudSize-21)
            {
                atks[i]+=" ";
            }
        
            atks[i]+="|Pw:"+player.spirits[0].attacks[i].power;
            while (atks[i].length()<boxHudSize-14)
            {
                atks[i]+=" ";
            }
        
            atks[i]+="Acc:"+player.spirits[0].attacks[i].atkAccuracy;
            while (atks[i].length()<boxHudSize-6)
            {
                atks[i]+=" ";
            }
        
            atks[i]+="Stk:"+player.spirits[0].attacks[i].stacks;
            while (atks[i].length()<boxHudSize)
            {
                atks[i]+=" ";
            }
            atks[i]+="|";
        }
        
        String lastLine="|";
        while(lastLine.length()<boxHudSize-21)
        {
            lastLine+="_";
        }
        lastLine+="|";
        while(lastLine.length()<boxHudSize)
        {
            lastLine+="_";
        }
        lastLine+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(atks[0]);
        System.out.println(atks[1]);
        System.out.println(atks[2]);
        System.out.println(atks[3]);
        System.out.println(lastLine);
    }
    
    private boolean CheckIfPlayerMadeAnAttack()
    {
        Util.ClearBoard();
        PrintAtkChoiceHud();
        
        int atkChoice=Integer.parseInt(scan.next());
        
        if(atkChoice!=1 && atkChoice!=2 && atkChoice!=3 && atkChoice!=4)
        {
            return false;
        }
        //to get the atk number to the array position
        atkChoice--;
        
        Util.ClearBoard();
        
        ResolveAttack(player,enemy.spirits[0],atkChoice);
        
        return true;
    }
    
    private void ChangePlayerSpirit(boolean canCancel)
    {
        do
        {
            PrintChangeSpiritOptions();
        
            try
            {
                int choice=Integer.parseInt(scan.next())-1;
                boolean validNumber=choice<player.GetSpiritsCount();
                if(choice>=0 && validNumber)
                {
                    if(player.spirits[choice].getHP()[0]>0)
                    {
                        Spirit temp=player.spirits[0];
                        player.spirits[0]=player.spirits[choice];
                        player.spirits[choice]=temp;
                        canCancel=true;
                    }
                }
                else
                {
                    //could be a missclick
                    canCancel=false;
                }
            }
            catch(Exception e)
            {
            
            }
        } while(canCancel==false);
    }
    
    private void PrintChangeSpiritOptions()
    {
        Util.ClearBoard();
        System.out.println("       Choose a new spirit       ");
        
        System.out.println(GetFirstLine());
        
            int count;
            for (count=0; count < player.GetSpiritsCount(); count++) 
            {
                String line="|";
                
                int spiritIndex=count+1;
                line+=spiritIndex;
                line+="-";
                line+=player.spirits[count].name;
                
                line+=" - ";
                line+=player.spirits[count].element;
                
                line+=" - HP:";
                line+=player.spirits[count].getHP()[0];
                line+="/";
                line+=player.spirits[count].getHP()[1];
                
                while (line.length()<boxHudSize)
                {
                    line+=" ";
                }
                    line+="|";
                System.out.println(line);
            }
            
        //make sure the box always have 6 lines
        for (int i = count; i < 6; i++) 
        {
            System.out.println(GetEmptyLine());
        }
        
        System.out.println(GetLastLine());
    }
    
    private void PrintAttackNameResultHud(Person attackerPerson,int atkNumber)
    {
        Spirit attacker=attackerPerson.spirits[0];
        String attackerNameUsedAttackName="| ";
        if(attackerPerson==player)
        {
            attackerNameUsedAttackName+="Your ";
        }
        else if(attackerPerson.spirits[0].wild==false)
        {
            attackerNameUsedAttackName+="Foe's ";
        }
        else
        {
            attackerNameUsedAttackName+="Wild ";
        }
        
        attackerNameUsedAttackName+=attacker.name;
        attackerNameUsedAttackName+=" used ";
        attackerNameUsedAttackName+=attacker.attacks[atkNumber].name;
        attackerNameUsedAttackName+="!";
        
        while (attackerNameUsedAttackName.length()<boxHudSize)
        {
            attackerNameUsedAttackName+=" ";
        }
        attackerNameUsedAttackName+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(attackerNameUsedAttackName);
    }
    
    private void PrintAttackDamageResultHud(float effective,Spirit defender,float damage,Person attackerPerson)
    {
        String lineEffective="| ";
        if(effective==0.5f)
        {
            lineEffective+="It was not very effective.";
        }
        else if(effective==1.0f)
        {
            lineEffective+="It was effective.";
        }
        else
        {
            lineEffective+="It was super effective.";
        }
        while(lineEffective.length()<boxHudSize)
        {
            lineEffective+=" ";
        }
        lineEffective+="|";
        System.out.println(lineEffective);
        
        String line;
        if(attackerPerson==player)
        {
            line="| The enemy ";
            line+=defender.name;
        }
        else
        {
            line="| Your ";
            line+=defender.name;
        }  
        while (line.length()<boxHudSize)
        {
            line+=" ";
        }
        line+="|";
          
        String secondLine="| suffered ";
        secondLine+=(int)damage;
        secondLine+= " damage!";
        while (secondLine.length()<boxHudSize)
        {
            secondLine+=" ";
        }
        secondLine+="|";
        
        System.out.println(line);
        System.out.println(secondLine);
        System.out.println(GetLastLine());
        
        if(isPlayerFighting)
        {
            System.out.print("Continue...");
            scan.next();
        }
    }
    
    private void PrintSpiritFaintedHud(Spirit defender, Person attackerPerson)
    {
        PrintInfCombatHud();
            
        String line="| ";
            
        if(attackerPerson!=player)
        {
            line+="Your ";
        }
        else if(defender.wild==false)
        {
            line+="Foe's ";
        }
        else
        {
            line+="Wild ";
        }
           
        line+=defender.name;
        line+=" fainted.";
            
        while (line.length()<boxHudSize)
        {
            line+=" ";
        }
        line+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(GetEmptyLine());
        System.out.println(GetEmptyLine());
        System.out.println(line);
        System.out.println(GetEmptyLine());
        System.out.println(GetLastLine());
        
        if(isPlayerFighting)
        {
            scan.next();
        }
    }
    
    private void PrintXpWonHud(int xp)
    {
        Util.ClearBoard();
        
        PrintInfCombatHud();
        
        String line="| Your ";
        line+=player.spirits[0].name;
        line+=" won";
        while(line.length()<boxHudSize)
        {
            line+=" ";
        }
        line+="|";
        
        String line2="| ";
        line2+=xp;
        line2+=" experience points!";
        while(line2.length()<boxHudSize)
        {
            line2+=" ";
        }
        line2+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(GetEmptyLine());
        System.out.println(line);
        System.out.println(line2);
        System.out.println(GetEmptyLine());
        System.out.println(GetLastLine());
        
        if(isPlayerFighting)
        {
            scan.next();
        }
    }
    
    private void PrintLvlUp()
    {
        Util.ClearBoard();
        
        PrintInfCombatHud();
        
        String line="| Your ";
        line+=player.spirits[0].name;
        line+=" leveled up!";
        while(line.length()<boxHudSize)
        {
            line+=" ";
        }
        line+="|";
        
        String line2="| It is now level ";
        line2+=player.spirits[0].getLevel();
        line2+=".";
        while(line2.length()<boxHudSize)
        {
            line2+=" ";
        }
        line2+="|";
        
        System.out.println(GetFirstLine());
        System.out.println(GetEmptyLine());
        System.out.println(line);
        System.out.println(line2);
        System.out.println(GetEmptyLine());
        System.out.println(GetLastLine());
        
        scan.next();
    }
    
    private void PrintBattleOver(Person attackerPerson)
    {
        String line="| ";
        boolean battleOver;
            
        if(attackerPerson==player && enemy.spirits[0].wild==false)
        {
            line+=enemy.name;
            battleOver=PersonLost(enemy);
        }
        else
        {
            line+=player.name;
            battleOver=PersonLost(player);
        }
        
        if(battleOver)
        {
            line+=" lost the battle!";
                
            while(line.length()<boxHudSize)
            {
                line+=" ";
            }
            line+="|";
            
            Util.ClearBoard();
            
            PrintInfCombatHud();
            
            System.out.println(GetFirstLine());
            System.out.println(GetEmptyLine());
            System.out.println(GetEmptyLine());
            System.out.println(line);
            System.out.println(GetEmptyLine());
            System.out.println(GetLastLine());
        
            if(isPlayerFighting)
            {
                scan.next();
            }
        }
    }
    
    private void PrintTryToRunAway(Person runner,boolean wasAbleToRunAway)
    {
        String start="|";
        
        if(runner==player)
        {
            start+=" You";
        }
        else if(runner.spirits[0].wild)
        {
            start+=" Wild ";
            start+=runner.spirits[0].name;
        }
        else
        {
            start+=" Foe";
        }
        
        String line=start;
        line+=" tried to run away!";
                
        while(line.length()<boxHudSize)
        {
            line+=" ";
        }
        line+="|";
        
        String line2=start;
        if(wasAbleToRunAway)
        {
            line2+=" got away safely!";
        }
        else
        {
            line2+=" can't escape!";
        }
          
        while(line.length()<boxHudSize)
        {
            line2+=" ";
        }
        line2+="|";
            
        Util.ClearBoard();
            
        PrintInfCombatHud();
            
        System.out.println(GetFirstLine());
        System.out.println(line);
        System.out.println(GetEmptyLine());
        System.out.println(line2);
        System.out.println(GetEmptyLine());
        System.out.println(GetLastLine());
        
        scan.next();
    }
    
    private String GetFirstLine()
    {
        String firstLine=" ";
        while(firstLine.length()<boxHudSize)
        {
            firstLine+="_";
        }
        firstLine+=" ";
        return firstLine;
    }
    
    private String GetEmptyLine()
    {
        String emptyLines="|";
        while(emptyLines.length()<boxHudSize)
        {
            emptyLines+=" ";
        }
        emptyLines+="|";
        return emptyLines;
    }
    
    private String GetLastLine()
    {
        String lastLine="|";
        while(lastLine.length()<boxHudSize)
        {
            lastLine+="_";
        }
        lastLine+="|";
        return lastLine;
    }
    
    //NPC Region
    private void NpcTurn(String attackerName)
    {
        Util.ClearBoard();
        
        Person me;
        Spirit foe;
        if(attackerName=="player")
        {
            me=player;
            foe=enemy.spirits[0];
        }
        else
        {
            me=enemy;
            foe=player.spirits[0];
        }
        
        int whatGonnaDoInItsTurn=new java.util.Random().nextInt(100)+1;
        if(whatGonnaDoInItsTurn<=85)
        {
            int atkChoice=new java.util.Random().nextInt(4);
            ResolveAttack(me,foe,atkChoice);
        }
        else if(whatGonnaDoInItsTurn<=90)
        {
            ChangeNpcSpirit(me);
        }
        else if (whatGonnaDoInItsTurn<=95) 
        {
            InventoryManager.UseInventory(me, foe, boxHudSize, true);
        }
        else if(foe.wild)
        {
            TryToRun(me,foe);
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
                if(person.spirits[i].getHP()[0]>0)
                {
                   Spirit temp=person.spirits[i];
                   person.spirits[i]=person.spirits[0];
                   person.spirits[0]=temp;
                }
            } 
    }
    
    //Both Person's Region
    private void ResolveAttack(Person personAttacker,Spirit defender,int atkNumber)
    {
        Spirit attacker=personAttacker.spirits[0];
        
        //Spirit accuracy * atk accuracy
        int chanceToHit=attacker.accuracy[0];
        chanceToHit*=attacker.attacks[atkNumber].atkAccuracy;
        
        //Spirit Dodge * base of atk accuracy (to see the diference)
        int chanceToDodge=defender.dodge[0];
        chanceToDodge*=100;
        
        int difference=chanceToHit-chanceToDodge;
        difference/=100;
        
        int random=new java.util.Random().nextInt(100)+1;
        
        //if diference is negative you can miss the atk
        if(random+difference>0)//atk hit
        {
            DealDamage(personAttacker,atkNumber,defender);
        }
        else//atk missed
        {
            PrintInfCombatHud();
            System.out.println("Attack missed!");
        }
    }
    
    private void DealDamage(Person attackerPerson,int atkNumber,Spirit defender)
    {
        Spirit attacker=attackerPerson.spirits[0];
        SpiritAttack spiritAtk=attacker.attacks[atkNumber];
        float damage = spiritAtk.power/25.0f;
        damage*=spiritAtk.stacks;
        
        if(spiritAtk.isMagical)
        {
            damage*=attacker.getMagicAttack()[0];
            damage/=defender.getMagicDefense()[0];
        }
        else
        {
            damage*=attacker.getAttack()[0];
            damage/=defender.getDefense()[0];
        }
        
        //randomize a little bit of the damage (* from 0.9 to 1.1)
        float damageRandomizer=(new java.util.Random().nextInt(20)+90)/100.0f;
        damage*=damageRandomizer;
        
        float resAndWeak=CheckResistenceAndWeaknesses(spiritAtk,defender);
        damage*=resAndWeak;
        
        damage=Math.round(damage);
        if(damage<1)
        {
            damage=1;
        }
        
        //deal the damage
        defender.getHP()[0]-=damage;
        
        rechargeAttackStack(attacker,atkNumber);
        
        PrintInfCombatHud();
        PrintAttackNameResultHud(attackerPerson,atkNumber);
        PrintAttackDamageResultHud(resAndWeak,defender,damage,attackerPerson);
        
        CheckIfDefensorIsStillAlive(defender, attackerPerson);
    }
    
    private float CheckResistenceAndWeaknesses(SpiritAttack attack,Spirit defender)
    {
        switch(attack.element)
        {
            case Water:
                switch(defender.element)
                {
                    case Water:
                    case Earth:
                        return 0.5f;
                    case Fire:
                        return 2.0f;
                }
                break;
            case Earth:
                switch(defender.element)
                {
                    case Water:
                        return 2.0f;
                    case Earth:
                    case Fire:
                        return 0.5f;
                }
                break;
            case Fire:
                switch(defender.element)
                {
                    case Water:
                    case Fire:
                        return 0.5f;
                    case Earth:
                        return 2.0f;
                }
                break;
            case Air:
                if(defender.element==Element.Type.Air)
                {
                    return 2.0f;
                }
                break;
        }
        return 1;
    }
    
    private void rechargeAttackStack(Spirit attacker,int atkNumber)
    {
        if(attacker.attacks[atkNumber].stacks>1)
        {
            attacker.attacks[atkNumber].stacks-=2;
        }
        
        for (int i=0; i < attacker.attacks.length; i++) 
        {
            if(i!=atkNumber && attacker.attacks[i].stacks<5)
            {
                attacker.attacks[i].stacks++;
            }
        }
    }
    
    private void CheckIfDefensorIsStillAlive(Spirit defender, Person attackerPerson)
    {
        if(defender.getHP()[0]<=0)
        {
            defender.setHP(0,0);
            
            Util.ClearBoard();
            
            PrintSpiritFaintedHud(defender,attackerPerson);
            
            AddXp(defender,attackerPerson);
            
            PrintBattleOver(attackerPerson);
        }
    }
    
    private void AddXp(Spirit defender, Person attackerPerson)
    {
        Spirit attacker=attackerPerson.spirits[0];
        
        //add xp to the spirit that won (100% of the loser lvl)
        int lvl =defender.getLevel();
        int xpToAddWin=(14*(lvl+2))+(((3*lvl)*(3*lvl))/5);
        attacker.experience+=xpToAddWin;
        
        //add xp to the spirit that lost (5% of the winner lvl)
        lvl =attacker.getLevel();
        int xpToAddLoss=(14*(lvl+2))+(((3*lvl)*(3*lvl))/5);
        xpToAddLoss*=0.05f;
        defender.experience+=xpToAddLoss;
        
        if(attackerPerson==player)
        {
            PrintXpWonHud(xpToAddWin);
        }
        else
        {
            PrintXpWonHud(xpToAddLoss);
        }
        
        enemy.spirits[0].CheckIfSpiritLeveledUp();
        
        if(player.spirits[0].CheckIfSpiritLeveledUp() && isPlayerFighting)
        {
            PrintLvlUp();
        }
    }
    
    private void ResetStacks(Person person)
    {
        for (int i = 0; i < person.GetSpiritsCount(); i++) 
        {
            Spirit spirit=person.spirits[i];
            for (SpiritAttack attack : spirit.attacks) {
                attack.stacks = 5;
            }
        }
    }
    
    private void TryToRun(Person runner,Spirit enemy)
    {
        float chanceToRun=runner.spirits[0].getLevel();
        chanceToRun*=(new java.util.Random().nextInt(10)+1)/5.0f;
        
        if(chanceToRun>enemy.getLevel() )
        {
             runnedAway=true;
        }
        
        if(isPlayerFighting)
        {
            if(runnedAway)
            {
                PrintTryToRunAway(runner,true);
            }
            else
            {
                PrintTryToRunAway(runner,false);
            }
        }
    }
    
}
