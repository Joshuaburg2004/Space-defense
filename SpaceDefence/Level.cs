using System.Collections.Generic;
using System.Linq;

namespace SpaceDefence
{
    public class Level
    {
        public static int CurrentLevel = 0;
        public int LevelNumber { get; set; }
        public int NumberOfEnemies { get; set; }
        public int StartingVersion { get; set; }
        public Map LevelMap { get; private set; }
        public static List<Level> Levels = new()
        {
            
        };

        public Level(int levelNumber, Map levelMap, int startingVersion, int numberOfEnemies)
        {
            LevelNumber = levelNumber;
            LevelMap = levelMap;
            StartingVersion = startingVersion;
            NumberOfEnemies = numberOfEnemies;
        }

        public Level(int levelNumber, int Width, int Height, int startingVersion, int numberOfEnemies)
        {
            LevelNumber = levelNumber;
            LevelMap = new Map(Width, Height);
            StartingVersion = startingVersion;
            NumberOfEnemies = numberOfEnemies;
        }

        public void Start()
        {
            GameManager gm = GameManager.GetGameManager();
            gm.Player.SetPosition(LevelMap.GetCenter());
        }

        public static Level GetCurrentLevel()
        {
            return Levels.First(x => x.LevelNumber == CurrentLevel);
        }
    }
}
