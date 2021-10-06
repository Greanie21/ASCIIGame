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
public class InventoryManager 
{
    static Scanner scan = new Scanner(System.in);
             
    public static void UseInventory(Person person,Spirit foe,int hudSize,boolean isNpc)
    {
        String choice;
        
        do
        {
            InventoryManager.PrintInventory(person, hudSize);
            
            if(isNpc)
            {
                choice=Integer.toString(new java.util.Random().nextInt(6)+1);
            }
            else
            {
                choice=scan.next();
            }
            
        }while(InventoryManager.InventoryChoiceIsValid(choice,person,foe,isNpc)==false);
        
      UseItem(person,choice,foe);
    }
    
    private static void PrintInventory(Person person,int hudSize)
    {
        Util.ClearBoard();
        
        String firstLine=" ";
        while(firstLine.length()<hudSize)
        {
            firstLine+="_";
        }
        firstLine+=" ";
        System.out.println(firstLine);
        
        String line2="| 1 - Soulstone: X ";
        line2+=person.soulStones;
        while(line2.length()<hudSize)
        {
            line2+=" ";
        }
        line2+="|";
        System.out.println(line2);
        
        String line3="| 2 - Lesser Hp Restore: X ";
        line3+=person.lesserHpRestores;
        while(line3.length()<hudSize)
        {
            line3+=" ";
        }
        line3+="|";
        System.out.println(line3);
        
        String line4="| 3 - Hp Restore: X ";
        line4+=person.hpRestores;
        while(line4.length()<hudSize)
        {
            line4+=" ";
        }
        line4+="|";
        System.out.println(line4);
        
        String line5="| 4 - Greater Hp Restore: X ";
        line5+=person.greaterHpRestores;
        while(line5.length()<hudSize)
        {
            line5+=" ";
        }
        line5+="|";
        System.out.println(line5);
        
        String line6="| 5 - Ultimate Hp Restore: X ";
        line6+=person.ultimateHpRestores;
        while(line6.length()<hudSize)
        {
            line6+=" ";
        }
        line6+="|";
        System.out.println(line6);
        
        String line7="| 6 - Cancel";
        while(line7.length()<hudSize)
        {
            line7+=" ";
        }
        line7+="|";
        System.out.println(line7);
        
        String lastLine="|";
        while(lastLine.length()<hudSize)
        {
            lastLine+="_";
        }
        lastLine+="|";
        System.out.println(lastLine);
    }
    
    private static boolean InventoryChoiceIsValid(String choice,Person person,Spirit foe,boolean isNpc)
    {
        switch(choice)
        {
            case "1":
                if(person.soulStones>0)
                {
                    if(foe==null)
                    {
                        PrintFail("You can only use this in battles",isNpc);
                        return false;
                    }
                    else if(foe.wild==false)
                    {
                        PrintFail("You can only capture Wild Spirits",isNpc);
                        return false;
                    }
                    boolean lowLevel=foe.getLevel()>person.spirits[0].getLevel();
                    boolean lowHp=foe.getHP()[0]<=(foe.getHP()[1]/2);
                    boolean itIsWeaker=(lowLevel || lowHp);
                    
                    if(itIsWeaker && person.GetSpiritsCount()<6)
                    {
                        return true;
                    }
                    PrintFail("You need to weaken the Wild Spirit before you can capture it",isNpc);
                }
                PrintFail("Not enough in bag",isNpc);
                return false;
            case "2":
                if(person.lesserHpRestores>0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag",isNpc);
                    return false;
                }
            case "3":
                if(person.hpRestores>0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag",isNpc);
                    return false;
                }
            case "4":
                if(person.greaterHpRestores>0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag",isNpc);
                    return false;
                }
            case "5":
                if(person.ultimateHpRestores>0)
                {
                    return true;
                }
                else
                {
                    PrintFail("Not enough in bag",isNpc);
                    return false;
                }
            case "6":
                return true;
            default:
                PrintFail("Please select an valid option",isNpc);
                return false;
                
        }
    }
    
    private static void UseItem(Person person,String choice,Spirit foe)
    {
        final int restoreQuantity;
        int hpCurrent=person.spirits[0].getHP()[0];
        int hpMax=person.spirits[0].getHP()[1];
        switch(choice)
        {
            case "1":
                person.soulStones--;
                int newSpiritPosition=person.GetSpiritsCount();
                foe.setHP(0, 0);
                foe.wild=false;
                person.spirits[newSpiritPosition]=foe;
                break;
            case "2":
                person.lesserHpRestores--;
                restoreQuantity=100;
                person.spirits[0].setHP(0, hpCurrent+restoreQuantity);
                if(hpCurrent>hpMax)
                {
                    person.spirits[0].setHP(0,hpMax);
                }
                break;
            case "3":
                person.hpRestores--;
                restoreQuantity=250;
                person.spirits[0].setHP(0, hpCurrent+restoreQuantity);
                if(hpCurrent>hpMax)
                {
                    person.spirits[0].setHP(0,hpMax);
                }
                break;
            case "4":
                person.greaterHpRestores--;
                restoreQuantity=500;
                person.spirits[0].setHP(0, hpCurrent+restoreQuantity);
                if(hpCurrent>hpMax)
                {
                    person.spirits[0].setHP(0,hpMax);
                }
                break;
            case "5":
                person.ultimateHpRestores--;
                person.spirits[0].setHP(0,hpMax);
                break;
        }
    }
    
    private static void PrintFail(String message,boolean isNpc)
    {
        if(isNpc==false)
        {
            Util.PrintAndWait(message);
        }
    }
}
