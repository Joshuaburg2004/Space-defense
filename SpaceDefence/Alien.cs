using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    internal class Alien : GameObject
    {
        private CircleCollider _circleCollider;
        private Texture2D _texture;
        private float playerClearance = 100;
        private int version = 0;
        private float accelerationRate = 50f;
        private float[] maxSpeed = [0f, 50f, 100f, 150f, 200f, 250f, 350f];
        private Vector2 velocity = Vector2.Zero;

        public Alien() 
        {
            
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            _texture = content.Load<Texture2D>("Alien");
            _circleCollider = new CircleCollider(Vector2.Zero, _texture.Width / 2);
            SetCollider(_circleCollider);
            RandomMove();
        }

        public override void OnCollision(GameObject other)
        {
            RandomMove();
            // Set the new max speed to the new version. Never go faster than player
            version = Math.Clamp(version + 1, 0, 6);
            if (version == 6) { 
                accelerationRate = Math.Clamp(accelerationRate * 2, 0, 350); 
            }
            base.OnCollision(other);
        }

        public void RandomMove()
        {
            GameManager gm = GameManager.GetGameManager();
            _circleCollider.Center = gm.RandomScreenLocation();

            Vector2 centerOfPlayer = gm.Player.GetPosition().Center.ToVector2();
            while ((_circleCollider.Center - centerOfPlayer).Length() < playerClearance)
                _circleCollider.Center = gm.RandomScreenLocation();
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
            float currentMaxSpeed = maxSpeed[version];
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
