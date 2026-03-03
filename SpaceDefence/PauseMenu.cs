using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    public class PauseMenu
    {
        private SpriteFont _font;
        private string[] _options = [ "Resume Game", "Quit to Main Menu" ];
        private int _selectedIndex = 0;

        public void Load(ContentManager content)
        {
            _font = content.Load<SpriteFont>("DejaVu Sans"); 
        }

        public void HandleInput(InputManager inputManager)
        {
            if (inputManager.IsKeyPress(Keys.Up))
                _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
            if (inputManager.IsKeyPress(Keys.Down))
                _selectedIndex = (_selectedIndex + 1) % _options.Length;

            if (inputManager.IsKeyPress(Keys.Enter))
            {
                if (_selectedIndex == 0) // Resume Game
                    GameManager.GetGameManager().gameState = GameManager.GameState.Playing;
                else if (_selectedIndex == 1) // Quit to Main Menu
                    GameManager.GetGameManager().gameState = GameManager.GameState.MainMenu;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                Color color = i == _selectedIndex ? Color.Yellow : Color.White;
                spriteBatch.DrawString(_font, _options[i], new Vector2(800, 400 + i * 50), color);
            }
        }
    }
}