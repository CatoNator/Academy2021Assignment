# Academy2021Assignment

## Improvements to be done
* I admittedly did not think very far when writing the initial code, and a flappy bird system where the game world moves areounf the player would've probably been a better choice for the spawning system.

## Known bugs and other miscellany
Some of the game's functions were designed around the limited scope, but could cause issues in the future:

* The player's position on screen is tracked using a collider around the camera view, that kills the player upon exiting the volume. If the player happens to get above the camera, the collider will also kill the player. This doesn't happen within this prototype, but could happen if someone was to tweak the camera functionality.
* The camera shake alters the camera's Y position. Because of the aforementioned behaviour, the camera shake could actually cause the player to fall outside the camera's boundaries (above or below) and get killed. Again, within this project, the camera shake only happens after death, so it's not a problem, but might cause unexpected behavior if the project was to be continued.
* There's a low chance that the game hangs when the player's colour is changed, because of the while loop. There's an insignificant chance that the randomizer would pick the same number over and over again, but I'm relying on the side of how mathemathically impossible it is for this to happen realistically.