/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import static pkmngame.InventoryManager.scan;

/**
 *
 * @author abol9
 */
public class Util 
{
   private final static int xVisionMax=7;
   private final static int yVisionMax=4;
   private final static int tileCharSizeX=12;
   private final static int tileCharSizeY=6;
   
   public static void DrawBoard(char[][] map,Person player)
    {
        ClearBoard();
        
        //Draw the top part of the board
        System.out.print(" ");
        for (int i = 0; i < xVisionMax*2; i++) 
        {
            System.out.print("_"); 
        }
        System.out.println();
        
        //Draw the board
         for (int j = 0; j < map[0].length; j++) 
        {
            //Draw the left part of the board
            String line="|"; 
            
            for (int i = 0; i < map.length; i++) 
            {
                if(PlayerIsNearThisPartOfTheMap(i,j,player))
                {
                    if(player.mapPosition[0]==i && player.mapPosition[1]==j)
                    {
                        line+="@";
                    }
                    else
                    {
                        line+=map[i][j];
                    }
                }
            }
            
            //Draw the right part of the board
            line+="|"; 
            
            //if the line will be showed than print
            if("||".equals(line)==false)
            {
                System.out.println(line); 
            }
        }
         
        //Draw the bottom part of the board
        System.out.print(" ");
        for (int i = 0; i < xVisionMax*2; i++) 
        {
            System.out.print("_"); 
        }
        System.out.println(); 
    }
   
   public static void DrawVisualBoard(char[][] visualMap,Person player)
    {        
        ClearBoard();
        
        //Draw the board
         for (int i = 0; i < visualMap[0].length; i++)//y
        {
            String line=""; 
            
            for (int j = 0; j < visualMap.length; j++)//x
            {
                int xTile=(int)Math.ceil((j+1.0f)/tileCharSizeX)-1;
                int yTile=(int)Math.ceil((i+1.0f)/tileCharSizeY)-1;

                if(PlayerIsNearThisPartOfTheMap(xTile,yTile,player))
                {
                    if(player.mapPosition[0]==xTile && player.mapPosition[1]==yTile)
                    {
                        //desenhar o player
                        line+="@";
                    }
                    else
                    {
                        line+=visualMap[j][i];
                    }
                }
            }
            
            //if the line will be showed than print
            if("".equals(line)==false)
            {
                System.out.println(line); 
            }
        }
    }
    
    public static void ClearBoard()
    {
      for (int i = 0; i < 100; ++i)  
       System.out.println();     
    }
    
   private static boolean PlayerIsNearThisPartOfTheMap(int xCoordinate,int yCoordinate,Person player)
    {
        int distanceX=xCoordinate-player.mapPosition[0];
        if (distanceX<0)
        {
            distanceX=distanceX*-1;
        }
        
        int distanceY=yCoordinate-player.mapPosition[1];
        if (distanceY<0)
        {
            distanceY=distanceY*-1;
        }
        
        if(distanceX<=xVisionMax && distanceY<=yVisionMax)
        {
            return true;
        }
        return false;
    }
   
   public static boolean TryParseInt(String value)
   {
        try 
        {  
            Integer.parseInt(value);  
            return true;  
        } 
        catch (NumberFormatException e) 
        {  
            return false;  
        }   
   }
   
    public static List<File> GetDirectoriesToShow()
    {
        List<File> directoriesToShow=new ArrayList<File>();
        
        String path= System.getProperty("user.dir")+"\\FilesToLoad";
        File[] directories = new File(path).listFiles(File::isDirectory);
        
        for (int i = 0; i < directories.length; i++) 
        {
            String directoryName=directories[i].getName();
            if(!"New Game".equals(directoryName) && !"Maps".equals(directoryName))
            {
                directoriesToShow.add(directories[i]);
            }
        }
        
        return directoriesToShow;
    }
    
    public static void PrintAndWait(String message)
    {
        ClearBoard();
        System.out.println(message);
        scan.next();
    }
}
