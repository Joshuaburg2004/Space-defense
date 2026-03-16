using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    internal class GameOver : Menu
    {
        public GameOver() {
            _options = ["TRY AGAIN", "ACCEPT YOUR FATE"];
            _title = "THE ALIEN SEEMS TO HAVE BESTED YOU... NOW ACCEPT YOUR FATE";
        }
        public override void HandleInput(InputManager im)
        {
            base.HandleInput(im);
            if (im.IsKeyPress(Keys.Enter))
            {
                if (_selectedIndex == 0) 
                {
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                    Level.GetCurrentLevel().Start();
                }
                else if (_selectedIndex == 1) // Quit
                    GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}
