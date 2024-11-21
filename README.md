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


