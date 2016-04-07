using System;
using UnityEngine;

namespace LightsOut {
	class LOAmbient {

		// Skybox
		Camera mainCamera;
		GameObject skyCamera;
		Material nightSkyboxMaterial;

		int newLayer;
		EditorFacility currentFacility;

		// Original Configurations
		int originalCullingMask;
		CameraClearFlags originalClearFlags;
		Color originalAmbientLight;
		LightmapData[] originalLightmapData;
		Material originalSkybox;
        float originalAmbientIntensity;

		public LOAmbient(EditorFacility facility, EditorLevel level, GameObject[] gameObjects, Camera camera, int layer) {
			mainCamera = camera;
			newLayer = layer;
			currentFacility = facility;

			originalCullingMask = camera.cullingMask;
			originalClearFlags = camera.clearFlags;
			originalAmbientLight = RenderSettings.ambientLight;
			originalSkybox = RenderSettings.skybox;
			originalLightmapData = LightmapSettings.lightmaps;
            originalAmbientIntensity = RenderSettings.ambientIntensity;

			// Create fake skybox
			skyCamera = new GameObject("NightSkyboxCamera", typeof(Camera));
            skyCamera.GetComponent<Camera>().depth = mainCamera.depth - 1;
            skyCamera.GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            skyCamera.GetComponent<Camera>().cullingMask = 0;

            nightSkyboxMaterial = new Material(originalSkybox);

			// GalaxyTex_PositiveX should be viewed outside window
			Debug.Log("LightsOut: Loading Night Sky Textures");
			foreach (Material material in Resources.FindObjectsOfTypeAll<Material>()) {
				Texture texture = material.mainTexture;
				if (texture) {
					switch (material.name) {
					case "ZP (Instance)":
						nightSkyboxMaterial.SetTexture("_FrontTex", material.mainTexture);
						break;
					case "ZN (Instance)":
						nightSkyboxMaterial.SetTexture("_BackTex", material.mainTexture);
						break;
					case "XP (Instance)":
						nightSkyboxMaterial.SetTexture("_LeftTex", material.mainTexture);
						break;
					case "XN (Instance)":
						nightSkyboxMaterial.SetTexture("_RightTex", material.mainTexture);
						break;
					case "YP (Instance)":
						nightSkyboxMaterial.SetTexture("_UpTex", material.mainTexture);
						break;
					case "YN (Instance)":
						nightSkyboxMaterial.SetTexture("_DownTex", material.mainTexture);
						break;
					default:
						break;
					}
				}
			}

			skyCamera.AddComponent<Skybox>();
			skyCamera.GetComponent<Skybox>().material = nightSkyboxMaterial;

			if (facility == EditorFacility.VAB) {
				switch (level) {
				case EditorLevel.Level1:
					break;

				case EditorLevel.Level2:
					break;

				case EditorLevel.Level3:
					foreach (GameObject gameObject in gameObjects) {
						ChangeLayersRecursively(gameObject, newLayer, "model_vab_interior_floor_cover_v20");
					}
					break;
				}
			}
			else if (facility == EditorFacility.SPH) {
				switch (level) {
				case EditorLevel.Level1:
					break;

				case EditorLevel.Level2:
					break;

				case EditorLevel.Level3:
					foreach (GameObject gameObject in gameObjects) {
						// These are all subsets of model_sph_interior_lights_v16
						// Component_611_1 to 6 is window reflection
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_1");
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_2");
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_3");
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_4");
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_5");
						ChangeLayersRecursively(gameObject, newLayer, "Component_611_6");
						ChangeLayersRecursively(gameObject, newLayer, "Component_749_1"); // Glow from Side Lights!
						ChangeLayersRecursively(gameObject, newLayer, "Component_750_1"); // Lights!
					}
					break;
				}
			}
		}

		public void SetAmbientMode(EditorTime time) {
			if (time == EditorTime.Night) {
                
				RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.05f);
				RenderSettings.fog = false;
                RenderSettings.ambientIntensity = 0.05f;
				mainCamera.clearFlags = CameraClearFlags.Nothing;
				LightmapSettings.lightmaps = new LightmapData[] { };

				mainCamera.cullingMask = originalCullingMask;
			}
			else {
				RenderSettings.ambientLight = originalAmbientLight;
				RenderSettings.fog = true;
				RenderSettings.skybox = originalSkybox;
                RenderSettings.ambientIntensity = originalAmbientIntensity;
				mainCamera.clearFlags = originalClearFlags;
				LightmapSettings.lightmaps = originalLightmapData;

				mainCamera.cullingMask = originalCullingMask | 1 << newLayer;
			}
		}

		public void rotateSkybox() {
			// Adjust Skybox Camera
			// This function can get called before skyCamera is set
			if (skyCamera) {
				skyCamera.transform.position = mainCamera.transform.position;
				skyCamera.transform.rotation = mainCamera.transform.rotation;
				// Rotate this correctly for SPH
				if (currentFacility == EditorFacility.VAB) {
					skyCamera.transform.Rotate(-90, 0, 0, Space.World);
				}
				else if (currentFacility == EditorFacility.SPH) {
					// Rotation order: Z, X, Y
					// x rotates about gravity axis
					// y rotates about sun's rotation axis
					// z rotates about sun's radial axis (don't know angle)
					skyCamera.transform.Rotate(-30, 90, -90, Space.World);
				}
			}
		}

		void ChangeLayersRecursively(GameObject gameObject, int layer, string objectname) {
			ChangeLayersRecursivelyHelper(gameObject, layer, objectname, false);
		}

		void ChangeLayersRecursivelyHelper(GameObject gameObject, int layer, string objectname, bool found) {
			if (objectname.Equals(gameObject.name, StringComparison.Ordinal)) {
				found = true;
			}

			if (found) {
				gameObject.layer = layer;
			}

			foreach (Transform child in gameObject.transform) {
				ChangeLayersRecursivelyHelper(child.gameObject, layer, objectname, found);
			}
		}
	}
}
