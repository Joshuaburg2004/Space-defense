using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    internal class Alien : Enemy
    {
        #nullable enable
        public Alien(float[]? speeds = null) 
        {
            if (speeds == null) { maxSpeeds = [150f, 200f, 250f, 350f, 400f, 500f]; }
            else maxSpeeds = speeds;
        }
        #nullable disable

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("Alien");
            _circleCollider = new CircleCollider(Vector2.Zero, _texture.Width / 2);
            SetCollider(_circleCollider);
            RandomMove();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.White);
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameManager gm = GameManager.GetGameManager();
            Vector2 playerPosition = gm.Player.GetPosition().Center.ToVector2();
            Vector2 alienPosition = _circleCollider.Center;

            // Calculate direction to the player
            Vector2 direction = playerPosition - alienPosition;
            if (direction.Length() > 0)
                direction.Normalize();

            // Accelerate toward the player
            velocity += direction * accelerationRate * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Clamp velocity to max speed for the current version
            float currentMaxSpeed = maxSpeeds[Version];
            if (velocity.Length() > currentMaxSpeed)
            {
                velocity.Normalize();
                velocity *= currentMaxSpeed;
            }

            Vector2 newPosition = _circleCollider.Center;
            newPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Ensure Ship stays on screen
            newPosition.X = MathHelper.Clamp(newPosition.X, 0, GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Width);
            newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Height);
            // New position

            // Update position
            _circleCollider.Center = newPosition;
        }
    }
}
