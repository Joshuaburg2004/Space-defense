using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    internal class GameOver
    {
        private SpriteFont _font;
        private string[] _options = ["ACCEPT YOUR FATE"];
        private int _selectedIndex = 0;
        public void Load(ContentManager cm)
        {
            _font = cm.Load<SpriteFont>("DejaVu Sans");
        }
        public void HandleInput(InputManager im)
        {            
            if (im.IsKeyPress(Keys.Enter)) {
                GameManager.GetGameManager().Game.Exit();
            }
        }
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(_font, "THE ALIEN SEEMS TO HAVE BESTED YOU... A CHOICE NOW LIES BEFORE YOU:", new Vector2(80, 200), Color.White);
            for (int i = 0; i < _options.Length; i++)
            {
                Color color = i == _selectedIndex ? Color.Yellow : Color.White;
                sb.DrawString(_font, _options[i], new Vector2(800, 400 + i * 50), color);
            }
        }
    }
}