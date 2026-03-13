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
        protected static int maxVersion { get => maxSpeeds.Length - 1; }
        protected static float accelerationRate;
        protected static float[] accelerationRates;
        protected static float[] maxSpeeds;
        protected Vector2 velocity = Vector2.Zero;
        public override void OnCollision(GameObject other)
        {
            if (other is not Laser && other is not Bullet) { return; }
            RandomMove();
            // Set the new max speed to the new version.
            Version = Math.Clamp(Version + 1, 0, maxVersion);
            accelerationRate = accelerationRates[Math.Clamp((int)Math.Floor(Convert.ToDouble(Version / 2)), 0, accelerationRates.Length)];
            var cl = Level.GetCurrentLevel();
            cl.CurrentProgression++;
            base.OnCollision(other);
            cl.CheckWin();

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
        public static void SetAccelerationRates(float[] accRates)
        {
            accelerationRates = accRates;
        }
    }
}