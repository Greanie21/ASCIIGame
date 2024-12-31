using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonManager
{
    public static void WriteJsonPerson(Person person, String playerName, String fileName)
    {
        // creating JObject 
        var jsonObjDetails = new JObject
        {
            //["personName"] = person.name,
            //["mapName"] = person.MapName,
            //["mapCoordinatesX"] = person.MapPosition[0],
            //["mapCoordinatesY"] = person.MapPosition[1],
            //["soulStones"] = person.SoulStones,
            //["lesserHpRestores"] = person.LesserHpRestores,
            //["hpRestores"] = person.HpRestores,
            //["greaterHpRestores"] = person.GreaterHpRestores,
            //["ultimateHpRestores"] = person.UltimateHpRestores
        };

        // NAME 
        jsonObjDetails["personName"] = person.name;

        //MAP COORDINATES
        jsonObjDetails["mapName"] = person.mapName;
        jsonObjDetails["mapCoordinatesX"] = person.mapPosition[0];
        jsonObjDetails["mapCoordinatesY"] = person.mapPosition[1];

        //SPIRITS
        JArray spiritsAray = new JArray();
        for (int i = 0; i < person.GetSpiritsCount(); i++)
        {
            //infs start
            JArray spiritsInfArray = new JArray();

            spiritsInfArray.Add(person.spirits[i].name);
            spiritsInfArray.Add(person.spirits[i].element.ToString());
            spiritsInfArray.Add(person.spirits[i].getLevel());
            spiritsInfArray.Add(person.spirits[i].experience);
            spiritsInfArray.Add(person.spirits[i].getHP()[0]);
            spiritsInfArray.Add(person.spirits[i].getHP()[1]);
            spiritsInfArray.Add(person.spirits[i].getAttack()[1]);
            spiritsInfArray.Add(person.spirits[i].getMagicAttack()[1]);
            spiritsInfArray.Add(person.spirits[i].getDefense()[1]);
            spiritsInfArray.Add(person.spirits[i].getMagicDefense()[1]);
            spiritsInfArray.Add(person.spirits[i].getSpeed()[1]);
            spiritsInfArray.Add(person.spirits[i].accuracy[1]);
            spiritsInfArray.Add(person.spirits[i].dodge[1]);

            //atks start
            JArray spiritsAtkArray = new JArray();

            for (int j = 0; j < person.spirits[i].attacks.Length; j++)
            {
                JArray spiritsAtkInfArray = new JArray();
                spiritsAtkInfArray.Add(person.spirits[i].attacks[j].name);
                spiritsAtkInfArray.Add(person.spirits[i].attacks[j].power);
                spiritsAtkInfArray.Add(person.spirits[i].attacks[j].atkAccuracy);
                spiritsAtkInfArray.Add(person.spirits[i].attacks[j].element.ToString());
                spiritsAtkInfArray.Add(person.spirits[i].attacks[j].isMagical);

                spiritsAtkArray.Add(spiritsAtkInfArray);
            }
            spiritsInfArray.Add(spiritsAtkArray);

            spiritsAray.Add(spiritsInfArray);
        }
        jsonObjDetails["spirits"] = spiritsAray;

        jsonObjDetails["soulStones"] = person.soulStones;
        jsonObjDetails["lesserHpRestores"] = person.lesserHpRestores;
        jsonObjDetails["hpRestores"] = person.hpRestores;
        jsonObjDetails["greaterHpRestores"] = person.greaterHpRestores;
        jsonObjDetails["ultimateHpRestores"] = person.ultimateHpRestores;

        //CREATE PERSON OBJECT
        JObject personObject = new JObject();
        personObject["person"] = jsonObjDetails;

        //create a JArray to put the JObject
        JArray personList = new JArray();
        personList.Add(personObject);

        CreateFolder(playerName, "Persons");

        //Write JSON file
        string path = Path.Combine("FilesToLoad", playerName, "Persons", fileName + ".json");
        File.WriteAllText(path, personList.ToString(Formatting.Indented));
    }

    public static void ReadJsonPerson(String playerName, String fileName, Person person)
    {
        string path = Path.Combine("FilesToLoad", playerName, "Persons", fileName + ".json");
        try
        {
            var personList = JArray.Parse(File.ReadAllText(path));

            //employeeList.forEach( emp -> parsePersonObject( (JObject) emp ) );
            ParsePersonObject((JObject)personList[0], person);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }



        //ERROR
    }

    //private static void parsePersonObject(JObject jsonObject,Person person) 
    //{
    //    JObject personObject = (JObject) jsonObject.get("person");

    //    //Get person name
    //    person.name = (String) personObject.get("personName");

    //    //get the person map name
    //    person.mapName=(String) personObject.get("mapName");

    //    //get the person map position
    //    person.mapPosition[0] =(int)(long)personObject.get("mapCoordinatesX");
    //    person.mapPosition[1] =(int)(long)personObject.get("mapCoordinatesY"); 

    //    //get all spirits of the person
    //    ArrayList<Object> listdata = new ArrayList<Object>();       
    //    JArray jArray = (JArray)personObject.get("spirits"); 
    //    for (int i=0;i<jArray.size();i++)
    //    { 
    //        //get each spirit of the person
    //        //convert the json Array to list data
    //        listdata.Add(jArray.get(i));

    //        //convert the listdata to jsonArray
    //        JArray jArray2=(JArray)listdata.get(i);
    //        //stats Length = array size - atks array
    //        int[] stats=new int[jArray2.size()-1];
    //        String spiritName=jArray2.get(0).toString();
    //        String elementType=jArray2.get(1).toString();
    //        SpiritAttack[] atks=new SpiritAttack[4];
    //        //start in 2 because of name and element
    //        for (int j = 2; j < jArray2.size(); j++) 
    //        {
    //            //convert the stats from jsonArray to int[]
    //            try
    //            {
    //                //for start in 2 and array in 0
    //                stats[j-2]=((Long)jArray2.get(j)).intValue();
    //            }
    //            catch(Exception e)
    //            {
    //                JArray jArray3=(JArray)jArray2.get(j);

    //                for (int k = 0; k < jArray3.size(); k++) 
    //                {
    //                    JArray jArray4=(JArray)jArray3.get(k);

    //                    SpiritAttack spiritAtk=new SpiritAttack();
    //                    spiritAtk.name=(String)jArray4.get(0);
    //                    spiritAtk.power=((Long)jArray4.get(1)).intValue();
    //                    spiritAtk.atkAccuracy=((Long)jArray4.get(2)).intValue();
    //                    spiritAtk.element=Element.Type.valueOf((String)jArray4.get(3));
    //                    spiritAtk.isMagical=(bool)jArray4.get(4);

    //                    atks[k]=spiritAtk;
    //                }
    //            }
    //        }

    //        //create the spirit
    //        person.spirits[i]=new Spirit(stats[0],stats[1],stats[2],
    //                stats[3],stats[4],stats[5],stats[6],stats[7],stats[8],
    //                stats[9],stats[10],spiritName,elementType,atks);
    //    }

    //    person.soulStones=(int)(long)personObject.get("soulStones");
    //    person.lesserHpRestores=(int)(long)personObject.get("lesserHpRestores");
    //    person.hpRestores=(int)(long)personObject.get("hpRestores");
    //    person.greaterHpRestores=(int)(long)personObject.get("greaterHpRestores");
    //    person.ultimateHpRestores=(int)(long)personObject.get("ultimateHpRestores");
    //}

    private static void ParsePersonObject(JObject jsonObject, Person person)
    {
        var personObject = (JObject)jsonObject["person"];
        person.name = personObject["personName"].ToString();
        person.mapName = personObject["mapName"].ToString();
        person.mapPosition[0] = personObject["mapCoordinatesX"].ToObject<int>();
        person.mapPosition[1] = personObject["mapCoordinatesY"].ToObject<int>();

        var spiritsArray = (JArray)personObject["spirits"];
        person.spirits = new Spirit[spiritsArray.Count];

        for (int i = 0; i < spiritsArray.Count; i++)
        {
            var spiritArray = (JArray)spiritsArray[i];
            string name = spiritArray[0].ToString();
            var element = Enum.Parse<Element.Type>(spiritArray[1].ToString());
            int[] stats = new int[11];
            SpiritAttack[] attacks = new SpiritAttack[4];

            for (int j = 2; j < spiritArray.Count; j++)
            {
                if (j < 13)
                {
                    stats[j - 2] = spiritArray[j].ToObject<int>();
                }
                else
                {
                    var attacksArray = (JArray)spiritArray[j];
                    for (int k = 0; k < attacksArray.Count; k++)
                    {
                        var attackArray = (JArray)attacksArray[k];
                        attacks[k] = new SpiritAttack
                        {
                            name = attackArray[0].ToString(),
                            power = attackArray[1].ToObject<int>(),
                            atkAccuracy = attackArray[2].ToObject<int>(),
                            element = Enum.Parse<Element.Type>(attackArray[3].ToString()),
                            isMagical = attackArray[4].ToObject<bool>()
                        };
                    }
                }
            }

            person.spirits[i] = new Spirit(stats, name, element, attacks);
        }

        person.soulStones = personObject["soulStones"].ToObject<int>();
        person.lesserHpRestores = personObject["lesserHpRestores"].ToObject<int>();
        person.hpRestores = personObject["hpRestores"].ToObject<int>();
        person.greaterHpRestores = personObject["greaterHpRestores"].ToObject<int>();
        person.ultimateHpRestores = personObject["ultimateHpRestores"].ToObject<int>();
    }

    public static void WriteJsonPlayerDetails(PlayerDetails player, String playerName)
    {
        // creating JObject 
        var jsonObjDetails = new JObject
        {
            ["day"] = player.day
        };

        //CREATE Player Detail OBJECT
        var playerDetailsObject = new JObject
        {
            ["details"] = jsonObjDetails
        };

        //create a JArray to put the JObject
        var playerDetailsList = new JArray { playerDetailsObject };

        CreateFolder(playerName, "PlayerDetails");

        //Write JSON file
        string path = Path.Combine("FilesToLoad", playerName, "PlayerDetails", "PlayerDetails.json");
        File.WriteAllText(path, playerDetailsList.ToString(Formatting.Indented));
    }

    public static void ReadJsonPlayerDetails(string playerName, PlayerDetails player)
    {
        string path = Path.Combine("FilesToLoad", playerName, "PlayerDetails", "PlayerDetails.json");

        try
        {
            var playerDetailsList = JArray.Parse(File.ReadAllText(path));
            ParsePlayerDetailObject((JObject)playerDetailsList[0], player);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ParsePlayerDetailObject(JObject jsonObject, PlayerDetails player)
    {
        var playerDetailObject = (JObject)jsonObject["details"];
        player.day = playerDetailObject["day"].ToObject<int>();
    }

    private static void CreateFolder(string playerName, string subFolder)
    {
        string basePath = Path.Combine(Environment.CurrentDirectory, "FilesToLoad", playerName);
        string folderPath = Path.Combine(basePath, subFolder);

        if (!Directory.Exists(basePath))
        {
            Directory.CreateDirectory(basePath);
        }

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}
