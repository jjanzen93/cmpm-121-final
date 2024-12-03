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
    Time is advanced manually via performing actions. Planting, cutting, and moving all advance time forward.
[F0.c] You can reap or sow plants on grid cells only when you are near them.
    Planting and cutting works on the space upon which the player is standing, so movement (and thus time advancement) is necessary to interact with multiple cells.
[F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
    Each time an action is performed and time advances, a random amount of water is added to the soil and a random amount of sun is generated per cell. The sun resets and is rerolled every time one of those actions is performed, while the water represents an aggregate of all water recieved over the plant's presence in the game.
[F0.e] Each plant on the grid has a distinct type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
    The plants have multiple levels of growth that are moved through based on the water and sun present. 
[F0.f] Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
    If there is enough of both water and sun, the plant will move to the next level. Since these values are rerolled every time, the length of time is random. However, certain plant types require more or less resources to grow, thus making their average times different. Additionally, there are adjustments made to the amount of necessary resources depending on the number and type of adjacent plants.
[F0.g] A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
    The point counter will display a "You Win!" message upon achieving 10 points, which are accumulated by harvesting fully grown plants.

## Reflection
Looking back on how you achieved the F0 requirements, how has your team’s plan changed? Did you reconsider any of the choices you previously described for Tools and Materials or your Roles? It would be very suspicious if you didn’t need to change anything. There’s learning value in you documenting how your team’s thinking has changed over time.

The holidays made it difficult to have an even workload, so it may be pertinent to stay more in the loop in the future in order to not get overwhelmed after a lot of work has been done. Additionally, we could better divvy up the work, which will be easier now that schedules are clearer and the project can take a clear priority over other life events. The chosen linter seems to have done a good job with keeping the code clean. When assets become more polished in the future, it could also be a good idea to push them through GitHub rather than share them through another service such as Google Drive.
