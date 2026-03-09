using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceDefence
{
    public abstract class Menu
    {
        protected SpriteFont _font;
        protected string[] _options;
        #nullable enable
        protected string? _title;
        #nullable disable
        protected int _selectedIndex = 0;
        public void Load(ContentManager cm)
        {
            _font = cm.Load<SpriteFont>("DejaVu Sans");
        }
        public void Draw(SpriteBatch sb)
        {
            Camera c = GameManager.GetGameManager().WorldCamera;
            if (_title != null)
            {
                Point screenPosition = new Vector2(80, 200).ToPoint(); // Original screen position
                Vector2 worldPosition = c.ScreenToWorld(screenPosition, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height).ToVector2();

                sb.DrawString(_font, _title, worldPosition, Color.White);
            }
            for (int i = 0; i < _options.Length; i++)
            {
                Point screenPosition = new Vector2(800, 400 + i * 50).ToPoint(); // Original screen position
                Vector2 worldPosition = c.ScreenToWorld(screenPosition, sb.GraphicsDevice.Viewport.Width, sb.GraphicsDevice.Viewport.Height).ToVector2();

                // Draw the text at the world position
                Color color = i == _selectedIndex ? Color.Yellow : Color.White;
                sb.DrawString(_font, _options[i], worldPosition, color);
            }
        }
        public virtual void HandleInput(InputManager im)
        {
            if (im.IsKeyPress(Keys.Up) || im.IsKeyPress(Keys.W))
                _selectedIndex = (_selectedIndex - 1 + _options.Length) % _options.Length;
            if (im.IsKeyPress(Keys.Down) || im.IsKeyPress(Keys.S))
                _selectedIndex = (_selectedIndex + 1) % _options.Length;
        }
    }
}