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
            _options = ["Resume Game", "Quit" ];
        }
        public void HandleInput(InputManager inputManager)
        {
            if (inputManager.IsKeyPress(Keys.Up) || inputManager.IsKeyPress(Keys.W))
                _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
            if (inputManager.IsKeyPress(Keys.Down) || inputManager.IsKeyPress(Keys.Up))
                _selectedIndex = (_selectedIndex + 1) % _options.Length;

            if (inputManager.IsKeyPress(Keys.Enter))
            {
                if (_selectedIndex == 0) // Resume Game
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                else if (_selectedIndex == 1) // Quit
                    GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}