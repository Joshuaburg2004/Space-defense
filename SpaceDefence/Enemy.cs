using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    public abstract class Enemy : GameObject
    {
        public static int Version = 0;
        protected CircleCollider _circleCollider;
        protected Texture2D _texture;
        protected float playerClearance = 100;
        protected int maxVersion { get => maxSpeeds.Length - 1; }
        protected float accelerationRate;
        protected float[] accelerationRates = [50f, 100f, 150f];
        protected static float[] maxSpeeds;
        protected Vector2 velocity = Vector2.Zero;
        public override void OnCollision(GameObject other)
        {
            if (other is not Laser && other is not Bullet) { return; }
            RandomMove();
            // Set the new max speed to the new version. Never go faster than player
            Version = Math.Clamp(Version + 1, 0, maxVersion);
            accelerationRate = accelerationRates[Math.Clamp((int)Math.Floor(Convert.ToDouble(Version / 2)), 0, accelerationRates.Length)];
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
        public static void SetMaxSpeeds(float[] speeds)
        {
            maxSpeeds = speeds;
        }
    }
}