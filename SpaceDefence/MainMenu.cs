using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    internal class MainMenu : Menu
    {
        public MainMenu()
        {
            _options = ["START GAME", "QUIT GAME"];
        }
        public void HandleInput(InputManager im)
        {
            if (im.IsKeyPress(Keys.Up) || im.IsKeyPress(Keys.W))
                _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
            if (im.IsKeyPress(Keys.Down) || im.IsKeyPress(Keys.S))
                _selectedIndex = (_selectedIndex + 1) % _options.Length;
            
            if (im.IsKeyPress(Keys.Enter)) {
                if (_selectedIndex == 0) GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                else GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}