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
        /// <returns>A Matrix representing the camera's transformation.</returns>
        public Matrix GetTransformMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0.0f)) *
                   Matrix.CreateRotationZ(rotation) *
                   Matrix.CreateScale(zoom, zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Width / 2.0f, GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Height / 2.0f, 0.0f));
        }

        /// <summary>
        /// Adjusts the camera position and ensures it stays within the map boundaries.
        /// </summary>
        /// <param name="newPosition">The new position to set.</param>
        public void AdjustPosition(Vector2 newPosition)
        {
            // Calculate the camera bounds
            float halfViewportWidth = GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Width / (2.0f * zoom);
            float halfViewportHeight = GameManager.GetGameManager().Game.GraphicsDevice.Viewport.Height / (2.0f * zoom);

            // Clamp the position to ensure the camera stays within the map
            float clampedX = MathHelper.Clamp(newPosition.X, halfViewportWidth, Level.GetCurrentLevel().LevelMap.Width - halfViewportWidth);
            float clampedY = MathHelper.Clamp(newPosition.Y, halfViewportHeight, Level.GetCurrentLevel().LevelMap.Height - halfViewportHeight);

            Position = new Vector2(clampedX, clampedY);
        }

        public Point ScreenToWorld(Point screenPosition)
        {
            // Get the viewport dimensions
            var viewport = GameManager.GetGameManager().Game.GraphicsDevice.Viewport;

            // Adjust the screen position to be relative to the camera's center
            Vector2 adjustedScreenPosition = screenPosition.ToVector2();
            adjustedScreenPosition -= new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);

            // Apply the inverse of the camera's transformations
            adjustedScreenPosition /= zoom; // Undo zoom
            adjustedScreenPosition = Vector2.Transform(adjustedScreenPosition, Matrix.CreateRotationZ(-rotation)); // Undo rotation
            adjustedScreenPosition += position; // Undo translation

            return adjustedScreenPosition.ToPoint();
        }
    }
}