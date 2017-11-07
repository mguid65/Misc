This is a super force mod added to Alexander Blade's native trainer for 
GTAV.

How it works:

1. Entities have a variable that determines whether they have been damaged by
the player.

2. An array of size 1024 holds all vehicles closest to the player. If a 
vehicle has been damaged by the player, a force of 1000 is applied to 
the vehicle entity in the vector direction of the player camera. 

Pedestrian damage is kept track of similarly. The pedestrian entity negates
force over a certain amount so a force of 10 is applied to a pedestrian entity. 
