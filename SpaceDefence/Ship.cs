using SpaceDefence.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceDefence
{
    public class Ship : GameObject
    {
        private Texture2D ship_body;
        private Texture2D base_turret;
        private Texture2D laser_turret;
        private float buffTimer = 100;
        private float buffDuration = 10f;
        private RectangleCollider _rectangleCollider;
        private Point target;
        private Vector2 velocity;
        private float accelerationRate = 25f;
        private float maxSpeed = 200f;

        /// <summary>
        /// The player character
        /// </summary>
        /// <param name="Position">The ship's starting position</param>
        public Ship(Point Position)
        {
            _rectangleCollider = new RectangleCollider(new Rectangle(Position, Point.Zero));
            SetCollider(_rectangleCollider);
        }

        public override void Load(ContentManager content)
        {
            // Ship sprites from: https://zintoki.itch.io/space-breaker
            ship_body = content.Load<Texture2D>("ship_body");
            base_turret = content.Load<Texture2D>("base_turret");
            laser_turret = content.Load<Texture2D>("laser_turret");
            _rectangleCollider.shape.Size = ship_body.Bounds.Size;
            _rectangleCollider.shape.Location -= new Point(ship_body.Width/2, ship_body.Height/2);
            base.Load(content);
        }


        public override void HandleInput(InputManager inputManager)
        {
            base.HandleInput(inputManager);
            target = inputManager.CurrentMouseState.Position;
            // Check W, A, S and D, adjust momentum accordingly
            Vector2 acceleration = new Vector2();
            if(inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                acceleration.Y -= 1;
            }
            if(inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                acceleration.Y += 1;
            }
            if(inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                acceleration.X -= 1;
            }
            if(inputManager.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                acceleration.X += 1;
            }
            if (acceleration != Vector2.Zero) { acceleration.Normalize(); }
            velocity += acceleration * accelerationRate;
            float maxSpeedSquared = maxSpeed * maxSpeed;
            if (velocity.LengthSquared() > maxSpeedSquared)
            {
                velocity = Vector2.Normalize(velocity) * maxSpeed;
            }
            if(inputManager.LeftMousePress())
            {
                Vector2 aimDirection = LinePieceCollider.GetDirection(GetPosition().Center, target);
                Vector2 turretExit = new Point(_rectangleCollider.shape.Left, _rectangleCollider.shape.Top).ToVector2() + aimDirection * base_turret.Height / 2f;
                if (buffTimer <= 0)
                {
                    GameManager.GetGameManager().AddGameObject(new Bullet(turretExit, aimDirection, 150));
                }
                else
                {
                    GameManager.GetGameManager().AddGameObject(new Laser(new LinePieceCollider(turretExit, target.ToVector2()),400));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Update the Buff timer
            if (buffTimer > 0)
                buffTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Apply velocity
            Vector2 newPosition = _rectangleCollider.shape.Location.ToVector2();
            newPosition += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Ensure Ship stays on screen
            newPosition.X = MathHelper.Clamp(newPosition.X, 0, GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Width - _rectangleCollider.shape.Width);
            newPosition.Y = MathHelper.Clamp(newPosition.Y, 0, GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Height - _rectangleCollider.shape.Height);
            // New position
            _rectangleCollider.shape.Location = newPosition.ToPoint();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //void SpriteBatch.Draw(, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
            float angle = (float)Math.Atan2(velocity.Y, velocity.X) + (float)Math.PI / 2.0f;
            spriteBatch.Draw(ship_body, _rectangleCollider.shape, null, Color.White, angle, ship_body.Bounds.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            float aimAngle = LinePieceCollider.GetAngle(LinePieceCollider.GetDirection(GetPosition().Center, target));
            if (buffTimer <= 0)
            {
                Rectangle turretLocation = base_turret.Bounds;
                turretLocation.Location = new Point(_rectangleCollider.shape.Left, _rectangleCollider.shape.Top);
                spriteBatch.Draw(base_turret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            }
            else
            {
                Rectangle turretLocation = laser_turret.Bounds;
                turretLocation.Location = new Point(_rectangleCollider.shape.Left, _rectangleCollider.shape.Top);
                spriteBatch.Draw(laser_turret, turretLocation, null, Color.White, aimAngle, turretLocation.Size.ToVector2() / 2f, SpriteEffects.None, 0);
            }
            base.Draw(gameTime, spriteBatch);
        }


        public void Buff()
        {
            buffTimer = buffDuration;
        }

        public Rectangle GetPosition()
        {
            return _rectangleCollider.shape;
        }
    }
}
