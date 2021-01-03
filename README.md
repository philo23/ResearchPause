# Getting started developing
- For easiest setup, clone into `steamapps\common\RimWorld\Mods\ResearchPause`
- Copy `0Harmony.dll` to `Source-DLLs\`
- Ensure paths to other dll's in project exist, such as `UnityEngine.dll`, `UnityEngine.Core.dll` and RimWorld's own `Assembly-CSharp.dll`. If you cloned this into the mods folder this should be all setup.

# Quick debugging
- To quickly test in-game, setup the Debug start options to point to your `rimworld.exe` and add `-quicktest` to the command line arguments.