/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package pkmngame;

//
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
//import java.util.LinkedHashMap;
//import java.util.Iterator; 
//import java.util.logging.Level;
//import java.util.logging.Logger;
 
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;
 
/**
 *
 * @author abol9
 */
public class JsonManager 
{
    public static void WriteJsonPerson(Person person,String playerName, String fileName)
    {
        // creating JSONObject 
        JSONObject jsonObjDetails = new JSONObject(); 
        
        // NAME 
        jsonObjDetails.put("personName", person.name); 
        
        //MAP COORDINATES
        jsonObjDetails.put("mapName", person.mapName); 
        jsonObjDetails.put("mapCoordinatesX", person.mapPosition[0]); 
        jsonObjDetails.put("mapCoordinatesY", person.mapPosition[1]); 
        
        //SPIRITS
        JSONArray spiritsAray= new JSONArray();
        for(int i=0;i<person.GetSpiritsCount();i++)
        {
            //infs start
            JSONArray spiritsInfArray= new JSONArray();
            
            spiritsInfArray.add(person.spirits[i].name);
            spiritsInfArray.add(person.spirits[i].element.toString());
            spiritsInfArray.add(person.spirits[i].getLevel());
            spiritsInfArray.add(person.spirits[i].experience);
            spiritsInfArray.add(person.spirits[i].getHP()[0]);
            spiritsInfArray.add(person.spirits[i].getHP()[1]);
            spiritsInfArray.add(person.spirits[i].getAttack()[1]);
            spiritsInfArray.add(person.spirits[i].getMagicAttack()[1]);
            spiritsInfArray.add(person.spirits[i].getDefense()[1]);
            spiritsInfArray.add(person.spirits[i].getMagicDefense()[1]);
            spiritsInfArray.add(person.spirits[i].getSpeed()[1]);
            spiritsInfArray.add(person.spirits[i].accuracy[1]);
            spiritsInfArray.add(person.spirits[i].dodge[1]);
             
            //atks start
            JSONArray spiritsAtkArray= new JSONArray();
            
            for (int j = 0; j < person.spirits[i].attacks.length; j++) 
            {
                JSONArray spiritsAtkInfArray= new JSONArray();
                spiritsAtkInfArray.add(person.spirits[i].attacks[j].name);
                spiritsAtkInfArray.add(person.spirits[i].attacks[j].power);
                spiritsAtkInfArray.add(person.spirits[i].attacks[j].atkAccuracy);
                spiritsAtkInfArray.add(person.spirits[i].attacks[j].element.toString());
                spiritsAtkInfArray.add(person.spirits[i].attacks[j].isMagical);
                
                spiritsAtkArray.add(spiritsAtkInfArray);
            }
            spiritsInfArray.add(spiritsAtkArray);
            
            spiritsAray.add(spiritsInfArray);
        }
        jsonObjDetails.put("spirits",spiritsAray);
        
        jsonObjDetails.put("soulStones", person.soulStones);
        jsonObjDetails.put("lesserHpRestores", person.lesserHpRestores);
        jsonObjDetails.put("hpRestores", person.hpRestores);
        jsonObjDetails.put("greaterHpRestores", person.greaterHpRestores);
        jsonObjDetails.put("ultimateHpRestores", person.ultimateHpRestores);
        
        //CREATE PERSON OBJECT
        JSONObject personObject = new JSONObject(); 
        personObject.put("person", jsonObjDetails);
        
        //create a JSONArray to put the JSONObject
        JSONArray personList = new JSONArray();
        personList.add(personObject);
        
        CreateFolder(playerName,"Persons");
        
        //Write JSON file
        try (FileWriter file = new FileWriter("FilesToLoad\\"+playerName+"\\Persons\\"+fileName+".json")) 
        {
            file.write(personList.toJSONString());
            file.flush();
        } 
        catch (IOException e) 
        {
            
        }
    }
    
    public static void ReadJsonPerson(String playerName,String fileName,Person person)
    {
        String pathName="FilesToLoad\\"+playerName+"\\Persons\\"+fileName+".json";
        try (FileReader reader = new FileReader(pathName))
        {
            Object obj = new JSONParser().parse(reader);
            
            JSONArray personList = (JSONArray) obj;
             
            //employeeList.forEach( emp -> parsePersonObject( (JSONObject) emp ) );
            
            parsePersonObject((JSONObject) personList.get(0),person );
 
        } 
        catch (FileNotFoundException e) 
        {
             e.printStackTrace();
        } 
        catch (IOException e) 
        {
            e.printStackTrace();
        } 
        catch (ParseException e) 
        {
            e.printStackTrace();
        }
        
         //ERROR
    }

    private static void parsePersonObject(JSONObject jsonObject,Person person) 
    {
        JSONObject personObject = (JSONObject) jsonObject.get("person");
        
        //Get person name
        person.name = (String) personObject.get("personName");
        
        //get the person map name
        person.mapName=(String) personObject.get("mapName");
        
        //get the person map position
        person.mapPosition[0] =(int)(long)personObject.get("mapCoordinatesX");
        person.mapPosition[1] =(int)(long)personObject.get("mapCoordinatesY"); 
        
        //get all spirits of the person
        ArrayList<Object> listdata = new ArrayList<Object>();       
        JSONArray jArray = (JSONArray)personObject.get("spirits"); 
        for (int i=0;i<jArray.size();i++)
        { 
            //get each spirit of the person
            //convert the json Array to list data
            listdata.add(jArray.get(i));
            
            //convert the listdata to jsonArray
            JSONArray jArray2=(JSONArray)listdata.get(i);
            //stats length = array size - atks array
            int[] stats=new int[jArray2.size()-1];
            String spiritName=jArray2.get(0).toString();
            String elementType=jArray2.get(1).toString();
            SpiritAttack[] atks=new SpiritAttack[4];
            //start in 2 because of name and element
            for (int j = 2; j < jArray2.size(); j++) 
            {
                //convert the stats from jsonArray to int[]
                try
                {
                    //for start in 2 and array in 0
                    stats[j-2]=((Long)jArray2.get(j)).intValue();
                }
                catch(Exception e)
                {
                    JSONArray jArray3=(JSONArray)jArray2.get(j);
                    
                    for (int k = 0; k < jArray3.size(); k++) 
                    {
                        JSONArray jArray4=(JSONArray)jArray3.get(k);
                        
                        SpiritAttack spiritAtk=new SpiritAttack();
                        spiritAtk.name=(String)jArray4.get(0);
                        spiritAtk.power=((Long)jArray4.get(1)).intValue();
                        spiritAtk.atkAccuracy=((Long)jArray4.get(2)).intValue();
                        spiritAtk.element=Element.Type.valueOf((String)jArray4.get(3));
                        spiritAtk.isMagical=(boolean)jArray4.get(4);
                        
                        atks[k]=spiritAtk;
                    }
                }
            }
            
            //create the spirit
            person.spirits[i]=new Spirit(stats[0],stats[1],stats[2],
                    stats[3],stats[4],stats[5],stats[6],stats[7],stats[8],
                    stats[9],stats[10],spiritName,elementType,atks);
        }
        
        person.soulStones=(int)(long)personObject.get("soulStones");
        person.lesserHpRestores=(int)(long)personObject.get("lesserHpRestores");
        person.hpRestores=(int)(long)personObject.get("hpRestores");
        person.greaterHpRestores=(int)(long)personObject.get("greaterHpRestores");
        person.ultimateHpRestores=(int)(long)personObject.get("ultimateHpRestores");
    }
    
    public static void WriteJsonPlayerDetails(PlayerDetails player,String playerName)
    {
        // creating JSONObject 
        JSONObject jsonObjDetails = new JSONObject(); 
        
        jsonObjDetails.put("day", player.day); 
        
        //CREATE Player Detail OBJECT
        JSONObject playerDetailsObject = new JSONObject(); 
        playerDetailsObject.put("details", jsonObjDetails);
        
        //create a JSONArray to put the JSONObject
        JSONArray playerDetailsList = new JSONArray();
        playerDetailsList.add(playerDetailsObject);
        
        CreateFolder(playerName,"PlayerDetails");
        
        //Write JSON file
        try (FileWriter file = new FileWriter("FilesToLoad\\"+playerName+"\\PlayerDetails\\PlayerDetails.json")) 
        {
            file.write(playerDetailsList.toJSONString());
            file.flush();
        } 
        catch (IOException e) 
        {
            
        }
    }
    
    public static void ReadJsonPlayerDetails(String playerName,PlayerDetails player)
    {
        String pathName="FilesToLoad\\"+playerName+"\\PlayerDetails\\PlayerDetails.json";
        try (FileReader reader = new FileReader(pathName)) 
        {
            Object obj = new JSONParser().parse(reader);
            
            JSONArray playerDetailsList = (JSONArray) obj;
             
            //employeeList.forEach( emp -> parsePersonObject( (JSONObject) emp ) );
            
            parsePlayerDetailObject((JSONObject) playerDetailsList.get(0),player);
 
        } 
        catch (FileNotFoundException e) 
        {
             e.printStackTrace();
        } 
        catch (IOException e) 
        {
            e.printStackTrace();
        } 
        catch (ParseException e) 
        {
            e.printStackTrace();
        }
        
         //ERROR
    }

    private static void parsePlayerDetailObject(JSONObject jsonObject,PlayerDetails player) 
    {
        JSONObject playerDetailObject = (JSONObject) jsonObject.get("details");
        
        player.day=(int)(long)playerDetailObject.get("day");
    }
    
    private static void CreateFolder(String playerName,String subFolder)
    {
        //if players folder have not been created
        Path pathname=Paths.get(System.getProperty("user.dir")+"\\FilesToLoad\\"+playerName);
        if(Files.notExists(pathname))
        {
            File file = new File(pathname.toString());
            //Creating the directory
            file.mkdir();
        }
        
        pathname=Paths.get(System.getProperty("user.dir")+"\\FilesToLoad\\"+playerName+"\\"+subFolder);
        if(Files.notExists(pathname))
        {
            File file = new File(pathname.toString());
            //Creating the directory
            file.mkdir();
        }
    }
}
