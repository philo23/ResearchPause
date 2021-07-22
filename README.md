# Workshop Page
The easiest way to install this mod is by subscribing from the Steam workshop.

https://steamcommunity.com/sharedfiles/filedetails/?id=2348520530

# Getting started developing
If you want to try building this mod yourself here are some steps to get you started:

- For easiest setup, clone into `steamapps\common\RimWorld\Mods\ResearchPause`
- Open the solution in Visual Studio and then add the following external DLLs through the Reference Manager (right click `References`, then choose `Add Reference...` under the ResearchPause project in the Solutions Explorer)
  - `0Harmony.dll` (from a copy of the Harmony mod)
  - `UnityEngine.CoreModule.dll` (from RimWorld install directory, `steamapps\common\RimWorld\RimWorldWin64_Data\Managed`...)
  - `UnityEngine.dll` (from the RimWorld install directory)
  - `Assembly-CSharp.dll` (from the RimWorld install directory)
- Ensure all these references are marked as `Copy Local: False` under each reference's properties.

If you cloned this into the mods folder this should be all setup to enable this mod in RimWorld.

## Tip for quick debugging
- To quickly test in-game, setup the Debug start options to point to your `RimWorldWin64.exe` and add `-quicktest` to the command line arguments.
