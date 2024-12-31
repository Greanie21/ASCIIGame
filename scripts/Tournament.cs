
public class Tournament 
{
    static Person[] npcs;
    static FightClass fight =new FightClass();
    static Person player;
    static bool playerIsParticipating;
    static String map;
    
    public static bool StartTournament(Person[] npcsArray,String mapName,bool playerParticipating,Person playerP)
    {
        npcs=npcsArray;
        player=playerP;
        playerIsParticipating=playerParticipating;
        map=mapName;
        
        for (int i = 0; i < npcs.Length; i++) 
        {
            Bed.Sleep(npcs[i], false);
        }
        
        List<int> tournamentNpc = new List<int>();
        for (int i = 0; i < npcs.Length; i++) 
        {
            if(npcs[i].mapName==(mapName))
            {
                tournamentNpc.Add(i);
            }
        }
        
        //TODO fazer chave do torneio e fazer lutas
        Person[] brackets=GetTournamentBrackets(tournamentNpc);
        
        if(TournamentFights(brackets))
        {
             Console.WriteLine("Player won the tournament");
            return true;
        }
        else if(playerIsParticipating)
        {
             Console.WriteLine("Player lost the tournament");
        }
        return false;
    }
    
    private static Person[] GetTournamentBrackets(List<int> tournamentNpc)
    {
        int size=tournamentNpc.Count();
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
        
        while(tournamentNpc.Count()>0)
        {
            Random random = new Random();
            int rand=random.Next(0,tournamentNpc.Count());
            
            brackets[count]=npcs[tournamentNpc.ElementAt(rand)];
            tournamentNpc.Remove(rand);
            count++;
        }
        
        return brackets;
    }
    
    private static bool TournamentFights(Person[] brackets)
    {
        int size=(int)Math.Ceiling((float)brackets.Length/2.0f);
        Person [] nextRound=new Person[size];
        int count=0;
        
        Util.PrintAndWait("Remaining fights:"+size);
        
        for (int i = 0; i < brackets.Length; i+=2) 
        {
            if(playerIsParticipating && i==0 && brackets.Length>1)
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
            else if (i+2<=brackets.Length) 
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
