using Microsoft.Xna.Framework.Input;
namespace SpaceDefence {
    public class WinScreen : Menu { 
        public WinScreen() {
            _title = $"You have beaten level {Level.GetCurrentLevel().LevelNumber}! Choose one of the options below:";
            _options = ["CONTINUE TO THE NEXT LEVEL", "TRY AGAIN", "QUIT GAME"];
        }
       public override void HandleInput(InputManager im)
        {
            base.HandleInput(im);
            
            if (im.IsKeyPress(Keys.Enter)) {
                if (_selectedIndex == 0) 
                {
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                    Level.CurrentLevel++;
                }
                GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}
