public class PkmnGame
{

    /**
     * @param args the command line arguments
     */
    static char[][] map;
    static char[][] visualMap;
    static bool mapIsVisualMode = true;

    static PlayerDetails playerDetails;
    static Person[] npcs;

    static int boxHudSize = 40;
    static bool gameIsFinished = false;
    static FightClass fight = new FightClass();

    public void Start()
    {
        if (ChooseToCreateNewGame())
        {
            CreateNewGame();
        }
        else
        {
            String savedGame = SelectSavedGame();
            LoadGame(savedGame);
        }

        PlayGame();

        SaveGame();
    }


    static bool ChooseToCreateNewGame()
    {
        // Console.WriteLine("Game Started!");
        Util.ClearBoard();
        Console.WriteLine("Game Started!");
        Console.WriteLine("");

        String decision = "";
        while (decision == ("1") == false && decision == ("2") == false)
        {
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Load Game");

            decision = Util.ReadUserNextKey().ToString();

            Util.ClearBoard();
        }
        return (decision == "1");
    }

    static void CreateNewGame()
    {
        LoadGame("New Game");

        String newName = "My New Name";
        bool valid = false;
        List<DirectoryInfo> InvalidNamesList = Util.GetDirectoriesToShow();
        InvalidNamesList.Add(new DirectoryInfo("New Game"));
        InvalidNamesList.Add(new DirectoryInfo("Maps"));

        Util.ClearBoard();

        while (valid == false)
        {
            Console.WriteLine("Choose Your name:");

            newName = Console.ReadLine();

            if (newName != null && InvalidNamesList.Contains(new DirectoryInfo(newName)) == false)
            {
                //TODO? - check if newName is a valid filename
                valid = true;
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }

        playerDetails.player.name = newName;
    }

    static String SelectSavedGame()
    {
        String savedGame = "New Game";
        //String choiceNumber = "Temporary Text";
        int playerChoice = -1;

        List<DirectoryInfo> directoriesToShow = Util.GetDirectoriesToShow();

        bool playerMadeChoice = false;

        while (playerMadeChoice == false)
        {
            Console.WriteLine("Select a game to load:\n");
            int count;
            for (count = 0; count < directoriesToShow.Count; count++)
            {
                Console.WriteLine(count + 1 + " - " + directoriesToShow[count].Name);
            }
            Console.WriteLine("\n" + (count + 1) + " - New Game");

            playerChoice = Util.ReadUserNextKey();

            if (playerChoice != -1)
            {
                playerMadeChoice = true;
            }

        }

        if (playerChoice > 0 && playerChoice <= directoriesToShow.Count())
        {
            savedGame = directoriesToShow[playerChoice - 1].Name;
        }

        return savedGame;
    }

    static void LoadGame(String playerName)
    {
        playerDetails = new PlayerDetails();
        int npcCount = 4;
        npcs = new Person[npcCount];

        JsonManager.ReadJsonPlayerDetails(playerName, playerDetails);
        JsonManager.ReadJsonPerson(playerName, "Player", playerDetails.player);

        for (int i = 0; i < npcCount; i++)
        {
            Person npc = new Person();
            JsonManager.ReadJsonPerson(playerName, "Npc" + i, npc);

            npcs[i] = npc;
        }
    }

    static void PlayGame()
    {
        ReadMaps();

        while (gameIsFinished == false)
        {
            if (PlayerActionIsValid(Util.ReadUserNextKeyString()) == false)
            {
                Console.WriteLine("Please input an valid action");
                Console.WriteLine("Type /help for the valid commands list");
            }
        }
    }

    static bool PlayerActionIsValid(string movement)
    {
        int newX = playerDetails.player.mapPosition[0];
        int newY = playerDetails.player.mapPosition[1];
        switch (movement.ToLower())
        {
            case "w":
                newY--;
                if (newY < 0)
                {
                    return false;
                }
                break;
            case "a":
                newX--;
                if (newX < 0)
                {
                    return false;
                }
                break;
            case "s":
                newY++;
                if (newY > map[0].Length)
                {
                    return false;
                }
                break;
            case "d":
                newX++;
                if (newX > map.Length)
                {
                    return false;
                }
                break;
            case "/help":
                Console.WriteLine("w -> move North (Up)");
                Console.WriteLine("a -> move West  (Left)");
                Console.WriteLine("s -> move South (Down)");
                Console.WriteLine("d -> move East  (Right)");
                Console.WriteLine("/save -> save the game");
                Console.WriteLine("/exit -> exit the game");
                Console.WriteLine("/changeview -> alternate between graphic and simbolic map");
                Console.WriteLine("/tournament -> Start a tournament with the npcs in your map");
                return true;
            case "/save":
                SaveGame();
                Console.WriteLine("Game Saved");
                return true;
            case "/exit":
            case "/quit":
                gameIsFinished = true;
                return true;
            case "/changeview":
                mapIsVisualMode = !mapIsVisualMode;
                if (mapIsVisualMode)
                {
                    Util.DrawVisualBoard(visualMap, playerDetails.player);
                }
                else
                {
                    Util.DrawBoard(map, playerDetails.player);
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

    static bool MapTilesEvents(int newX, int newY)
    {
        char mapTile = map[newX][newY];
        if (mapTile == '*')
        {
            Move(newX, newY);
            return true;
        }
        if (mapTile == 'W')
        {
            Move(newX, newY);
            StepOnTallGrass();
            return true;
        }
        if (mapTile == '#')
        {
            return false;
        }

        String mapName = playerDetails.player.mapName;

        switch (mapName)
        {
            case "Pewter":
                switch (mapTile)
                {
                    case '1':
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '2':
                        ChangeMaps("Route101", 12, 1);
                        return true;
                    case '3':
                        Console.WriteLine("You Entered the Museum!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '4':
                        Console.WriteLine("You Entered a house!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '5':
                        Console.WriteLine("You Entered a house!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '6':
                        return TryToEnterTournament(mapName, 15, 16);
                    case '7':
                        Console.WriteLine("You Entered the store!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '8':
                        ChangeMaps("SleepCenterPewter", 6, 8);
                        return true;
                    case '9':
                        Console.WriteLine("You Entered a house!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                }
                break;
            case "Celadon":
                switch (mapTile)
                {
                    case '1':
                        ChangeMaps("Route204", 8, 1);
                        return true;
                    case '2':
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '3':
                        ChangeMaps("SleepCenterCeladon", 6, 8);
                        return true;
                    case '4':
                        return TryToEnterTournament(mapName, 20, 12);
                    case '5':
                        Console.WriteLine("You Entered a house!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '6':

                    case '7':
                        Console.WriteLine("You Entered the store!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                    case '8':
                    case '9':
                        Console.WriteLine("You Entered a house!");
                        Console.WriteLine("You changed maps!");
                        Console.WriteLine("Not Yet Implemented");
                        gameIsFinished = true;
                        return true;
                }
                break;
            case "Route101":
                if (mapTile == '1')
                {
                    ChangeMaps("Pewter", 20, 39);
                    return true;
                }
                else if (mapTile == '2')
                {
                    ChangeMaps("Route102", 1, 1);
                    return true;
                }
                break;
            case "Route102":
                if (mapTile == '1')
                {
                    ChangeMaps("Route101", 12, 38);
                    return true;
                }
                else if (mapTile == '2')
                {
                    PassTheDay();
                    ChangeMaps("Route201", 5, 10);
                    return true;
                }
                break;
            case "Route201":
                if (mapTile == '1')
                {
                    PassTheDay();
                    ChangeMaps("Route102", 20, 10);
                    return true;
                }
                else if (mapTile == '2')
                {
                    ChangeMaps("Route202", 1, 9);
                    return true;
                }
                break;
            case "Route202":
                if (mapTile == '1')
                {
                    ChangeMaps("Route201", 36, 4);
                    return true;
                }
                else if (mapTile == '2')
                {
                    ChangeMaps("Route203", 18, 37);
                    return true;
                }
                else if (mapTile == '3')
                {
                    Console.WriteLine("You changed maps!");
                    Console.WriteLine("Secret Route Not Yet Implemented");
                    gameIsFinished = true;
                    return true;
                }
                break;
            case "Route203":
                if (mapTile == '1')
                {
                    PassTheDay();
                    ChangeMaps("Route202", 20, 2);
                    return true;
                }
                else if (mapTile == '2')
                {
                    ChangeMaps("Route204", 11, 18);
                    return true;
                }
                else if (mapTile == '3')
                {
                    Console.WriteLine("You changed maps!");
                    Console.WriteLine("Route Not Yet Implemented");
                    Console.WriteLine("How the Fuck did you get here?");
                    gameIsFinished = true;
                    return true;
                }
                else if (mapTile == '4')
                {
                    Console.WriteLine("You changed maps!");
                    Console.WriteLine("Route Not Yet Implemented");
                    Console.WriteLine("How the Fuck did you get here?");
                    gameIsFinished = true;
                    return true;
                }
                break;
            case "Route204":
                if (mapTile == '1')
                {
                    PassTheDay();
                    ChangeMaps("Route203", 18, 14);
                    return true;
                }
                else if (mapTile == '2')
                {
                    ChangeMaps("Celadon", 22, 38);
                    return true;
                }
                break;
            case "SleepCenterPewter":
                return SleepCenterTileEvents(mapTile, mapName, 15, 26);
            case "SleepCenterCeladon":
                return SleepCenterTileEvents(mapTile, mapName, 20, 12);
        }
        return false;
    }

    static void ChangeMaps(String mapName, int x, int y)
    {
        playerDetails.player.mapName = mapName;
        playerDetails.player.mapPosition[0] = x;
        playerDetails.player.mapPosition[1] = y;
        ReadMaps();
    }

    static bool TryToEnterTournament(String mapName, int x, int y)
    {
        int remainingDays = playerDetails.day % 10;
        if (remainingDays == 0)
        {
            if (Tournament.StartTournament(npcs, mapName, true, playerDetails.player))
            {
                CallDrawMap();
            }
            else
            {
                Bed.Sleep(playerDetails.player, true);
                PassTheDay();

                ChangeMaps(mapName, x, y);
            }
            return true;
        }
        else
        {
            remainingDays = 10 - remainingDays;
            Console.WriteLine("Can only enter the Tournament every 10 days");
            Console.WriteLine("Days until next Tournament:" + remainingDays);
            return false;
        }
    }

    static bool SleepCenterTileEvents(char mapTile, String mapName, int x, int y)
    {
        if (mapTile == '1')
        {
            ChangeMaps(mapName, x, y);
            return true;
        }
        if (mapTile == '2')
        {
            Util.ClearBoard();
            Console.WriteLine("If you sleep the day will pass and you will recover your Spirits Strenght.");
            Console.WriteLine("Do you want to sleep?(Y/N)");

            String response = Util.ReadUserNextKeyString();
            if ("Y" == (response.ToUpper()))
            {
                Bed.Sleep(playerDetails.player, true);
                PassTheDay();

                ChangeMaps(mapName, x, y);
            }
            else
            {
                playerDetails.player.mapPosition[0] = 6;
                playerDetails.player.mapPosition[1] = 5;
                CallDrawMap();
            }
            return true;
        }
        return false;
    }

    static void Move(int newX, int newY)
    {
        playerDetails.player.mapPosition[0] = newX;
        playerDetails.player.mapPosition[1] = newY;

        CallDrawMap();
    }

    static void CallDrawMap()
    {
        if (mapIsVisualMode)
        {
            Util.DrawVisualBoard(visualMap, playerDetails.player);
        }
        else
        {
            Util.DrawBoard(map, playerDetails.player);
        }
    }

    static void StepOnTallGrass()
    {
        if (new Random().Next(1, 101) <= 10)
        {
            int difficulty = 0;
            switch (playerDetails.player.mapName)
            {
                case "Route101":
                    difficulty = 0;
                    break;
                case "Route102":
                case "Route201":
                    difficulty = 1;
                    break;
                case "Route202":
                    difficulty = 2;
                    break;
                default:
                    difficulty = 0;
                    break;
            }

            Person wild = CreateWildSpirit.WildEncounter(difficulty);
            if (fight.Fight(playerDetails.player, wild, true))
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
        map = TxtReader.ReadMap(playerDetails.player.mapName);
        visualMap = TxtReader.ReadVisualMap(playerDetails.player.mapName);

        CallDrawMap();
    }

    static void SaveGame()
    {
        String playerName = playerDetails.player.name;

        JsonManager.WriteJsonPerson(playerDetails.player, playerName, "Player");

        for (int i = 0; i < npcs.Length; i++)
        {
            JsonManager.WriteJsonPerson(npcs[0], playerName, "Npc" + i);
        }

        //save the day
        JsonManager.WriteJsonPlayerDetails(playerDetails, playerName);
    }

    static void PassTheDay()
    {
        playerDetails.day++;

        for (int i = 0; i < npcs.Length; i++)
        {
            PassPersonsDay(npcs[i], true);
        }
    }

    static void PassPersonsDay(Person person, bool isNpc)
    {
        Bed.Sleep(person, false);
        //TODO make the person pass the day doing things like:
        //like gain xp with its spirits 
        for (int i = 0; i < person.GetSpiritsCount(); i++)
        {
            int lvl = person.spirits[i].getLevel();
            int xpWon = ((lvl + 2) * 14 + (((3 * lvl) * (3 * lvl)) / 3));

            person.spirits[i].experience += xpWon;
            person.spirits[i].CheckIfSpiritLeveledUp();
        }

        if (isNpc)
        {
            //maybe change maps
        }
    }
}
