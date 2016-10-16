using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KSP.UI.Screens;

namespace LightsOut {
	[KSPAddon(KSPAddon.Startup.EditorAny, false)]

	class LightsOut : MonoBehaviour {
		private static LightsOut Instance;
		EditorTime currentTime = EditorTime.Day;
		bool currentPartLightsEnabled = false;
		bool firstTime = true;
		int newLayer = 14;

		LOAmbient ambientManager;
		LOShaders shaderManager;
		LOExternalLights externalLightsManager;

		ApplicationLauncherButton launcherButton;
//		bool launcherButtonNeedsInitializing = true;
		string munIcon = "LightsOut/Textures/mun_icon";
		string sunIcon = "LightsOut/Textures/sun_icon";

		public void Awake()
		{
			if (Instance != null) {
				Destroy (this);
				return;
			}
			Instance = this;
		}

		public void Start() {
			GameEvents.onGUIApplicationLauncherReady.Add(OnGUIAppLauncherReady);
		}

		void OnDestroy() {
			GameEvents.onGUIApplicationLauncherReady.Remove (OnGUIAppLauncherReady);
			if (launcherButton != null)
				ApplicationLauncher.Instance.RemoveModApplication (launcherButton);
		}

		void OnGUIAppLauncherReady() {
			if (launcherButton == null) {
				launcherButton = ApplicationLauncher.Instance.AddModApplication(
					ToggleDayNight,
					ToggleDayNight,
					null,
					null,
					null,
					null,
					ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.SPH,
					(Texture)GameDatabase.Instance.GetTexture(munIcon, false));
			}
		}

		void ToggleDayNight() {
			SetTime((currentTime == EditorTime.Day) ? EditorTime.Night : EditorTime.Day);
		}

		void Update() {
			// Ignore keystrokes when a text field has focus (e.g. part search, craft title box)
			GameObject obj = EventSystem.current.currentSelectedGameObject;
			bool inputFieldIsFocused = (obj != null && obj.GetComponent<InputField> () != null && obj.GetComponent<InputField> ().isFocused);
			if (inputFieldIsFocused)
				return;

			// changed from L to P, to avoid conflicts with RCS Build Aid (translation keys) and RPM Camera (toggle all camera FOV's)
			if (Input.GetKeyDown (KeyCode.P)) {
				SetTime ((currentTime == EditorTime.Day) ? EditorTime.Night : EditorTime.Day);
			} else if (Input.GetKeyDown (KeyCode.U)) {
				SetPartLights (!currentPartLightsEnabled);
			}
		}

		void Setup() {
//			Debug.Log("LightsOut: First Time Setup");

			EditorFacility facility = EditorDriver.editorFacility;
			EditorLevel level = 0;

			switch (facility) {
			case EditorFacility.SPH:
				// 0 = base, 0.5 = 1st, 1 = final
				level = (EditorLevel)(2 * ScenarioUpgradeableFacilities.GetFacilityLevel(SpaceCenterFacility.SpaceplaneHangar) + 1);
				break;

			case EditorFacility.VAB:
				// 0 = base, 0.5 = 1st, 1 = final
				level = (EditorLevel)(2 * ScenarioUpgradeableFacilities.GetFacilityLevel(SpaceCenterFacility.VehicleAssemblyBuilding) + 1);
				break;
			}

//			Debug.Log("LightsOut: Entered " + facility + " " + level);

			// Set up ambient and shader managers
			GameObject[] gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			Camera[] cameras = FindObjectsOfType(typeof(Camera)) as Camera[];
			foreach (Camera camera in cameras) {
				if (camera.name == "sceneryCam") {
					ambientManager = new LOAmbient(facility, level, gameObjects, camera, newLayer);
				}
			}
			shaderManager = new LOShaders(facility, level, gameObjects);
			externalLightsManager = new LOExternalLights(facility, level);

			firstTime = false;
		}

		void SetTime(EditorTime newTime) {
			if (firstTime) {
				Setup();
			}
			currentTime = newTime;

//			Debug.Log("LightsOut: Switching to " + newTime + " Mode");

			// Toggle Shaders
//			Debug.Log("LightsOut: Updating Shaders");
			shaderManager.SetShaderMode(newTime);

			// Toggle ambient settings
//			Debug.Log("LightsOut: Updating Ambient Light and Skybox");
			ambientManager.SetAmbientMode(newTime);

			//toggle VAB/SPH lights
//			Debug.Log("LightsOut: Updating VAB/SPH Lights");
			externalLightsManager.SetExternalLightsMode(newTime, FindObjectsOfType(typeof(Light)) as Light[]);

			// Toggle all part lights*/
			SetPartLights(newTime == EditorTime.Night);

			if (newTime == EditorTime.Night) {
				launcherButton.SetTexture((Texture)GameDatabase.Instance.GetTexture(sunIcon, false));
			}
			else {
				launcherButton.SetTexture((Texture)GameDatabase.Instance.GetTexture(munIcon, false));
			}
		}

		void SetPartLights(bool lightsEnabled) {
			currentPartLightsEnabled = lightsEnabled;
//			Debug.Log("LightsOut: Turning Part Lights " + (lightsEnabled ? "on" : "off"));
			foreach (Part part in EditorLogic.fetch.ship.parts) {
				// Lights
				if (part.FindModuleImplementing<ModuleLight>() != null)
					part.SendEvent(lightsEnabled ? "LightsOn" : "LightsOff");

				// Pods
				foreach (ModuleColorChanger changer in part.FindModulesImplementing<ModuleColorChanger>())
					if (changer.defaultActionGroup == KSPActionGroup.Light)
						changer.animState = lightsEnabled; //changer.ToggleEvent();

				// Cabins, can be toggled only
				foreach (ModuleAnimateGeneric animator in part.FindModulesImplementing<ModuleAnimateGeneric>()) {
					if (animator.defaultActionGroup == KSPActionGroup.Light)
						animator.Toggle ();
				}
			}
		}

		void LateUpdate() {
//			if (launcherButtonNeedsInitializing) {
//				GameEvents.onGUIApplicationLauncherReady.Add(OnGUIAppLauncherReady);
////				OnGUIAppLauncherReady();
//				launcherButtonNeedsInitializing = false;
//			}
			if ((ambientManager != null) && (currentTime == EditorTime.Night)) {
				ambientManager.rotateSkybox();
			}
		}
	}
}
