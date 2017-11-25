using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightsOut {
	class LOExternalLights {
		EditorFacility currentFacility;
		EditorLevel currentLevel;

		class LightToChange {
			public string name;
			public string parentName;
		}

		List<LightToChange> lightsToChange;

		public LOExternalLights(EditorFacility facility, EditorLevel level) {
			currentFacility = facility;
			currentLevel = level;

			if (currentFacility == EditorFacility.VAB) {
				switch (currentLevel) {
				case EditorLevel.Level1:
                        //[LOG 17:53:55.152] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:53:55.153] Parent: SunLight
                        //[LOG 17:53:55.154] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:53:55.154] Parent: Scenery
                        //[LOG 17:53:55.155] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:53:55.155] Parent: _UI
                        //[LOG 17:53:55.156] Light 3: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:53:55.156] Parent: Day Lights
                        //[LOG 17:53:55.157] Light 4: SpotlightSun, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:53:55.158] Parent: Day Lights
                        //[LOG 17:53:55.158] Light 5: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:53:55.159] Parent: Day Lights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "SpotlightCraft", parentName = "Day Lights" },
                        new LightToChange(){ name = "SpotlightSun", parentName = "VAB_interior_modern" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Shadow Light", parentName = "Day Lights" }					};
					break;

				case EditorLevel.Level2:
                        //[LOG 17:56:18.866] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:56:18.867] Parent: SunLight
                        //[LOG 17:56:18.867] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:56:18.868] Parent: Scenery
                        //[LOG 17:56:18.868] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:56:18.869] Parent: _UI
                        //[LOG 17:56:18.869] Light 3: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:56:18.870] Parent: Day Lights
                        //[LOG 17:56:18.871] Light 4: SpotlightSun, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:56:18.871] Parent: Day Lights
                        //[LOG 17:56:18.872] Light 5: roofFill, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:56:18.872] Parent: Day Lights
                        //[LOG 17:56:18.873] Light 6: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:56:18.873] Parent: Day Lights
                        //[LOG 17:56:18.874] Light 7: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:56:18.875] Parent: Day Lights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "SpotlightCraft", parentName = "Day Lights" },
                        new LightToChange(){ name = "SpotlightSun", parentName = "VAB_interior_modern" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Shadow Light", parentName = "Day Lights" }
                    };
					break;

				case EditorLevel.Level3:
                        //[LOG 17:58:21.702] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:58:21.703] Parent: SunLight
                        //[LOG 17:58:21.703] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:58:21.704] Parent: Scenery
                        //[LOG 17:58:21.704] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.705] Parent: _UI
                        //[LOG 17:58:21.705] Light 3: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:58:21.706] Parent: Day Lights
                        //[LOG 17:58:21.707] Light 4: SpotlightSun, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.707] Parent: Day Lights
                        //[LOG 17:58:21.708] Light 5: Spotlight, RGBA(1.000, 0.236, 0.104, 1.000), Default ON
                        //[LOG 17:58:21.708] Parent: model_prop_truck_h03
                        //[LOG 17:58:21.709] Light 6: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.710] Parent: Day Lights
                        //[LOG 17:58:21.710] Light 7: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.711] Parent: model_vab_prop_truck_01
                        //[LOG 17:58:21.711] Light 8: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.712] Parent: model_prop_truck_h02
                        //[LOG 17:58:21.712] Light 9: Spotlight, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:58:21.713] Parent: Day Lights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "SpotlightCraft", parentName = "Day Lights" },
                        new LightToChange(){ name = "SpotlightSun", parentName = "VAB_interior_modern" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Shadow Light", parentName = "Day Lights" }
                    };
					break;
				}
			}
			else if (currentFacility == EditorFacility.SPH) {
				switch (currentLevel) {
				case EditorLevel.Level1:
                        //[LOG 17:54:50.069] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:54:50.070] Parent: SunLight
                        //[LOG 17:54:50.070] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:54:50.071] Parent: Scenery
                        //[LOG 17:54:50.072] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:54:50.072] Parent: _UI
                        //[LOG 17:54:50.073] Light 3: SpotlightWindow, RGBA(0.866, 0.863, 0.903, 1.000), Default OFF
                        //[LOG 17:54:50.073] Parent: Editors_DayLights
                        //[LOG 17:54:50.074] Light 4: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:54:50.075] Parent: Editors_DayLights
                        //[LOG 17:54:50.075] Light 5: SpotlightCraft, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:54:50.076] Parent: Editors_DayLights
                        //[LOG 17:54:50.076] Light 6: SpotlightExteriorSun, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:54:50.077] Parent: Editors_DayLights
                        //[LOG 17:54:50.077] Light 7: SpotlightWindow, RGBA(0.866, 0.863, 0.903, 1.000), Default OFF
                        //[LOG 17:54:50.078] Parent: Editors_DayLights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "Realtime_SpotlightCraft", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_ExteriorSun", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_Shadow Light", parentName = "Lighting_Realtime" }
                    };
					break;

				case EditorLevel.Level2:
                        //[LOG 17:57:04.716] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:57:04.717] Parent: SunLight
                        //[LOG 17:57:04.717] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:57:04.718] Parent: Scenery
                        //[LOG 17:57:04.718] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:57:04.719] Parent: _UI
                        //[LOG 17:57:04.720] Light 3: SpotlightWindow, RGBA(0.866, 0.863, 0.903, 1.000), Default OFF
                        //[LOG 17:57:04.720] Parent: Editors_DayLights
                        //[LOG 17:57:04.721] Light 4: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:57:04.721] Parent: Editors_DayLights
                        //[LOG 17:57:04.722] Light 5: SpotlightCraft, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:57:04.723] Parent: Editors_DayLights
                        //[LOG 17:57:04.723] Light 6: SpotlightExteriorSun, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:57:04.724] Parent: Editors_DayLights
                        //[LOG 17:57:04.724] Light 7: SpotlightWindow, RGBA(0.866, 0.863, 0.903, 1.000), Default OFF
                        //[LOG 17:57:04.725] Parent: Editors_DayLights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "Realtime_SpotlightCraft", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_ExteriorSun", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_Shadow Light", parentName = "Lighting_Realtime" }
                    };
					break;

				case EditorLevel.Level3:
                        //[LOG 17:59:41.825] Light 0: Scaledspace SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:59:41.826] Parent: SunLight
                        //[LOG 17:59:41.826] Light 1: SunLight, RGBA(1.000, 1.000, 1.000, 1.000), Default OFF
                        //[LOG 17:59:41.827] Parent: Scenery
                        //[LOG 17:59:41.828] Light 2: Directional light, RGBA(1.000, 1.000, 1.000, 1.000), Default ON
                        //[LOG 17:59:41.828] Parent: _UI
                        //[LOG 17:59:41.829] Light 3: Shadow Light, RGBA(1.000, 0.955, 0.896, 1.000), Default ON
                        //[LOG 17:59:41.829] Parent: Editors_DayLights
                        //[LOG 17:59:41.830] Light 4: SpotlightCraft, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:59:41.831] Parent: Editors_DayLights
                        //[LOG 17:59:41.831] Light 5: Spotlight, RGBA(1.000, 0.236, 0.104, 1.000), Default ON
                        //[LOG 17:59:41.832] Parent: model_prop_truck_h03
                        //[LOG 17:59:41.832] Light 6: SpotlightExteriorSun, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:59:41.833] Parent: Editors_DayLights
                        //[LOG 17:59:41.833] Light 7: SpotlightWindow, RGBA(0.866, 0.863, 0.903, 1.000), Default ON
                        //[LOG 17:59:41.834] Parent: Editors_DayLights
					lightsToChange = new List<LightToChange>() {
                        new LightToChange(){ name = "Realtime_SpotlightCraft", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_ExteriorSun", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_SpotlightScenery", parentName = "Lighting_Realtime" },
                        new LightToChange(){ name = "Realtime_Shadow Light", parentName = "Lighting_Realtime" }
                    };
					break;
				}
			}
		}

		public void SetExternalLightsMode(EditorTime time, Light[] lights) {
			foreach (Light light in lights) {
				//Debug.Log("Light: " + light.name + ", Parent: " + light.transform.parent.gameObject.name + ", " + light.color + ", Default " + (light.enabled ? "ON" : "OFF"));
				foreach (LightToChange lightToChange in lightsToChange) {
					if (light.name.Equals(lightToChange.name, StringComparison.Ordinal)
					    && light.transform.parent.gameObject.name.Equals(lightToChange.parentName, StringComparison.Ordinal)) {
						light.enabled = (time == EditorTime.Day ? true : false);
					}
				}
			}
		}
	}
}
