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
public class AttacksVault 
{
    
    public static SpiritAttack GetAttack(String atkName)
    {
        SpiritAttack atk=new SpiritAttack();
        switch (atkName)
        {
            case "Water Spray":
                atk.name=atkName;
                atk.power=25;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Water;
                break;
            case "Water Whip":
                atk.name=atkName;
                atk.power=50;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Water;
                break;
//            case "":
//                atk.name=atkName;
//                atk.power=75;
//                atk.atkAccuracy=100;
//                atk.isMagical=false;
//                atk.element=Element.Type.Water;
//                break;
            case "High Tide":
                atk.name=atkName;
                atk.power=60;
                atk.atkAccuracy=85;
                atk.isMagical=true;
                atk.element=Element.Type.Water;
                break;
            case "Flood":
                atk.name=atkName;
                atk.power=125;
                atk.atkAccuracy=75;
                atk.isMagical=true;
                atk.element=Element.Type.Water;
                break;
            case "Pebble Toss":
                atk.name=atkName;
                atk.power=25;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Earth;
            case "Rock Toss":
                atk.name=atkName;
                atk.power=50;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Earth;
                break;
            case "Boulder Throw":
                atk.name=atkName;
                atk.power=75;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Earth;
            case "Earth Tremor":
                atk.name=atkName;
                atk.power=60;
                atk.atkAccuracy=85;
                atk.isMagical=true;
                atk.element=Element.Type.Earth;
                break;
            case "Tectonic Shake":
                atk.name=atkName;
                atk.power=125;
                atk.atkAccuracy=75;
                atk.isMagical=true;
                atk.element=Element.Type.Earth;
                break;
            case "Ember Slap":
                atk.name=atkName;
                atk.power=25;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Fire;
                break;
            case "Flame Punch":
                atk.name=atkName;
                atk.power=25;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Fire;
                break;
            case "Fire Kick":
                atk.name=atkName;
                atk.power=25;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Fire;
                break;
            case "Flame Breath":
                atk.name=atkName;
                atk.power=60;
                atk.atkAccuracy=85;
                atk.isMagical=true;
                atk.element=Element.Type.Fire;
                break;
            case "Blaze Bomb":
                atk.name=atkName;
                atk.power=125;
                atk.atkAccuracy=75;
                atk.isMagical=true;
                atk.element=Element.Type.Fire;
                break;
//            case "":
//                atk.name=atkName;
//                atk.power=25;
//                atk.atkAccuracy=100;
//                atk.isMagical=false;
//                atk.element=Element.Type.Air;
//                break;
            case "Pressure Punch":
                atk.name=atkName;
                atk.power=50;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Air;
                break;
            case "Air Blade":
                atk.name=atkName;
                atk.power=75;
                atk.atkAccuracy=100;
                atk.isMagical=false;
                atk.element=Element.Type.Air;
                break;
            case "Wind Ball":
                atk.name=atkName;
                atk.power=60;
                atk.atkAccuracy=85;
                atk.isMagical=true;
                atk.element=Element.Type.Air;
                break;
//            case "":
//                atk.name=atkName;
//                atk.power=60;
//                atk.atkAccuracy=85;
//                atk.isMagical=true;
//                atk.element=Element.Type.Air;
//                break;
            default:
                atk.name="Soul Attack";
                atk.power=15;
                atk.atkAccuracy=125;
                atk.isMagical=true;
                atk.element=Element.Type.Air;
                break;
        }
        
        return atk;
    }    
    
    public static SpiritAttack GetAttack(String element,boolean isPhysical,int power)
    {
        switch(element)
        {
            case "Water":
                if(isPhysical)
                {
                    if(power==1)
                    {
                        return GetAttack("Water Spray");
                    }
                    else if(power==2)
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
                    if(power==1)
                    {
                        return GetAttack("High Tide");
                    }
                    else
                    {
                        return GetAttack("Flood");
                    }
                }
            case "Earth":
                if(isPhysical)
                {
                    if(power==1)
                    {
                        return GetAttack("Pebble Toss");
                    }
                    else if(power==2)
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
                    if(power==1)
                    {
                        return GetAttack("Earth Tremor");
                    }
                    else
                    {
                        return GetAttack("Tectonic Shake");
                    }
                }
            case "Fire":
                if(isPhysical)
                {
                    if(power==1)
                    {
                        return GetAttack("Ember Slap");
                    }
                    else if(power==2)
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
                    if(power==1)
                    {
                        return GetAttack("Flame Breath");
                    }
                    else
                    {
                        return GetAttack("Blaze Bomb");
                    }
                }
            default:
                if(isPhysical)
                {
                    if(power==1)
                    {
                        return GetAttack("");
                    }
                    else if(power==2)
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
                    if(power==1)
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
}
