/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author abol9
 */
public class Tournament 
{
    static Person[] npcs;
    static Fight fight=new Fight();
    static Person player;
    static boolean playerIsParticipating;
    static String map;
    
    public static boolean StartTournament(Person[] npcsArray,String mapName,boolean playerParticipating,Person playerP)
    {
        npcs=npcsArray;
        player=playerP;
        playerIsParticipating=playerParticipating;
        map=mapName;
        
        for (int i = 0; i < npcs.length; i++) 
        {
            Bed.Sleep(npcs[i], false);
        }
        
        List<Integer> tournamentNpc = new ArrayList<Integer>();
        for (int i = 0; i < npcs.length; i++) 
        {
            if(npcs[i].mapName.equals(mapName))
            {
                tournamentNpc.add(i);
            }
        }
        
        //TODO fazer chave do torneio e fazer lutas
        Person[] brackets=GetTournamentBrackets(tournamentNpc);
        
        if(TournamentFights(brackets))
        {
            System.out.println("Player won the tournament");
            return true;
        }
        else if(playerIsParticipating)
        {
            System.out.println("Player lost the tournament");
        }
        return false;
    }
    
    private static Person[] GetTournamentBrackets(List<Integer> tournamentNpc)
    {
        int size=tournamentNpc.size();
        if(playerIsParticipating)
        {
            size++;
        }
        Person[] brackets=new Person[size];
        
        int count=0;
        if(playerIsParticipating)
        {
            brackets[0]=player;
            count++;
        }
        
        while(tournamentNpc.size()>0)
        {
            int rand=new java.util.Random().nextInt(tournamentNpc.size());
            
            brackets[count]=npcs[tournamentNpc.get(rand)];
            tournamentNpc.remove(rand);
            count++;
        }
        
        return brackets;
    }
    
    private static boolean TournamentFights(Person[] brackets)
    {
        int size=(int)Math.ceil((float)brackets.length/2.0f);
        Person [] nextRound=new Person[size];
        int count=0;
        
        Util.PrintAndWait("Remaining fights:"+size);
        
        for (int i = 0; i < brackets.length; i+=2) 
        {
            if(playerIsParticipating && i==0 && brackets.length>1)
            {
                if(fight.Fight(brackets[i], brackets[i+1],true))
                {
                    //CallDrawMap();
                    nextRound[count]=brackets[i];
                }
                else
                {
                    //ReadMaps();
                    //playerDetails.day++;
                    
                    nextRound[count]=brackets[i+1];
                    playerIsParticipating=false;
                }
            }
            else if (i+2<=brackets.length) 
            {
                if(fight.Fight(brackets[i], brackets[i+1],false))
                {
                    nextRound[count]=brackets[i];
                }
                else
                {
                    nextRound[count]=brackets[i+1];
                }
            }
            else
            {
                nextRound[count]=brackets[i];
            }
            count++;
        }
        
        if(size==1)
        {
            //if there is only one Person remaining
            //give the winner their reward
            RewardWinner(nextRound[0]);
            
            //the winner is the player only if he is still on the competition
            return playerIsParticipating;
        }
        else
        {
            //else continue the tournament until there is only 1 Person remaining
            return TournamentFights(nextRound);
        }
    }
    
    private static void RewardWinner(Person winner)
    {
        switch (map)
        {
            case "Pewter":
                winner.soulStones++;
                break;
            default:
                winner.lesserHpRestores+=3;
                break;
        }
    }
}
