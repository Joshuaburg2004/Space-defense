using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public abstract class Enemy : GameObject
    {
        protected CircleCollider _circleCollider;
        protected Texture2D _texture;
        protected float playerClearance = 100;
        protected int version = 0;
        protected int maxVersion { get => maxSpeeds.Length - 1; }
        protected float accelerationRate = 50f;
        protected float[] maxSpeeds;
        protected Vector2 velocity = Vector2.Zero;
        public override void OnCollision(GameObject other)
        {
            if (other is not Laser && other is not Bullet) { return; }
            RandomMove();
            // Set the new max speed to the new version. Never go faster than player
            version = Math.Clamp(version + 1, 0, maxVersion);
            if (version == maxVersion) { 
                accelerationRate = Math.Clamp(accelerationRate * 2, 0, 350f); 
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
    }
}