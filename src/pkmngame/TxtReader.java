/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;


/**
 *
 * @author abol9
 */
public class TxtReader 
{
    public static void Print(String fileName)
    {
        // pass the path to the file as a parameter 
        File file = new File(System.getProperty("user.dir")+"\\"+fileName+".txt"); 
    
        try 
        {
           Scanner sc = new Scanner(file);
            
            while (sc.hasNextLine()) 
            {
                System.out.println(sc.nextLine());
            }
        }
        catch (FileNotFoundException ex) 
        {
            Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }
  
        
    }
    
    public static char[][] ReadMap(String fileName)
    {
        // pass the path to the file as a parameter 
        String pathName=System.getProperty("user.dir")+"\\FilesToLoad\\Maps\\"+fileName+".txt";
        File file = new File(pathName); 
        List<String> list = new ArrayList<String>();
        try 
        {
           Scanner sc = new Scanner(file);
            
            while (sc.hasNextLine()) 
            {
                list.add(sc.nextLine());
            }
        }
        catch (FileNotFoundException ex) 
        {
            Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }
        int x=list.get(0).length();
        int y=list.size();
        char[][] map=new char[x][y];
        
        for (int i = 0; i < list.size(); i++) 
        {
            String line=list.get(i);
            for (int j = 0; j < line.length(); j++) 
            {
                map[j][i]=line.charAt(j);
            }
        }
        return map;
    }
    
    public static char[][] ReadVisualMap(String fileName)
    {
        // pass the path to the file as a parameter 
        String pathName=System.getProperty("user.dir")+"\\FilesToLoad\\Maps\\Visuals\\"+fileName+".txt";
        File file = new File(pathName); 
        
        //read the txt
        List<String> list = new ArrayList<String>();
        try 
        {
           Scanner sc = new Scanner(file);
            
            while (sc.hasNextLine()) 
            {
                list.add(sc.nextLine());
            }
        }
        catch (FileNotFoundException ex) 
        {
            Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        int xSize=list.get(0).length();
        int ySize=list.size();
        
        char[][] visualMap=new char[xSize][ySize];
        
        for (int i = 0; i < ySize; i++)//y 
        {
            String line=list.get(i);
            for (int j = 0; j < xSize; j++)//x 
            {
                visualMap[j][i]=line.charAt(j);
            }
        }
        
        return visualMap;
    }
}
