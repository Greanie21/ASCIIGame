/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

/**
 *
 * @author abol9
 */
public class Bed 
{
    public static void Sleep(Person person,boolean print)
    {
        for (int i = 0; i < person.GetSpiritsCount(); i++) 
        {
                int maxHp = person.spirits[i].getHP()[1];
                person.spirits[i].setHP(0, maxHp);
        }
        
        if(print)
        {
            Util.ClearBoard();
            System.out.println(person.name+"'s Spirits have been completely healed!");
            
            System.out.print("Continue...");
            new java.util.Scanner(System.in).next();
        }
    }
}
