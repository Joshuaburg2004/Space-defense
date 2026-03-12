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
        public void SetBackground(string name, ContentManager content)
        {
            Background = content.Load<Texture2D>(name);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Background != null) spriteBatch.Draw(Background, new Rectangle(0, 0, Width, Height), Color.Black);
            base.Draw(gameTime, spriteBatch);
        }

        public Point GetCenter()
        {
            return new(Width / 2, Height / 2);
        }
    }
}