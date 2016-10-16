# LightsOut
LightsOut is a Kerbal Space Program add-on that allows the user to switch between day and night mode in the VAB and SPH.

This build is forked from the official LightsOut 1.0.5 code, and includes these enhancements:

* Fwiffo's edit to change hanger lights hotkey from L to P to avoid conflicts with RCS Build Aid (thruster translation) and RPM JSI Camera (toggle all camera FOV's)
* Fwiffo's edit to incorporate linuxgurugamer's fix to avoid capturing hotkeys when focus is in a text field (e.g. parts search box)
  https://github.com/linuxgurugamer/EditorExtensionsRedux/commit/72dadd79c0611fb52b15f526ebfe19b67d2b8d57#diff-d9ad04479ff4a0b367ff88d3bc07ec9a
* RealGecko's May 6, 2016 recompile for 1.1.2:
  http://forum.kerbalspaceprogram.com/index.php?/topic/102558-105-lightsout-v015-daynight-mode-in-vabsph/&do=findComment&comment=2565403
* Kujuman's contributions to make it look better:
  http://forum.kerbalspaceprogram.com/index.php?/topic/102558-105-lightsout-v015-daynight-mode-in-vabsph/&do=findComment&comment=2565903

### Download
[v0.1.5](https://github.com/nodrog6/LightsOut/releases/download/v0.1.5/LightsOut-v0.1.5.zip) (Compatible with Kerbal Space Program v1.0.5)

### Hotkeys
* **P** - Toggle day/night and all part lights
* **U** - Toggle all part lights

### Installation
Extract and merge the GameData folder with the GameData folder in your KSP directory.

### FAQ
* __Does this add-on cause time to flow in the VAB or SPH?__

No, time stands still in the editor as usual.
* __Why doesn't the VAB/SPH start in night-mode when it's night outside?__

I prefer to build craft when the lights are on.  For now, turning the VAB/SPH lights on and off is tied to the day/night cycle.

### Todo's and Future Features
* Optimize first time setup
* Configurable hotkeys

### License
MIT
