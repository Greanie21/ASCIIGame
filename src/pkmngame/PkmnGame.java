/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

import java.io.File;
//import java.util.ArrayList;
//import java.util.HashMap;
import java.util.List;
//import java.util.Map;
import java.util.Scanner;

/**
 *
 * @author abol9
 */
public class PkmnGame 
{
 
    /**
     * @param args the command line arguments
     */
    static char[][] map;
    static char[][] visualMap;
    static boolean mapIsVisualMode=true;
    
    static PlayerDetails playerDetails;
    static Person[] npcs;
    
    static int boxHudSize=40;
    static boolean gameIsFinished=false;
    static Fight fight=new Fight();
    
    public static void main(String[] args) 
    {
        if(ChooseToCreateNewGame())
        {
            CreateNewGame();
        }
        else
        {
            String savedGame=SelectSavedGame();
            LoadGame(savedGame);
        }
        
        PlayGame();
        
        SaveGame();
    }
    
    static boolean ChooseToCreateNewGame()
    {
        //System.out.println("Game Started!");
        Util.ClearBoard();
        System.out.println("Game Started!");
        System.out.println("");
        
        String decision="";
        while (decision.equals("1")==false && decision.equals("2")==false )
        {
            System.out.println("1 - New Game"); 
            System.out.println("2 - Load Game"); 
            
            Scanner scan = new Scanner(System.in);
            decision=scan.next();
            
            Util.ClearBoard();
        }
        return (decision=="1");
    }
    
    static void CreateNewGame()
    {
        LoadGame("New Game");
        
        String newName="My New Name";
        boolean valid=false;
        List<File> InvalidNamesList=Util.GetDirectoriesToShow();
        InvalidNamesList.add(new File("New Game"));
        InvalidNamesList.add(new File("Maps"));
        
        Util.ClearBoard();
        
        while (valid==false)
        {
            System.out.println("Choose Your name:"); 
            
            newName=new Scanner(System.in).next();
            
            if(InvalidNamesList.contains(new File(newName))==false)
            {
                //TODO? - check if newName is a valid filename
                valid=true;
            }
            else
            {
                System.out.println("Invalid choice!"); 
            }
        }
        
        playerDetails.player.name=newName;
    }
    
    static String SelectSavedGame()
    {
        String savedGame="New Game";
        String choiceNumber="Temporary Text";
        
        List<File> directoriesToShow=Util.GetDirectoriesToShow();
        
        boolean playerMadeChoice=false;
        
        while (playerMadeChoice==false)
        {
            System.out.println("Select a game to load:\n");
            int count;
            for (count = 0; count < directoriesToShow.size(); count++) 
            {
                System.out.println(count+1+" - "+directoriesToShow.get(count).getName()); 
            }
            System.out.println("\n"+(count+1)+" - New Game");
        
            choiceNumber=new Scanner(System.in).next();
            playerMadeChoice=Util.TryParseInt(choiceNumber);
        }
         
        int choice=Integer.parseInt(choiceNumber);
        if(choice>0 && choice<=directoriesToShow.size())
        {
            savedGame=directoriesToShow.get(choice-1).getName();
        }
                
        return savedGame;
    }
    
    static void LoadGame(String playerName)
    {
        playerDetails=new PlayerDetails();
        int npcCount=4;
        npcs=new Person[npcCount];
        
        JsonManager.ReadJsonPlayerDetails(playerName,playerDetails);
        JsonManager.ReadJsonPerson(playerName,"Player",playerDetails.player);
        
        for (int i = 0; i < npcCount; i++) 
        {
            Person npc=new Person();
            JsonManager.ReadJsonPerson(playerName,"Npc"+i,npc);
        
            npcs[i]=npc;
        }
    }
    
    static void PlayGame()
    {
        ReadMaps();
        
        while (gameIsFinished==false)
        {
            if(PlayerActionIsValid(new Scanner(System.in).next())==false)
            {
                System.out.println("Please input an valid action"); 
                System.out.println("Type /help for the valid commands list"); 
            }
        }
    }
    
    static boolean PlayerActionIsValid(String movement)
    {
        int newX=playerDetails.player.mapPosition[0];
        int newY=playerDetails.player.mapPosition[1];
        switch(movement.toLowerCase())
        {
            case "w":
                newY--;
                if(newY<0)
                {
                    return false;
                }
                break;
            case "a":
                newX--;
                if(newX<0)
                {
                    return false;
                }
                break;
            case "s":
                newY++;
                if(newY>map[0].length)
                {
                    return false;
                }
                break;
            case "d":
                newX++;
                if(newX>map.length)
                {
                    return false;
                }
                break;
            case "/help":
                System.out.println("w -> move North (Up)");
                System.out.println("a -> move West  (Left)");
                System.out.println("s -> move South (Down)");
                System.out.println("d -> move East  (Right)");
                System.out.println("/save -> save the game");
                System.out.println("/exit -> exit the game");
                System.out.println("/changeview -> alternate between graphic and simbolic map");
                System.out.println("/tournament -> Start a tournament with the npcs in your map");
                return true;
            case "/save":
                SaveGame();
                System.out.println("Game Saved");
                return true;
            case "/exit":
            case "/quit":
                gameIsFinished=true;
                return true;
            case "/changeview":
                mapIsVisualMode=!mapIsVisualMode;
                if(mapIsVisualMode)
                {
                    Util.DrawVisualBoard(visualMap,playerDetails.player);
                }
                else
                {
                    Util.DrawBoard(map,playerDetails.player);
                }
                return true;
            case "/tournament":
                Tournament.StartTournament(npcs, playerDetails.player.mapName, true, playerDetails.player);
                CallDrawMap();
                return true;
            case "/bag":
            case "/inventory":
                InventoryManager.UseInventory(playerDetails.player, null, boxHudSize, false);
                CallDrawMap();
                return true;
            default:
                return false;
        }
        
        return MapTilesEvents(newX, newY);
    }
    
    static boolean MapTilesEvents(int newX, int newY)
    {
        char mapTile=map[newX][newY];
        if(mapTile=='*')
        {
            Move(newX,newY);
            return true;
        }
        if(mapTile=='W')
        {
            Move(newX,newY);
            StepOnTallGrass();
            return true;
        }
        if(mapTile=='#')
        {
            return false;
        }
        
        String mapName=playerDetails.player.mapName;
        
        switch(mapName)
        {
            case "Pewter":
                switch (mapTile)
                {
                    case '1':
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '2':
                        ChangeMaps("Route101",12,1);
                        return true;
                    case '3':
                        System.out.println("You Entered the Museum!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '4':
                        System.out.println("You Entered a house!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '5':
                        System.out.println("You Entered a house!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '6':
                        return TryToEnterTournament(mapName,15,16);
                    case '7':
                        System.out.println("You Entered the store!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '8':
                        ChangeMaps("SleepCenterPewter",6,8);
                        return true;
                    case '9':
                        System.out.println("You Entered a house!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                }
                break;
            case "Celadon":
                switch (mapTile)
                {
                    case '1':
                        ChangeMaps("Route204",8,1);
                        return true;
                    case '2':
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '3':
                        ChangeMaps("SleepCenterCeladon",6,8);
                        return true;
                    case '4':
                        return TryToEnterTournament(mapName,20,12);
                    case '5':
                        System.out.println("You Entered a house!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '6':
                        
                    case '7':
                        System.out.println("You Entered the store!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                    case '8':
                    case '9':
                        System.out.println("You Entered a house!");
                        System.out.println("You changed maps!");
                        System.out.println("Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                }
                break;
            case "Route101":
                if(mapTile=='1')
                {
                    ChangeMaps("Pewter",20,39);
                    return true;
                }
                else if(mapTile=='2')
                {
                    ChangeMaps("Route102",1,1);
                    return true;
                }
                break;
            case "Route102":
                if(mapTile=='1')
                {
                    ChangeMaps("Route101",12,38);
                    return true;
                }
                else if(mapTile=='2')
                {
                    PassTheDay();
                    ChangeMaps("Route201",5,10);
                    return true;
                }
                break;
            case "Route201":
                if(mapTile=='1')
                {
                    PassTheDay();
                    ChangeMaps("Route102",20,10);
                    return true;
                }
                else if(mapTile=='2')
                {
                    ChangeMaps("Route202",1,9);
                    return true;
                }
                break;
            case "Route202":
                if(mapTile=='1')
                {
                    ChangeMaps("Route201",36,4);
                    return true;
                }
                else if(mapTile=='2')
                {
                    ChangeMaps("Route203",18,37);
                    return true;
                }
                else if(mapTile=='3')
                {
                        System.out.println("You changed maps!");
                        System.out.println("Secret Route Not Yet Implemented");
                        gameIsFinished=true;
                        return true;
                }
                break;
            case "Route203":
                if(mapTile=='1')
                {
                    PassTheDay();
                    ChangeMaps("Route202",20,2);
                    return true;
                }
                else if(mapTile=='2')
                {
                    ChangeMaps("Route204",11,18);
                    return true;
                }
                else if(mapTile=='3')
                {
                        System.out.println("You changed maps!");
                        System.out.println("Route Not Yet Implemented");
                        System.out.println("How the Fuck did you get here?");
                        gameIsFinished=true;
                        return true;
                }
                else if(mapTile=='4')
                {
                        System.out.println("You changed maps!");
                        System.out.println("Route Not Yet Implemented");
                        System.out.println("How the Fuck did you get here?");
                        gameIsFinished=true;
                        return true;
                }
                break;
            case "Route204":
                if(mapTile=='1')
                {
                    PassTheDay();
                    ChangeMaps("Route203",18,14);
                    return true;
                }
                else if(mapTile=='2')
                {
                    ChangeMaps("Celadon",22,38);
                    return true;
                }
                break;
            case "SleepCenterPewter":
               return SleepCenterTileEvents(mapTile,mapName,15,26);
            case "SleepCenterCeladon":
               return SleepCenterTileEvents(mapTile,mapName,20,12);
        }
        return false;
    }
    
    static void ChangeMaps(String mapName, int x, int y)
    {
        playerDetails.player.mapName=mapName;
        playerDetails.player.mapPosition[0]=x;
        playerDetails.player.mapPosition[1]=y;
        ReadMaps();
    }
    
    static boolean TryToEnterTournament(String mapName, int x, int y)
    {
        int remainingDays=playerDetails.day%10;
        if(remainingDays==0)
        {
            if(Tournament.StartTournament(npcs, mapName, true, playerDetails.player))
            {
                CallDrawMap();
            }
            else
            {
                Bed.Sleep(playerDetails.player,true);
                PassTheDay();
                            
                ChangeMaps(mapName,x,y);
            }
            return true;
        }
        else
        {
            remainingDays=10-remainingDays;
            System.out.println("Can only enter the Tournament every 10 days");
            System.out.println("Days until next Tournament:"+remainingDays);
            return false;
        }
    }
    
    static boolean SleepCenterTileEvents(char mapTile,String mapName,int x,int y)
    {
        if(mapTile=='1')
        {
            ChangeMaps(mapName,x,y);
            return true;
        }
        if(mapTile=='2')
        {
            Util.ClearBoard();
            System.out.println("If you sleep the day will pass and you will recover your Spirits Strenght.");
            System.out.println("Do you want to sleep?(Y/N)");
                        
            String response=new Scanner(System.in).next();
            if("Y".equals(response.toUpperCase()))
            {
                Bed.Sleep(playerDetails.player,true);
                PassTheDay();
                            
                ChangeMaps(mapName,x,y);
            }
            else
            {
                playerDetails.player.mapPosition[0]=6;
                playerDetails.player.mapPosition[1]=5;
                CallDrawMap();
            }
            return true;
        }
        return false;
    }
    
    static void Move(int newX,int newY)
    {
        playerDetails.player.mapPosition[0]=newX;
        playerDetails.player.mapPosition[1]=newY;
        
        CallDrawMap();
    }
    
    static void CallDrawMap()
    {
        if(mapIsVisualMode)
        {
            Util.DrawVisualBoard(visualMap,playerDetails.player);
        }
        else
        {
            Util.DrawBoard(map,playerDetails.player);
        }
    }
    
    static void StepOnTallGrass()
    {
        if(new java.util.Random().nextInt(100)+1<=10)
        {
            int difficulty=0;
            switch (playerDetails.player.mapName)
            {
                case "Route101":
                    difficulty=0;
                    break;
                case "Route102":
                case "Route201":
                    difficulty=1;
                    break;
                case "Route202":
                    difficulty=2;
                    break;
                default:
                    difficulty=0;
                    break; 
            }
            
            Person wild=CreateWildSpirit.WildEncounter(difficulty);
            if(fight.Fight(playerDetails.player, wild,true))
            {
                CallDrawMap();
            }
            else
            {
                //TODO - COLOCAR ELE NO CENTRO PKMN
                //e dormir
                ReadMaps();
                PassTheDay();
            }
        }
    }
    
    static void ReadMaps()
    {
        map=TxtReader.ReadMap(playerDetails.player.mapName);
        visualMap=TxtReader.ReadVisualMap(playerDetails.player.mapName);
        
        CallDrawMap();
    }
    
    static void SaveGame()
    {
        String playerName=playerDetails.player.name;
        
        JsonManager.WriteJsonPerson(playerDetails.player,playerName,"Player");
        
        for (int i = 0; i < npcs.length; i++)
        {
            JsonManager.WriteJsonPerson(npcs[0],playerName,"Npc"+i);
        }
        
        //save the day
        JsonManager.WriteJsonPlayerDetails(playerDetails,playerName);
    }

    static void PassTheDay()
    {
        playerDetails.day++;
        
        for (int i = 0; i < npcs.length; i++) 
        {
            PassPersonsDay(npcs[i],true);
        }
    }
    
    static void PassPersonsDay(Person person,boolean isNpc)
    {
        Bed.Sleep(person, false);
        //TODO make the person pass the day doing things like:
        //like gain xp with its spirits 
        for (int i = 0; i < person.GetSpiritsCount(); i++) 
        {
            int lvl=person.spirits[i].getLevel();
            int xpWon=((lvl+2)*14+(((3*lvl)*(3*lvl))/3));
            
            person.spirits[i].experience+=xpWon;
            person.spirits[i].CheckIfSpiritLeveledUp();
        }
        
        if(isNpc)
        {
            //maybe change maps
        }
    }
}
