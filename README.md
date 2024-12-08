# Devlog Entry #-1 - 11/21/24
Tools/Design Lead: Jack
Engine/Design Lead: Jerry

## Tools and materials
# Tell us about what engines, libraries, frameworks, and or platforms you intend to use, and give us a tiny bit of detail about why your team chose those.

For the engine, we’ll be using Godot 4.2, as it is something that Jerry is familiar with and Jack is interested in exploring. We are using C# in Godot, as it is a common language in the industry, and comes with its fair share of challenges when it comes to implementation that will both further our learning within it and give reason to switch platforms should the need arise. As far as libraries or frameworks, none have really stood out to us as being necessary, but if any appear to be extremely useful then we’ll indicate their addition in future devlogs (as is the case for any additional components).

# Tell us programming languages (e.g. TypeScript) and data languages (e.g. JSON) your team expects to use and why you chose them. Presumably you’ll just be using the languages expected by your previously chosen engine/platform.

We are using C# in the Godot engine and planning on moving towards GDScript. Using C# is a benefit to users that are coming from Unity, which Jack and I both have experience in. We want to move towards GDScript because there is more support for it and C# doesn’t have support for web builds. This would further necessitate the need to switch to an alternate platform. It will also be interesting to learn how we can translate a Unity-like project into Godot and GDScript.

# Tell us about which tools you expect to use in the process of authoring your project. You might name the IDE for writing code, the image editor for creating visual assets, or the 3D editor you will use for building your scene. Again, briefly tell us why you made these choices. Maybe one of your teammates feels especially skilled in that tool or it represents something you all want to learn about.

We’ll be writing our code in VSCode, as it is familiar to us (being the norm for most classes at UCSC). For visual assets, we’ll be using Pixelorama, as it is very well integrated with Godot and will streamline the asset creation process, which is crucial in a programming-first project like this one. Any assets we use that we did not ourselves create will come from Kenney Assets, though it remains to be seen which specific packs we’ll be using. Lastly, we are using SonarLint to ensure clean code, since it seems like a solid all-around linter that is easy to set up.

# Tell us about your alternate platform choice. Remember, your alternate platform must differ from your primary platform by either changing the primary primarily language used or the engine/library/framework used for building your user interface.

Our alternate platform is going to be Godot with GDScript, we chose this because it would complement our learning in Godot. It will also allow us to focus our learning on data structures and keeping our code clean.

## Outlook
We’re going into this project knowing that it will not be viable throughout the course of this project, mostly because of the lack of C# browser support in Godot 4.2. As such, we are creating a learning opportunity for ourselves where we develop a framework that will have to be actively changed in order to fulfill the project requirements of mobile deployment. We planned for the platform change to be an active necessity.

# Devlog Entry #0 - 12/2/24

## How we satisfied the software requirements
For each of the F0 requirements, give a paragraph of explanation for how your game’s implementation satisfies the requirements.
Your team can earn partial credit for covering only a subset of the F0 requirements at this stage. (It is much better to satisfy the requirements in a sloppy way right now than lock in your partial credit.)
[F0.a] You control a character moving over a 2D grid.
	The game utilizes the arrow keys to move the character around the 2D grid. Each of these movements calls a function to increment time passed, as well as a function to shift the player's location on the board.
[F0.b] You advance time manually in the turn-based simulation.
	Time is advanced manually via performing actions (and is presented as a function call within the action function). Planting, cutting, and moving all advance time forward.
[F0.c] You can reap or sow plants on grid cells only when you are near them.
	Planting and cutting interacts with the cell upon which the player is present, so movement (and thus time advancement) is necessary to interact with multiple cells.
[F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
	Each time an action is performed and time advances, a random amount of water is added to the soil and a random amount of sun is generated per cell. The sun resets and is rerolled every time one of those actions is performed, while the water represents an aggregate of all water recieved over the plant's presence in the game.
[F0.e] Each plant on the grid has a distinct type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
	The plants have multiple levels of growth that are moved through based on the water and sun present.
[F0.f] Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
	If there is enough of both water and sun, the plant will move to the next level. Since these values are rerolled every time, the length of time is random. However, certain plant types require more or less resources to grow, thus making their average times different. Some plants can only grow with other plants of a certain type near them.
[F0.g] A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
	The point counter will display a "You Win!" message upon achieving 10 points, which are accumulated by harvesting fully grown plants.

## Reflection
Looking back on how you achieved the F0 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.

The holidays made it difficult to have an even workload, so it may be pertinent to stay more in the loop in the future in order to not get overwhelmed after a lot of work has been done. Additionally, we could better divvy up the work, which will be easier now that schedules are clearer and the project can take a clear priority over other life events. The chosen linter seems to have done a good job with keeping the code clean. When assets become more polished in the future, it could also be a good idea to push them through GitHub rather than share them through another service such as Google Drive.

# Devlog Entry #1 - 12/5/24

## How we satisfied the software requirements
Using a bulleted list, comment on how each of the 7 + 4 = 11 software requirements is satisfied. If the way your new game version satisfies a requirement is the same as how you described it before, you can simply write "same as last week" for that requirement.

For the F1.a requirement, your team must describe your byte array format as either Structure-of-Arrays or Array-of-Structures (or some mixture of these). You must also use an embedded image to illustrate your memory allocation strategy.

[F0.a] You control a character moving over a 2D grid.
	Same as last week.
[F0.b] You advance time manually in the turn-based simulation.
	Same as last week.
[F0.c] You can reap or sow plants on grid cells only when you are near them.
	Same as last week.
[F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
	Same as last week.
[F0.e] Each plant on the grid has a distinct type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
	Same as last week.
[F0.f] Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
	Same as last week.
[F0.g] A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
	Same as last week.
[F1.a] The important state of your game's grid must be backed by a single contiguous byte array in AoS or SoA format. If your game stores the grid state in multiple format, the byte array format must be the primary format (i.e. other formats are decoded from it as needed).
"![F1.a data structure diagram](./f1chartpng)"
	Because we made our undo/redo arrays store game information, the important state of our games grid is just the most recent element in the undo array
	Now that we know we only need to save our undo/redo arrays, we first store each array's size in their respective order, then the arrays themselves
	Each element in the arrays are comprised of the players location, the points, and the turns that I use to derive pseudorandom water spawns. this takes up the first 16 bytes
	The next 128 bytes are saved for the cells of the map, each containing the plant information. This is an integer array converted to a byte array.
[F1.b] The player must be able to manually save their progress in the game. This must allow them to load state and continue play another day (i.e. after quitting the game app). The player must be able to manage multiple save files/slots.
	We implemented several functions that produce new save files for each manual save. These can be accessed from a dropdown menu in-game. The game state is converted into a string, which is then stored in a text file. Each item in the drop down is assigned an ID according to the file number, and when a signal is sent out with that ID attatched, the loading functions return the text state to the actual game state.
[F1.c] The game must implement an implicit auto-save system to support recovery from unexpected quits. (For example, when the game is launched, if an auto-save entry is present, the game might ask the player "do you want to continue where you left off?" The auto-save entry might or might not be visible among the list of manual save entries available for the player to load as part of F1.b.)
	Every time an action is committed, the game state is saved to save_0.txt, which is subesquently overwritten every time an autosave occurs. This way, the save file will always have the most recent changes, and we won't have to sort through file dates and things like that. A button is available under the Manual Load drop down to load the most recent autosave.
[F1.d] The player must be able to undo every major choice (all the way back to the start of play), even from a saved game. They should be able to redo (undo of undo operations) multiple times.
	The game state byte array contains the present game state in addition to the current undo path. Even after loading a saved game, the state will still reflect that path, and can thus be undone all the way back to the start.

## Reflection
Looking back on how you achieved the new F1 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? Has your game design evolved now that you've started to think about giving the player more feedback? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.

Working in C# has definitely come with its fair share of challenges, such as a lack of online resources and documentation, or at the very least a clear difficulty in finding those resources. Seeing as how we will be switching to GDScript soon, we're glad we'll have the chance to refactor into something a bit more readable, in addition to the web capabilities present using GDScript rather than .NET/C#. Sonarlint may not work with GDScript in the same way, so a choice will have to be made in either manually upholding code standards or finding a linter that works with the language. As far as the course of F1, we found that we were able to split up each of the parts into clear segments for each of us to be in charge of, with Jack in charge of saves and autosaves, and Jerry in charge of byte array implementation and the undo sequence. Our communication was also a lot better since the holiday season was over, so the process was a lot more efficient. Looking ahead, we will continue splitting tasks up in this manner as we move into the latter half of this project, and keep looking for ways to improve both communication and collaboration.

# Devlog Entry #2 - 12/8/24

## How we satisfied the software requirements
### F0+F1

No major changes were made. The only new thing is tracking the number of currently planted plants for use in the external DSL.

### External DSL for Scenario Design

The external DSL is not based on a pre-existing data language, but is rather built to appear closer to javascript, with calls to pseudo-functions and some limited variable assignment with conditions. I felt that this was most familiar to me and I already had a clear idea of how to parse it from a .txt file, so it seemed like the logical way to go.
There is a set of values that can be changed by using “val” or “rul”. These are the maximum and minimum amounts of sun and water (sun_min, sun_max, water_min, water_max), points (points_earned), and the accumulation of sun and water (sun_accumulates, water_accumulates).
Conditions can be created by using “con”. These are assigned a name (e.g. below these are timePassedCondition and currentlyPlantedCondition, but they can have any name the author wishes), and are created using a pseudo-function call to “buildcondition”, in which you specify the attribute of the game state being tracked, and the value at which the condition will be satisfied. Available attributes are the amount of time passed (time_passed), amount of points (point_threshold), and number of plants growing at once (currently_growing).
Lastly, events can be created by using “eve”. These call a pseudo-function, “buildevent” that takes in the name of the given condition, a value to change, and the number to change that value to. Right now, buildevent can take points_earned as a value to change, and the number will increase the point amount by that much.

```
val sun_max = 0;
rul water_accumulates = false;
con timePassedCondition = buildcondition(time_passed, 4);
con currentlyPlantedCondition = buildcondition(currently_planted, 10);
eve buildevent(timePassedCondition, points_earned, 10);
eve buildevent(currentlyPlantedCondition, points_earned, 15);
end
```

This program sets the max sun production to 0 and makes it impossible for water to accumulate on plants. This guarantees no plants will grow. It then creates two conditions, one checking for if 4 turns have passed, and one checking if 10 plants exist at the same time. Then an event is created where if that 4-turn condition is satisfied, the player gains 10 points. After this, an event is created where if the 10-plant condition is satisfied, the player gains 15 points. Then, the phrase “end” indicates that the file is done.

### Internal DSL for Plants and Growth Conditions

This is an example of how to add a plant to the dictionary: plantTypes

```
{"name": "sunflower",
"sprite" : "Jerry/assets/PlantC.png",
"checkGrowth": func checkGrowth(plant : Plant, water : int,
 sun : int, cells : Array):
	if sun >= 8:
		plant.grow();
	plant.label.text = str("sun:", sun," water:",water);
}
```
In the internal DSL, you can use GDScript in a dictionary to define the plants’ names, sprites, and how they grow. Using GDScript allows for the user to define more complicated data types that are not supported in our external DSL. You are also able to look at the code and find workarounds to issues internally.

### Switch to Alternate Platform

To switch from C# to GDScript, we had to recreate the files that were attached to the nodes. We chose this change because we predicted it would  be smooth and GDScript supports web builds. The logic was able to be carried over, which made the change much quicker than writing it from scratch. Structs don’t exist in GDScript, so we had to replace them with classes and dictionaries.
Porting file saving from C# to GDScript was relatively simple, though the methods used to accessing, writing to, reading from, and creating these files was different between the two. Reading through the documentation made this fairly simple. Additionally, the new methods made it far easier to store save files in a way that would be consistent no matter who was running the game or where it was being run.

## Reflection

Maintaining clean code has been a bit more challenging without the linter made available by the popularity of the language we were previously working in. Additionally, we are now using different IDEs according to our familiarity and comfort, which has had only the positive effect of neither one of us feeling particularly out of place.
In the future, it would be pertinent to go through our code and ensure that at least naming conventions are consistent, and getting rid of any unused variables associated with some functionality that we were experimenting with. Not having that linter, especially when learning a new language and platform (as Jack is doing) poses some great challenges on both his and Jerry’s part that make for a deeply educational collaborative experience.
