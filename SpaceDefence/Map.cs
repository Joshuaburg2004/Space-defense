using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public class Map : GameObject
    {
        private Texture2D Background = null;
        public int Width {get; set;}
        public int Height {get; set;}
        public Map(int width, int height)
        {
            Width = width; 
            Height = height;
        }
        public void SetBackground(string name)
        {
            Background = GameManager.GetGameManager()._content.Load<Texture2D>(name);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Background != null) 
                spriteBatch.Draw(Background, new Rectangle(0, 0, Width, Height), new Rectangle(-Width, -Height, Width, Height), Color.White);
        }

        public Point GetCenter()
        {
            return new(Width / 2, Height / 2);
        }

        public bool Contains(Vector2 p)
        {
            return p.X < Width && p.Y < Height && p.X > 0 && p.Y > 0;
        }
    }
}
