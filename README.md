Space defense repository - first project.

TODO: 
Add player movement:

[X]    In space, momentum is conserved. When the player presses one of the WASD keys, accelerate the ship in the matching direction. Make sure the player accelerates with the same speed when moving diagonally and when moving orthogonally (vertical or horizontal). (1p)
[X]    Rotate the space ship in the direction it last accelerated. (1p)

Add movement to the enemies:

[X]    Make the Aliens chase the player. Every time the alien dies, a new alien spawns that should move faster than the previous. (.5p)
[X]    When the alien comes within a certain range of the player, the player is game over. (.5p)

Collision:

In the Collision folder are several collider classes for different shapes. Rectangles, circles and line pieces, but the intersection logic is still missing. The Circle collider describes a circle using a location, described by 2 floats for the X and Y coordinates respectively and a radius. Everything that is less than the Radius away from the circle is considered to be within the circle. In the CircleCollider class add:

[X]    Logic to calculate the intersection between two circles in the Intersects(CircleCollider other) method. (1p)
[X]    Logic to calculate the intersection between a circle and a rectangle in the Intersects(RectangleCollider other) method. (1.5p)

A LinePiece is described by two Vector2: Start and End. Everything that is on the line between Start and End is on the Line. In the LinePieceCollider class add:

[X]    Logic to calculate the intersection between a line piece and a circle in the Intersects(CircleCollider other) method. (Hint: start by implementing the NearestPointOnLine method) (1.5p)
[X]    Logic to calculate the intersection between a line piece and a rectangle in the Intersects(RectangleCollider other) method. (Hint: start by implementing the standard line formula method) (2p)


# 3 Space Defence 2
For this assignment we are going to continue with the space defence game, adding the finishing touches to turn Space Defence into a real game.

To expand the game we increase the playing area beyond the screen, we add more enemies, another weapon and a new mechanic, where the player can score by shipping goods between 2 planets.

All the sprites you need can be found in the assets folder, but you are also free to use your own!

### Functionality:
Implement the following functionality:

#### Add a start and pauze screen:
[X] Add a start screen to the game from where the player can at least start or quit the game. (0.5p)
[X] Add a pause screen from where the player can at least continue or quit the game. The game should still be visible in the background, but not be updated.  (0.5p)

#### Expand the level:
[X] Create a camera class that follows the player as it moves around the level and expand the play area. (1 p)

#### More enemies:
[ ] Add a new asteroid enemy that does not move, and destroys both the player and enemies on touch. (0.5p)
[ ] Spawn more enemies over time, slowly ramping up the difficulty. (0.5p)

#### Animations:
[ ] Implement a class that can play an animation using a sprite sheet. (0.5p)
[ ] It should at least be possible to adjust the speed of the animation and whether or not it loops in the class.
[ ] Whenever a player or an alien dies, play an explosion animation (0.5p)

#### Game Goal (HUD):
[ ] Add at least 2 planetes to the level. When the player goes to one planet, they pick up cargo. At the other planet they can drop it off. (0.5p)
[ ] Whenever the player drops off cargo they score points. The score should always be visible on screen. (HUD) (1p)
[ ] There should always be some indication of whether or not the player is carrying any cargo. (HUD) (1p)

#### New Weapon:
[ ] Extract a weapon class for the two current weapons that can be added to the ship as a component to the ship. (1p)
[ ] Add a third weapon, the player can pick up. (0.5)
[ ] Add some unique functionality to the weapon. You can deside what it does. It can for example be an explosion, or a lightning weapon that also hits nearby targets (1p)

#### Expand the Game:
Add something new to the game of your chosing. For example: 
-  More enemy types (.5-2p depending on complexity of the enemies)
-  Healthbars (~1p for healthbars that follow both enemies and the player)
-  Weapon upgrades (1-2p depending on depth)

Add a short description of what you added. You can get up to 2 points depending on complexity 
Added Levels to the game, with progression between levels being available and allowing for both more enemies per level or allowing for different speed modes depending on the level. Enemies share one speed list and share a speed ramping - if one enemy dies then all enemies speed up. Levels can be tracked in the Level class in the Levels list, which can be expanded.

You can get a maximum of 11 points for this assignment.

The final grade for Space defence is (your points for the first part + the second part) /2 with a maximum of 10
