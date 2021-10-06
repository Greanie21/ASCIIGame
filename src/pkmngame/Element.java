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
public class Element 
{
    public enum Type 
    {
        Water(1),Earth(2),Fire(3),Air(4);
        
        int elementValue;
        
        Type(int value)
        {
            elementValue=value;   
        }
        
    }
}
