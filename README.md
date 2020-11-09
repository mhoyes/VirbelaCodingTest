# VirbelaCodingTest
A Unity Project for a coding test at Virbela

## Questions ##

--------------------------------------------
1. How can your implementation be optimized?
--------------------------------------------
I feel like the FindClosestObject algorithm could optimized, although I'm not clear as to how just yet.
It seems efficient enough for the task at hand, as I had over 1000 "Thing" and "Bot" objects at one time to test and it functioned
smoothly, and fast.

I would gladly be open to thoughts and ideas to optimize it however.

------------------------------------------------------
2. How much time did you spend on your implementation?
------------------------------------------------------
I honestly spent a few hours on it, as I wanted to be thorough and cover everthing, as well as double check, and also document the code.
I originally pushed an initial changelist to the repo just as a barebones Unity Project before I began, however from that point on, weekend plans took
effect and I wasn't able to get the time to focus on this. I knew that I would find the time though, and from the time I actually began working, it
took a few hours to cover all my bases on the requirments, answering these questions included.

-------------------------------------
3. What was most challenging for you?
-------------------------------------
I wouldn't classify it as most challenging for me, but with this test being in Phases, each with Functional Goals, I took a methodical approach to it.
Although I could read the Functional Goals for the each Phase beforehand, I programmed it following each Phase accordingly, starting only with "Thing"
objects. When "Bot" objects were introduced in Phase 2, at that point I could a step back and knew "Bot" objects would share most functionality with
"Thing" objects, other than their color difference primarily. With that being said, I then stripped the code from "Thing" and created an abstract
Base Class with which both inherit from, implementing most of the common functionality in the Base Class. From there, it was just a matter of implementing
the primary difference within both "Thing" and "Bot" in their own classes accordingly; That being their color.

The "Player" class then required modification slightly to store the Base Class and act on it when finding the closest object, since this allows both "Thing"
and "Bot" objects to be accounted for within the algorithm.

I also made each "Thing" and "Bot" hold their own responsibility to deactivate (return to their original color) when not closest to the Player, or active
(change to red, or blue, respectively) when closest to the Player. This approach provided a much more clean implementation so that all classes hold
responsibility within themselves.
