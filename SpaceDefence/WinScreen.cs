using Microsoft.Xna.Framework.Input;
namespace SpaceDefence {
    public class WinScreen : Menu {
        public bool IsEnd = false;
        public WinScreen() { }
        public void SetIsEnd(bool isEnd)
        {
            IsEnd = isEnd;
            if (!isEnd)
            {
                _title = $"You have beaten level {Level.GetCurrentLevel().LevelNumber}! Choose one of the options below:";
                _options = ["CONTINUE TO THE NEXT LEVEL", "TRY AGAIN", "QUIT GAME"];
            }
            else
            {
                _title = "You have conquered the galaxy!";
                _options = ["TRY FINAL LEVEL AGAIN", "QUIT GAME"];
            }
        }
        public override void HandleInput(InputManager im)
        {
            base.HandleInput(im);
            GameManager gm = GameManager.GetGameManager();
            if (im.IsKeyPress(Keys.Enter)) {
                if (!IsEnd)
                {
                    if (_selectedIndex == 0 || _selectedIndex == 1) 
                    {
                        gm.gameState = GameManager.GameState.Playing;
                        if (_selectedIndex == 0 ) { Level.CurrentLevel++; }
                        SetIsEnd(Level.IsLast(Level.GetCurrentLevel()));
                        Level.GetCurrentLevel().Start();
                    }
                    else gm.Game.Exit();
                }
                else
                {
                    if (_selectedIndex == 0)
                    {
                        gm.gameState = GameManager.GameState.Playing;
                        SetIsEnd(Level.IsLast(Level.GetCurrentLevel()));
                        Level.GetCurrentLevel().Start();
                    }
                    else gm.Game.Exit();
                }
            }
        }
    }
}
