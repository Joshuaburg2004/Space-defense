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