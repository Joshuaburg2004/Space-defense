using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    public class PauseMenu : Menu
    {
        public PauseMenu()
        {
            _options = ["RESUME GAME", "TRY AGAIN", "QUIT" ];
        }
        public override void HandleInput(InputManager im)
        {
            base.HandleInput(im);
            if (im.IsKeyPress(Keys.Enter))
            {
                if (_selectedIndex == 0) // Resume Game
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                if (_selectedIndex == 1) 
                {
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                    Level.GetCurrentLevel().Start();
                }
                else if (_selectedIndex == 2) // Quit
                    GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}