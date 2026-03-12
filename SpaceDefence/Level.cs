namespace SpaceDefence
{
    public class Level
    {
        public Map LevelMap { get; private set; }

        public Level(Map levelMap)
        {
            LevelMap = levelMap;
        }
    }
}
