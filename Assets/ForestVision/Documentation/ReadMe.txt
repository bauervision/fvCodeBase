ForestVision 2.2 by BauerVision
----------------------

Thank you for purchasing ForestVision!  I hope you find as much delight in creating your scenes with these assets as I do.

----------------------
Please feel free to drop me a line on the forum page or on my Facebook page: https://www.facebook.com/Bauervision/

You can also email me directly at 

mikecbauervision@gmail.com

----------------------
One or more textures in this package have been created with photographs from Textures.com. These photographs may not be redistributed by default; please visit www.textures.com for more information.

Other textures altered from originals found at: http://www.texturemate.com/

----------------------
Best place to start with these assets is with the Asset Collection scene. At the top of your menu bar you should see the Tools menu, inside this you will find the Forestvision menu, and then inside that are the Tools, FBX Exporter and the now Deprecated Browser.

----------------------

Substances!

Now included are 3 main Substances to use, found in the 10 Extras folder, there is the Foliage Blender, Texture Adapter and Dual Channel Blender.

Foliage Blender: supply a Winter texture (bare branches), Spring (buds on the branches), Spring Flowers, and Summer leaves, and I will generate a fully operational material with a single slider to blend through all 4 seasons.

Texture Adapter: Provide the base color texture, and this Substance will produce a fully PBR Material for you. No need to store Normal, Height, AO and Metallic maps!

Dual Channel Blender: give it 2 different base colors and you can blend between them both!

All substances included several options for tweaking the material on the fly including adding water, snow, moss and turning the water into ice!

----------------------

The new FV Tools editor presents a collection of productivity tools that I use which help you create and adjust your creations.

Reset Transform helps you set the initial scale of your models to whatever size you desire.  Simply select whatever object you want to reset the transforms on ( you need to make sure it is centered to the world but scaled to your desired size), and I add a new empty game object as its parent.
From here you can press the New Optimized button to collpase your selection down into a single object.

New Optimized takes whatever you have selected and collpases it down to a single mesh, while maintaining the current materials. This is great for lowering draw calls. I'll add the ability to atlas the texture soon.

New Prefab, generates a new prefab for you based on what you have selected.

High Res Screenshot simply gives you a built in tool to take nice screenshots of your world.

Tree Tools is the latest tool to the Forestvision family.

There is now a button to quickly add the Branch Rotations script, and FV Items.

---------------------------------------------------------

Tree Tools

Note: it will crash if you try to generate branches on too complicated of a mesh. I apply a Mesh collider in order to ray trace to the surface, and this is a very processor intensive operation so be careful.  Only spawn on trunks.

If you desire to spawn branches, on branches I would recommend you pick very simple branches. But even more so I would recommend you simply make your branches by hand, and then save them as optimized meshes, and spawn them on trunks.


To use: add a branch type to the Branch Spawner, at least one is required. Set the number of branches with the "How Many?" slider. Note I've limited it to 5 at one time simply to keep things manageable. 
Per Branch Type checkbox will allow you to generate the number of branches, for each type you have loaded. So if you have 3 branches loaded, with How Many set to 2, then you will get 6 total branches. Without it checked,
you will only get 3.

Delete Old Branches on Generate checkbox, if checked will clear the current spawn variety with each press of the Generate buttons. Uncheck this and you will get more and more branches with each spawn.

Starting height simply offers a quick way of positioning the spawn cluster up and down the trunk.

Up X, Up Y and Up Z offer quick ways to alter the projection of the raytrace spawning. Up X checked means a postive vector, with a negative being loaded unchecked. Basically, this helps you
to place branches on both sides of the tree quickly.

Gen X, Gen Y and Gen Z. These buttons take what you have set so far and generates solely based on the given vector.
Generate on All Axis will take your current settings and generate on all 3 vectors at once.

Randomize Branches: simply takes what you have thus generated and randomizes the arrangement.

Clear Branches removes everything from your selected game object.

Rotate All Branches on the following Axis: offes you the ability to quickly offset the rotation of all the spawned children based on the parameters you set.

If you check "Rotate Just Selected" then the rotation offset will only be applied to what is currently selected.

Scale All Branches Uniformly: scales up or down all children based on the buttons pressed. Again, if Scale Just Selected is checked then it will not be applied to the children.

------------------------

If you have any questions or issues please don't hesitate to contact me right away!

Thank you again, I hope that these assets help you to fulfill your own ForestVision!


