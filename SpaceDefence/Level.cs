using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceDefence
{
    public class Level : IEquatable<Level>
    {
        public static int CurrentLevel = 0;
        public int LevelNumber { get; set; }
        public int NumberOfEnemies { get; set; }
        public float[] Speeds { get; set; }
        public Map LevelMap { get; private set; }
        public static List<Level> Levels = new()
        {
            new (1, 2000, 2000, [150f, 200f, 250f, 350f, 400f, 500f], 1)
        };

        public Level(int levelNumber, Map levelMap, float[] speeds, int numberOfEnemies)
        {
            LevelNumber = levelNumber;
            LevelMap = levelMap;
            Speeds = speeds;
            NumberOfEnemies = numberOfEnemies;
        }

        public Level(int levelNumber, int Width, int Height, float[] speeds, int numberOfEnemies)
        {
            LevelNumber = levelNumber;
            LevelMap = new Map(Width, Height);
            Speeds = speeds;
            NumberOfEnemies = numberOfEnemies;
        }

        public void Start()
        {
            GameManager gm = GameManager.GetGameManager();
            gm.Reset();
            gm.AddGameObject(gm.Player);
            gm.Player.SetPosition(LevelMap.GetCenter());
            Enemy.Version = 0;
            Enemy.SetMaxSpeeds(Speeds);
            for (int _ = 0; _ < NumberOfEnemies; _++)
            {
                gm.AddGameObject(new Alien());
            }
            gm.AddGameObject(new Supply());
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
            if (level1 == null)
            {
                if (level2 == null) return true;
                return false;
            }
            return level1.Equals(level2);
        }
        public static bool operator != (Level level1, Level level2)
        {
            if (level1 == null)
            {
                if (level2 == null) return false;
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
