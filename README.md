# Academy2021Assignment

## Improvements to be done and features to be added
* Some sort of main menu or title screen would be nice, if this was a game to be played on mobile.
* Similarly, an ability to pause the game should probably be added.
* The game could have a high score table accessible from the main menu, to keep track of your best runs in the game. Maybe an ability to share said scores could also be added?
* The spawning algorithm is kind of lame... The collectibles and obstacles are always an equal space apart. It's not very fun, and could be improved.
* Additionally the code that spawns the initial obstacle is frankly terrible. It should be replaced alongside the spawning system refactor.
* Some of the obstacles aren't all that engaging... An enemy designer I am not.
* I'd probably switch the game idea from counting collected stars to counting passed obstacles. The stars spawn infrequently, so passed obstacles would make for a better indication of progression.
* Different kinds of collectables could be included; the spawning system was written to be flexible, so other player state modifiers besides just colour could be added fairly easily.
* The system for picking the colours is not really all that flexible. Right now the colours need to be individually reset in all the objects that can be coloured (player, obstacle pieces etc.). It'd be better to have a global set of colours, easily modifiable. This'd also add possibilities for accessibility options, not just easier development! 

## Known bugs and other miscellany
Some of the game's functions were designed around the limited scope, but could cause issues in the future:

* The player's position on screen is tracked using a collider around the camera view, that kills the player upon exiting the volume. If the player happens to get above the camera, the collider will also kill the player. This doesn't happen within this prototype, but could happen if someone was to tweak the camera functionality.
* The camera shake alters the camera's Y position. Because of the aforementioned behaviour, the camera shake could actually cause the player to fall outside the camera's boundaries (above or below) and get killed. Again, within this project, the camera shake only happens after death, so it's not a problem, but might cause unexpected behavior if the project was to be continued.
* There's a low chance that the game hangs when the player's colour is changed, because of the while loop. There's an insignificant chance that the randomizer would pick the same number over and over again, but I'm relying on the side of how mathemathically impossible it is for this to happen realistically.
* The game's main design around actually moving upwards is not great, there's a chance the player will eventually reach a killscreen when some of the positions reach inf in size.
* Something in the system is causing performance problems, I'm guessing it's the polygon colliders used for the arc pieces. The performance on desktop is negligible, but I'm not sure how good it would be on mobile. The scripts themselves also indicate issues in the profiler, so some optimization would be ahead for a non-prototype version.