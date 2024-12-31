
public class TxtReader
{
    public static void Print(String fileName)
    {
        // pass the path to the file as a parameter 
        string filePath = Path.Combine(Environment.CurrentDirectory, fileName + ".txt");

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            // Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }
    }


    public static char[][] ReadMap(String fileName)
    {
        // pass the path to the file as a parameter 
        string filePath = Path.Combine(Environment.CurrentDirectory, "\\FilesToLoad\\Maps\\" + fileName + ".txt");

        List<String> list = new List<String>();
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                list.Add(line);
            }
        }
        catch (Exception ex)
        {
            //  Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }

        int x = list[0].Count();
        int y = list.Count();
        char[][] map = new char[x][];

        for (int i = 0; i < x; i++)
        {
            map[i] = new char[y];
        }

        for (int i = 0; i < list.Count(); i++)
        {
            String line = list[i];
            for (int j = 0; j < line.Count(); j++)
            {
                map[j][i] = line[j];
            }
        }
        return map;
    }


    public static char[][] ReadVisualMap(String fileName)
    {
        // pass the path to the file as a parameter 
        string filePath = Path.Combine(Environment.CurrentDirectory, "\\FilesToLoad\\Maps\\Visuals\\" + fileName + ".txt");


        //read the txt
        List<String> list = new List<String>();
        try
        {

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                list.Add(line);
            }
        }
        catch (Exception ex)
        {
            //Logger.getLogger(TxtReader.class.getName()).log(Level.SEVERE, null, ex);
        }

        int xSize = list[0].Count();
        int ySize = list.Count();

        // create the map
        char[][] visualMap = new char[xSize][];
        for (int i = 0; i < xSize; i++)
        {
            visualMap[i] = new char[ySize];
        }

        // fill the map
        for (int i = 0; i < ySize; i++)//y 
        {
            String line = list[i];
            for (int j = 0; j < xSize; j++)//x 
            {
                visualMap[j][i] = line[j];
            }
        }

        return visualMap;
    }
}
