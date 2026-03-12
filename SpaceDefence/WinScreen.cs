using Microsoft.Xna.Framework.Input;
namespace SpaceDefence {
    public class WinScreen : Menu { 
        private Level _currentLevel;
        public WinScreen(Level _currentLevel) {
            _title = $"You have beaten level {_currentLevel.LevelNumber}! Choose one of the options below:";
            _options = [$"CONTINUE TO THE NEXT LEVEL", "TRY AGAIN", "QUIT GAME"];
        }
       public override void HandleInput(InputManager im)
        {
            base.HandleInput(im);
            
            if (im.IsKeyPress(Keys.Enter)) {
                if (_selectedIndex == 0) GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                else GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}
