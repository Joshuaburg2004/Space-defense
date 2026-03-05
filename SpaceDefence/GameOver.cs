using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    internal class GameOver : Menu
    {
        public GameOver() {
            _options = ["ACCEPT YOUR FATE"];
            _title = "THE ALIEN SEEMS TO HAVE BESTED YOU... NOW ACCEPT YOUR FATE";
        }
        public void HandleInput(InputManager im)
        {            
            if (im.IsKeyPress(Keys.Enter)) {
                GameManager.GetGameManager().Game.Exit();
            }
        }
    }
}