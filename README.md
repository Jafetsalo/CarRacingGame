This is a nano project featuring a car race and police pursuit. This is a code review:

We created a list of picture boxes, one for our moving elements and another for our enemies

Inside the Form1 constructor we added the instructions to iterate, find and add the picture boxes to our lists

The class method moveLine for Form1 does two things. 1. It updates the labels for gameSpeed and steerSpeed. 2. It reframes moving elements in the picture box list

Explaining function 2: we run a foreach cycle on each item of the picture box list for moving elements, then if it's moving positively (speed > 0) we compare it's current position making sure it stays within frame, and we move it to the top when it reaches the end of the frame.

Every tick (roughly every 25 miliseconds) we run moveLine sending it the gameSpeed as parameter, and the tick event executes a number of functions to control gameSpeed and steerSpeed in order to simulate driving minimal physics.  

