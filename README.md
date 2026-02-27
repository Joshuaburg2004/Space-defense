Space defense repository - first project.

TODO: 
Add player movement:

[X]    In space, momentum is conserved. When the player presses one of the WASD keys, accelerate the ship in the matching direction. Make sure the player accelerates with the same speed when moving diagonally and when moving orthogonally (vertical or horizontal). (1p)
[X]    Rotate the space ship in the direction it last accelerated. (1p)

Add movement to the enemies:

[ ]    Make the Aliens chase the player. Every time the alien dies, a new alien spawns that should move faster than the previous. (.5p)
[ ]    When the alien comes within a certain range of the player, the player is game over. (.5p)

Collision:

In the Collision folder are several collider classes for different shapes. Rectangles, circles and line pieces, but the intersection logic is still missing. The Circle collider describes a circle using a location, described by 2 floats for the X and Y coordinates respectively and a radius. Everything that is less than the Radius away from the circle is considered to be within the circle. In the CircleCollider class add:

[ ]    Logic to calculate the intersection between two circles in the Intersects(CircleCollider other) method. (1p)
[ ]    Logic to calculate the intersection between a circle and a rectangle in the Intersects(RectangleCollider other) method. (1.5p)

A LinePiece is described by two Vector2: Start and End. Everything that is on the line between Start and End is on the Line. In the LinePieceCollider class add:

[X]    Logic to calculate the intersection between a line piece and a circle in the Intersects(CircleCollider other) method. (Hint: start by implementing the NearestPointOnLine method) (1.5p)
[X]    Logic to calculate the intersection between a line piece and a rectangle in the Intersects(RectangleCollider other) method. (Hint: start by implementing the standard line formula method) (2p)
