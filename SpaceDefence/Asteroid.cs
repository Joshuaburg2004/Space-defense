using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefence
{
    internal class Asteroid : GameObject
    {
        protected CircleCollider _circleCollider;
        protected Texture2D _texture;
        protected float playerClearance = 100;
        public Asteroid() { }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            // <a href="https://www.freepik.com/free-psd/isolated-dark-grey-asteroid-3d-render-space-rock_409843503.htm">Image by tohamina on Freepik</a>
            _texture = content.Load<Texture2D>("Asteroid");
            _circleCollider = new CircleCollider(Vector2.Zero, 50);
            SetCollider(_circleCollider);
            RandomMove();
        }

        public override void OnCollision(GameObject other)
        {
            if (other is not Laser && other is not Bullet) { return; }
            RandomMove();
            // Set the new max speed to the new version.
            base.OnCollision(other);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _circleCollider.GetBoundingBox(), Color.White);
            base.Draw(gameTime, spriteBatch);
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
