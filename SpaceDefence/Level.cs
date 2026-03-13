using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceDefence
{
    public class Level : IEquatable<Level>
    {
        public static int CurrentLevel = 1;
        public int LevelNumber { get; set; }
        // Number of enemies on screen at a time
        public int NumberOfEnemies { get; set; }
        public float[] Speeds { get; set; }
        public float[] Rates { get; set; }
        public Map LevelMap { get; private set; }
        public int ProgressionLimit { get; set; }
        public int CurrentProgression { get; set; } = 0;
        public static List<Level> Levels = new()
        {
            new (1, 2000, 2000, [150f, 200f, 250f, 350f, 400f, 500f], [50f, 100f, 150f], 1, 7),
            new (2, 4000, 4000, [300f, 300f, 400f, 400f, 500f, 500f], [100f, 200f, 300f], 2, 5)
        };

        public Level(int levelNumber, Map levelMap, float[] speeds, float[] rates, int numberOfEnemies, int progressionLimit)
        {
            LevelNumber = levelNumber;
            LevelMap = levelMap;
            Speeds = speeds;
            NumberOfEnemies = numberOfEnemies;
            ProgressionLimit = progressionLimit;
            Rates = rates;
        }

        public Level(int levelNumber, int Width, int Height, float[] speeds, float[] rates, int numberOfEnemies, int progressionLimit)
        {
            LevelNumber = levelNumber;
            LevelMap = new Map(Width, Height);
            Speeds = speeds;
            NumberOfEnemies = numberOfEnemies;
            ProgressionLimit = progressionLimit;
            Rates = rates;
        }

        public void Start()
        {
            GameManager gm = GameManager.GetGameManager();
            gm.Reset();
            CurrentProgression = 0;
            gm.AddGameObject(gm.Player);
            gm.Player.SetPosition(LevelMap.GetCenter());
            Enemy.Version = 0;
            Enemy.SetMaxSpeeds(Speeds);
            Enemy.SetAccelerationRates(Rates);
            for (int _ = 0; _ < NumberOfEnemies; _++)
            {
                gm.AddGameObject(new Alien());
            }
            gm.AddGameObject(new Supply());
        }

        public void CheckWin()
        {
            if (CurrentProgression == ProgressionLimit)
            {
                GameManager gm = GameManager.GetGameManager();
                gm.gameState = GameManager.GameState.Won;
                gm._winScreen.SetIsEnd(IsLast(this));
            }
        }

        public static bool IsLast(Level level)
        {
            return level == Levels.Last();
        }

        public static Level GetCurrentLevel()
        {
            try
            {
                return Levels.First(x => x.LevelNumber == CurrentLevel);            
            }
            catch (InvalidOperationException)
            {
                return Levels.Last();
            }
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((Level)obj);
        }
        #nullable enable
        public bool Equals(Level? level)
        {
            return level != null && level.LevelNumber == LevelNumber;
        }
        #nullable disable

        public static bool operator ==(Level level1, Level level2)
        {
            if (level1 is null)
            {
                if (level2 is null) return true;
                return false;
            }
            return level1.Equals(level2);
        }
        public static bool operator != (Level level1, Level level2)
        {
            if (level1 is null)
            {
                if (level2 is null) return false;
                return true;
            }
            return !level1.Equals(level2);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
