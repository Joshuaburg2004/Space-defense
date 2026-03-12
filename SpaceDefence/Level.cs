namespace SpaceDefence
{
    public class Level
    {
        public int StartingVersion { get; set; }
        public Map LevelMap { get; private set; }

        public Level(Map levelMap, int startingVersion)
        {
            LevelMap = levelMap;
            StartingVersion = startingVersion;
        }

        public Level(int Width, int Height, int startingVersion)
        {
            LevelMap = new Map(Width, Height);
            StartingVersion = startingVersion;
        }
    }
}
