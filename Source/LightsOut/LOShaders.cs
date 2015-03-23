using System;
using System.Collections.Generic;
using UnityEngine;

namespace LightsOut {
	class LOShaders {

		struct MaterialTracker {
			public Material current;
			public Material original;
		}

		class ShaderToChange {
			public string name;
			public List<string> path = new List<string>();
			public List<string> shaders = new List<string>();
		}

		List<MaterialTracker> originalShaders = new List<MaterialTracker>();

		public LOShaders(EditorFacility facility, EditorLevel level, GameObject[] gameObjects) {
			List<ShaderToChange> shadersToFind = new List<ShaderToChange>();

			List<string> defaultShaders = new List<string>() {
				"KSP/Emissive/Specular",
				"KSP/Emissive/Bumped",
				"KSP/Emissive/Diffuse"
			};

			if (facility == EditorFacility.VAB) {
				switch (level) {
				case EditorLevel.Level1:
					{
						//INTERIOR_VAB_lev2 1_MeshPart1
						ShaderToChange shader = new ShaderToChange();
						shader.path.Add("VAB_lev1_interior");
						shader.path.Add("INTERIOR_VAB_lev2 1");
						shader.name = "INTERIOR_VAB_lev2 1_MeshPart1";
						shader.shaders = new List<string>(defaultShaders);
						shadersToFind.Add(shader);
						break;
					}

				case EditorLevel.Level2:
                        // shader.path.Add("VAB_lev3_mainStructure");
					break;

				case EditorLevel.Level3:
					for (int i = 0; i < 3; i++) {
						ShaderToChange shader = new ShaderToChange();
						shader.path.Add("VAB_Interior_Geometry");
						shader.shaders = new List<string>(defaultShaders);
						switch (i) {
						case 0:
							shader.name = "model_vab_interior_props_v16";
							break;

						case 1:
							shader.name = "model_vab_walls";
							break;

						case 2:
							shader.name = "model_vab_windows";
							break;
						}
						shadersToFind.Add(shader);
					}
					break;
				}
			}
			else if (facility == EditorFacility.SPH) {
				switch (level) {
				case EditorLevel.Level1:
					{
						ShaderToChange shader = new ShaderToChange();
						shader.path.Add("SPH-1_Interior");
						shader.name = "group37_polySurface813";
						shader.shaders = new List<string>(defaultShaders);
						shadersToFind.Add(shader);
						break;
					}

				case EditorLevel.Level2:
					for (int i = 0; i < 3; i++) {
						ShaderToChange shader = new ShaderToChange();
						shader.shaders = new List<string>(defaultShaders);
						switch (i) {
						case 0:
							shader.path.Add("SPH_2_interior");
							shader.name = "SPH_2_windows";
							break;

						case 1:
							shader.path.Add("SPH_2_interior");
							shader.name = "SPH_2_door3";
							break;

						case 2:
							shader.path.Add("SPH_2_interior");
							shader.name = "SPH_2_door4";
							break;
						}
						shadersToFind.Add(shader);
					}
					break;

				case EditorLevel.Level3:
					for (int i = 0; i < 2; i++) {
						ShaderToChange shader = new ShaderToChange();
						shader.path.Add("SPH_Interior_Geometry");
						shader.shaders = new List<string>(defaultShaders);
						switch (i) {
						case 0:
							shader.name = "model_sph_interior_main_v16";
							break;

						case 1:
							shader.path.Add("model_sph_interior_gates_v16");
							shader.name = "Component_755_1";
							break;
						}
						shadersToFind.Add(shader);
					}
					break;
				}
			}

			foreach (ShaderToChange shader in shadersToFind) {
				foreach (GameObject gameObject in gameObjects) {
					FindShader(gameObject, shader, 0);
				}
			}
		}

		public void SetShaderMode(EditorTime time) {
			for (int i = 0; i < originalShaders.Count; i++) {
				MaterialTracker material = originalShaders[i];
				if (time == EditorTime.Night) {
					material.current.shader = Shader.Find("KSP/Diffuse");
				}
				else {
					material.current.shader = material.original.shader;
				}
			}
		}

		// Save shaders to preserve
		void FindShader(GameObject gameObject, ShaderToChange shader, int level) {
			if (gameObject.name.Equals(shader.name, StringComparison.Ordinal)) {
				AddShadersToList(gameObject, shader.shaders);
			}
			else {
				if (level < (shader.path.Count - 1)) {
					foreach (Transform child in gameObject.transform) {
						if (child.gameObject.name.Equals(shader.path[level + 1], StringComparison.Ordinal)) {
							FindShader(child.gameObject, shader, level + 1);
						}
					}
				}
			}
		}

		void AddShadersToList(GameObject gameObject, List<string> shaderTypes) {
			foreach (Component component in gameObject.GetComponents<Component>()) {
				if (component.GetType().Name.Equals("MeshRenderer", StringComparison.Ordinal)) {
					MeshRenderer meshRenderer = component as MeshRenderer;
					if (meshRenderer) {
						for (int i = 0; i < meshRenderer.materials.Length; i++) {
							bool match = false;

							foreach (string shaderString in shaderTypes) {
								if (meshRenderer.materials[i].shader.name.Equals(shaderString, StringComparison.Ordinal)) {
									match = true;
									break;
								}
							}

							if (match) {
								MaterialTracker material;
								material.current = meshRenderer.materials[i];
								material.original = new Material(meshRenderer.materials[i]);
								originalShaders.Add(material);
							}
						}
					}
				}
			}
			foreach (Transform child in gameObject.transform) {
				AddShadersToList(child.gameObject, shaderTypes);
			}
		}
	}
}
