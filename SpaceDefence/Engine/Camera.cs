using Microsoft.Xna.Framework;

namespace SpaceDefence
{
    public class Camera
    {
        private Vector2 position;
        private float zoom;
        private float rotation;

        public Camera()
        {
            position = Vector2.Zero;
            zoom = 1.0f;
            rotation = 0.0f;
        }

        /// <summary>
        /// The position of the camera in the game world.
        /// </summary>
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        /// <summary>
        /// The zoom level of the camera. Values greater than 1 zoom in, values less than 1 zoom out.
        /// </summary>
        public float Zoom
        {
            get => zoom;
            set => zoom = MathHelper.Clamp(value, 0.1f, 10.0f); // Prevent extreme zoom levels
        }

        /// <summary>
        /// The rotation of the camera in radians.
        /// </summary>
        public float Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        /// <summary>
        /// Calculates the transformation matrix for the camera.
        /// </summary>
        /// <param name="viewportWidth">The width of the viewport (screen).</param>
        /// <param name="viewportHeight">The height of the viewport (screen).</param>
        /// <returns>A Matrix representing the camera's transformation.</returns>
        public Matrix GetTransformMatrix(int viewportWidth, int viewportHeight)
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
                   Matrix.CreateRotationZ(rotation) *
                   Matrix.CreateScale(zoom, zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(viewportWidth / 2.0f, viewportHeight / 2.0f, 0.0f));
        }
        public void AdjustPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
        public Point ScreenToWorld(Point screenPosition, int viewportWidth, int viewportHeight)
        {
            // Invert the camera's transformation matrix
            var inverseTransform = Matrix.Invert(GetTransformMatrix(viewportWidth, viewportHeight));

            // Transform the screen position into world coordinates
            return Vector2.Transform(screenPosition.ToVector2(), inverseTransform).ToPoint();
        }
    }
}