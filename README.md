![Game Cover](https://i.imgur.com/50VEmUQ.jpeg)

# Game Title
### Night Bus

# Game Page
https://gurd62.itch.io/night-bus

# Premise
The player waits for the bus at night. During some nights, the player experiences different events (picking up trash, a scream etc.).
On the last night, the player gets chased by a monster and is able to barely escape it or get caught by it.
Missing the bus, getting run over by the bus or getting caught by the monster will kill the player, sending them back to the menus screen.

# Controls
Move with WASD, look with the mouse, sprint with Shift, interact with E.

# Development
The game was developed with the Unity Engine.
Free game assets are used and authors are credited in the release build.
Development time was 13 days for first release, with an additional 2 days for releasing small updates.
Some code was taken from older projects of mine and from the Unity Marketplace (Modular First Person Controller).
This game is fully playable, polished, and uploaded to Itch.io for feedback and testing. Led to development of updates.

## Key Takeaways
- Improved event handling in C# (subscribing/unsubscribing)
- Applied Scriptable Objects for flexible night/event management
- Implemented multiple camera setups and coroutine management
- Learned to adapt previous code for new projects efficiently
- Reacted to player feedback from Itch.io to improve polish

## Critique / Things to Look Out For
- UI scaling issues for resolutions beyond 320x240 —> plan better for different screens
- Long downtime during some nights reduces player interaction —> consider pacing adjustments
- Restarting player at menu after death reduces freedom hinders freedom of looking around —> could allow restart at level
- Some scare elements were missed by players —> consider ways to guide player attention
- Intuitive interactions (e.g. entering bus) need better playtesting in future projects
