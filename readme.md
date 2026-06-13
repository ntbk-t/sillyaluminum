# Silly Aluminum
After playing vintage story a bit, we joked around about how you could theoretically use bauxite to make aluminum.  The conversation
drifted to why it wasn't already in the game, and then the infinite research rabbit hole consumed us all...

We have escaped from the other side with the most Realistic Aluminum Making you've ever seen!! (at least that is possible in vintage story
without adding custom crafting recipes).

You too can refine bauxite into alumina (the Bayer Process:tm:), create sodium metal (it explodes in water :3) from saltwort plants,
create aluminum chloride with chlorine gas??? (the gas is in a bucket...), and combine them to create a tiny bit of aluminum powder
(the Deville Process:tm:).  We wanted to go with the original method using metallic potassium, but it turns out you. uh. cant do that.
Cool.

Notably we have NOT added electrolysis to the game, so this process is ever so slightly ahistorical. (The invention of electrolysis
is what allowed aluminum to be made in the first place).  It is possible though!  You just have to imagine that Jonas or whoever
discovered aluminum early with his magic powers.

# Building the mod
I would recommend using the dotnet cli to build and test the project!  That is what we used for development.

I am sorry for the evil makefile.... I am sorry.  If you have make installed you can run `make test` to launch your vintage story client
(assuming you have VINTAGE_STORY set in your environment variables).

# Future plans
The sodium part of this mod kind of ballooned out of control (mostly cause it was fun to make all the explosions), so it might eventually
be split out into a seperate mod?  It also might be fun to work on some custom crafting, but it's a little unclear how things like
sand filtering would work in vintage story (apparenty we need a rotary kiln? could you even do that with the tech in game??)
Please make an issue if you have ideas on how any of that would work!

We also haven't done all that much balancing work, so some number tweaking might be in order.  Feel free to suggest balancing changes!
It is meant to be difficult, though.  We will not remove any crafting steps.