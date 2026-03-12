using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceDefence.Collision;

namespace SpaceDefence
{
    public class Ship : GameObject
    {
        private Texture2D ship_body;
        private Texture2D base_turret;
        private Texture2D laser_turret;
        private float buffTimer = 0;
        private float buffDuration = 10f;
        private RectangleCollider _rectangleCollider;
        private Rectangle drawRectangle;
        private Point target;
        private Vector2 velocity;
        private float accelerationRate = 30f;
        private float maxSpeed = 250f;
        private float angle = 0f;

        /// <summary>
        /// The player character
        /// </summary>
        public Ship()
        {
            
        }

        public void SetPosition(Point Position)
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
            _rectangleCollider.shape.Location -= new Point(
                ship_body.Width / 2,
                ship_body.Height / 2
            );
            drawRectangle = new Rectangle(_rectangleCollider.shape.Center, ship_body.Bounds.Size);

            base.Load(content);
        }

        public override void HandleInput(InputManager inputManager)
        {
            base.HandleInput(inputManager);

            GameManager gm = GameManager.GetGameManager();
            if (inputManager.IsKeyPress(Keys.Escape))
            {
                if (gm.gameState == GameManager.GameState.Playing)
                    gm.gameState = GameManager.GameState.Paused;
                else if (gm.gameState == GameManager.GameState.Paused)
                    gm.gameState = GameManager.GameState.Playing;
            }
            if (gm.gameState == GameManager.GameState.Playing)
            {
                target = gm.WorldCamera.ScreenToWorld(
                    inputManager.CurrentMouseState.Position,
                    gm.Game.GraphicsDevice.Viewport.Width,
                    gm.Game.GraphicsDevice.Viewport.Height
                );
                // Check W, A, S and D, adjust momentum accordingly
                Vector2 acceleration = new Vector2();
                if (inputManager.IsKeyDown(Keys.W))
                {
                    acceleration.Y -= 1;
                }
                if (inputManager.IsKeyDown(Keys.S))
                {
                    acceleration.Y += 1;
                }
                if (inputManager.IsKeyDown(Keys.A))
                {
                    acceleration.X -= 1;
                }
                if (inputManager.IsKeyDown(Keys.D))
                {
                    acceleration.X += 1;
                }
                if (acceleration != Vector2.Zero)
                {
                    acceleration.Normalize();
                    float targetAngle =
                        (float)Math.Atan2(acceleration.Y, acceleration.X) + (float)Math.PI / 2.0f;
                    float rotationSpeed = 0.15f;
                    float angleDifference = MathHelper.WrapAngle(targetAngle - angle);
                    angle += angleDifference * rotationSpeed;
                }
                velocity += acceleration * accelerationRate;
                float maxSpeedSquared = maxSpeed * maxSpeed;
                if (velocity.LengthSquared() > maxSpeedSquared)
                {
                    velocity = Vector2.Normalize(velocity) * maxSpeed;
                }
                if (inputManager.LeftMousePress())
                {
                    Vector2 aimDirection = LinePieceCollider.GetDirection(
                        GetPosition().Center,
                        target
                    );
                    Vector2 turretExit =
                        drawRectangle.Location.ToVector2() + aimDirection * base_turret.Height / 2f;
                    if (buffTimer <= 0)
                    {
                        GameManager
                            .GetGameManager()
                            .AddGameObject(new Bullet(turretExit, aimDirection, 350));
                    }
                    else
                    {
                        GameManager
                            .GetGameManager()
                            .AddGameObject(
                                new Laser(
                                    new LinePieceCollider(turretExit, target.ToVector2()),
                                    400
                                )
                            );
                    }
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
            // Ensure Ship stays on screen
            newPosition.X = MathHelper.Clamp(
                newPosition.X,
                0,
                GameManager.GetGameManager().CurrentLevel.LevelMap.Width - _rectangleCollider.shape.Width
            );
            newPosition.Y = MathHelper.Clamp(
                newPosition.Y,
                0,
                GameManager.GetGameManager().CurrentLevel.LevelMap.Height - _rectangleCollider.shape.Height
            );
            // Update collider position
            _rectangleCollider.shape.Location = newPosition.ToPoint();
            GameManager.GetGameManager().WorldCamera.AdjustPosition(newPosition);

            // Update the draw rectangle to match the collider's position
            drawRectangle.Location = _rectangleCollider.shape.Center;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                ship_body,
                drawRectangle,
                null,
                Color.White,
                angle,
                ship_body.Bounds.Size.ToVector2() / 2f,
                SpriteEffects.None,
                0
            );

            float aimAngle = LinePieceCollider.GetAngle(
                LinePieceCollider.GetDirection(GetPosition().Center, target)
            );
            if (buffTimer <= 0)
            {
                Rectangle turretLocation = base_turret.Bounds;
                turretLocation.Location = new Point(drawRectangle.Left, drawRectangle.Top);
                spriteBatch.Draw(
                    base_turret,
                    turretLocation,
                    null,
                    Color.White,
                    aimAngle,
                    turretLocation.Size.ToVector2() / 2f,
                    SpriteEffects.None,
                    0
                );
            }
            else
            {
                Rectangle turretLocation = laser_turret.Bounds;
                turretLocation.Location = new Point(drawRectangle.Left, drawRectangle.Top);
                spriteBatch.Draw(
                    laser_turret,
                    turretLocation,
                    null,
                    Color.White,
                    aimAngle,
                    turretLocation.Size.ToVector2() / 2f,
                    SpriteEffects.None,
                    0
                );
            }

            base.Draw(gameTime, spriteBatch);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Alien)
            {
                GameManager.GetGameManager().gameState = GameManager.GameState.GameOver;
            }
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
