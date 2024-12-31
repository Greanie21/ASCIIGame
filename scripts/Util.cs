

//import java.io.File;
//import java.util.ArrayList;
//import java.util.List;
//import static pkmngame.InventoryManager.scan;
public class Util
{
    private static int xVisionMax = 7;
    private static int yVisionMax = 4;
    private static int tileCharSizeX = 12;
    private static int tileCharSizeY = 6;

    public static void DrawBoard(char[][] map, Person player)
    {
        ClearBoard();

        //Draw the top part of the board
        Console.Write(" ");
        for (int i = 0; i < xVisionMax * 2; i++)
        {
            Console.Write("_");
        }
        Console.WriteLine();

        //Draw the board
        for (int j = 0; j < map[0].Length; j++)
        {
            //Draw the left part of the board
            String line = "|";

            for (int i = 0; i < map.Length; i++)
            {
                if (PlayerIsNearThisPartOfTheMap(i, j, player))
                {
                    if (player.mapPosition[0] == i && player.mapPosition[1] == j)
                    {
                        line += "@";
                    }
                    else
                    {
                        line += map[i][j];
                    }
                }
            }

            //Draw the right part of the board
            line += "|";

            //if the line will be showed than print
            if (("||" == line) == false)
            {
                Console.WriteLine(line);
            }
        }

        //Draw the bottom part of the board
        Console.Write(" ");
        for (int i = 0; i < xVisionMax * 2; i++)
        {
            Console.Write("_");
        }
        Console.WriteLine();
    }

    public static void DrawVisualBoard(char[][] visualMap, Person player)
    {
        ClearBoard();

        //Draw the board
        for (int i = 0; i < visualMap[0].Length; i++)//y
        {
            String line = "";

            for (int j = 0; j < visualMap.Length; j++)//x
            {
                int xTile = (int)Math.Ceiling((j + 1.0f) / tileCharSizeX) - 1;
                int yTile = (int)Math.Ceiling((i + 1.0f) / tileCharSizeY) - 1;

                if (PlayerIsNearThisPartOfTheMap(xTile, yTile, player))
                {
                    if (player.mapPosition[0] == xTile && player.mapPosition[1] == yTile)
                    {
                        //desenhar o player
                        line += "@";
                    }
                    else
                    {
                        line += visualMap[j][i];
                    }
                }
            }

            //if the line will be showed than print
            if (("" == line) == false)
            {
                Console.WriteLine(line);
            }
        }
    }

    public static void ClearBoard()
    {
        for (int i = 0; i < 100; ++i)
            Console.WriteLine();
    }

    private static bool PlayerIsNearThisPartOfTheMap(int xCoordinate, int yCoordinate, Person player)
    {
        int distanceX = xCoordinate - player.mapPosition[0];
        if (distanceX < 0)
        {
            distanceX = distanceX * -1;
        }

        int distanceY = yCoordinate - player.mapPosition[1];
        if (distanceY < 0)
        {
            distanceY = distanceY * -1;
        }

        if (distanceX <= xVisionMax && distanceY <= yVisionMax)
        {
            return true;
        }
        return false;
    }

    public static bool TryParseInt(String value)
    {
        try
        {
            int.Parse(value);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public static List<DirectoryInfo> GetDirectoriesToShow()
    {
        List<DirectoryInfo> directoriesToShow = new List<DirectoryInfo>();

        string path = Path.Combine(Environment.CurrentDirectory, "FilesToLoad");
        DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();

        foreach (var directory in directories)
        {
            string directoryName = directory.Name;
            if (directoryName != "New Game" && directoryName != "Maps")
            {
                directoriesToShow.Add(directory);
            }
        }

        return directoriesToShow;
    }

    public static void PrintAndWait(String message)
    {
        ClearBoard();
        Console.WriteLine(message);
        Console.ReadLine();
    }


    public static int ReadUserNextKey()
    {
        try
        {
            char choiceChar = Console.ReadKey().KeyChar;

            return int.Parse(choiceChar.ToString());
        }
        catch (Exception e)
        {
            return -1;
        }
    }


    public static string ReadUserNextKeyString()
    {
        try
        {
            char choiceChar = Console.ReadKey().KeyChar;

            return choiceChar.ToString();
        }
        catch (Exception e)
        {
            return string.Empty;
        }
    }
}
