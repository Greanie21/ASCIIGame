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
public class Person 
{
    String name;
    String mapName;
    int[] mapPosition=new int[]{0,0}; 
    Spirit[] spirits=new Spirit[6];
    
    public static int soulStones;//capture new spirit
    public static int lesserHpRestores;//restore small amount of hp
    public static int hpRestores;//restore some amount of hp
    public static int greaterHpRestores;//restore big amount of hp
    public static int ultimateHpRestores;//restore all hp
    int GetSpiritsCount()
    {
        int count=0;
        
        for (int i=0;i<spirits.length;i++)
        {
            try
            {
                if(spirits[i].getLevel()>0)
                {
                    count++;
                }
            }
            catch(Exception e)
            {
                    
            }
        }
        return count;
    }
}
