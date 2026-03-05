using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    internal class MainMenu
    {
        private SpriteFont _font;
        private string[] _options = ["START GAME", "QUIT GAME"];
        private int _selectedIndex = 0;
        public void Load(ContentManager cm)
        {
            _font = cm.Load<SpriteFont>("DejaVu Sans");
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
        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                Color color = i == _selectedIndex ? Color.Yellow : Color.White;
                sb.DrawString(_font, _options[i], new Vector2(800, 400 + i * 50), color);
            }
        }
    }
}