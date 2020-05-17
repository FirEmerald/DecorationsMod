﻿using DecorationsMod.Controllers;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace DecorationsMod.NewItems
{
    public class ExosuitDoll : DecorationItem
    {
        public ExosuitDoll() // Feeds abstract class
        {
            this.ClassID = "ExosuitDoll";
            this.PrefabFileName = DecorationItem.DefaultResourcePath + this.ClassID;

            this.GameObject = AssetsHelper.Assets.LoadAsset<GameObject>("prawnsuitdoll");

            this.TechType = SMLHelper.V2.Handlers.TechTypeHandler.AddTechType(this.ClassID,
                                                        LanguageHelper.GetFriendlyWord("ExosuitDollName"),
                                                        LanguageHelper.GetFriendlyWord("ExosuitDollDescription"),
                                                        true);

            this.IsHabitatBuilder = true;

#if BELOWZERO
            this.Recipe = new SMLHelper.V2.Crafting.RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[3]
                    {
                        new Ingredient(TechType.Titanium, 1),
                        new Ingredient(TechType.Glass, 1),
                        new Ingredient(TechType.Silicone, 1)
                    }),
            };
#else
            this.Recipe = new SMLHelper.V2.Crafting.TechData()
            {
                craftAmount = 1,
                Ingredients = new List<SMLHelper.V2.Crafting.Ingredient>(new SMLHelper.V2.Crafting.Ingredient[3]
                    {
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Titanium, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Glass, 1),
                        new SMLHelper.V2.Crafting.Ingredient(TechType.Silicone, 1)
                    }),
            };
#endif
        }

        public override void RegisterItem()
        {
            if (this.IsRegistered == false)
            {
                GameObject aquarium = Resources.Load<GameObject>("Submarine/Build/Aquarium");

                // Retrieve model node
                GameObject model = this.GameObject.FindChild("prawnsuit");

                // Move model
                model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y - 0.002f, model.transform.localPosition.z);

                // Add prefab identifier
                var prefabId = this.GameObject.AddComponent<PrefabIdentifier>();
                prefabId.ClassId = this.ClassID;

                // Add large world entity
                PrefabsHelper.SetDefaultLargeWorldEntity(this.GameObject);

                // Add tech tag
                var techTag = this.GameObject.AddComponent<TechTag>();
                techTag.type = this.TechType;

                // Add box collider
                var collider = this.GameObject.AddComponent<BoxCollider>();
                //collider.radius = 0.0375f;
                collider.size = new Vector3(0.04f, 0.115f, 0.04f);
                collider.center = new Vector3(collider.center.x, collider.center.y + 0.0575f, collider.center.z);

                // Get glass material
                Material glass = null;
                Renderer[] aRenderers = aquarium.GetComponentsInChildren<Renderer>(true);
                foreach (Renderer aRenderer in aRenderers)
                {
                    foreach (Material aMaterial in aRenderer.materials)
                    {
                        if (aMaterial.name.StartsWith("Aquarium_glass", StringComparison.OrdinalIgnoreCase))
                        {
                            glass = aMaterial;
                            break;
                        }
                    }
                    if (glass != null)
                        break;
                }

                // Set proper shaders (for crafting animation)
                Shader shader = Shader.Find("MarmosetUBER");
                Texture normal = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_normal");
                Texture spec = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_spec");
                Texture colorMask = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_colorMask");
                Texture illum = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_illum");
                Texture normal2 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_fp_normal");
                Texture illum2 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_fp_illum");
                Texture normal3 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_Arm_Propulsion_Cannon_normal");
                Texture colorMask3 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_Arm_Propulsion_Cannon_colorMask");
                Texture illum3 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_Arm_Propulsion_Cannon_illum");
                Texture normal4 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_grappling_arm_normal");
                Texture colorMask4 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_grappling_arm_colorMask");
                Texture illum4 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_grappling_arm_illum");
                Texture normal5 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_01_glass_normal");
                Texture normal6 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_storage_01_normal");
                Texture colorMask6 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_storage_01_colorMask");
                Texture illum6 = AssetsHelper.Assets.LoadAsset<Texture>("exosuit_storage_01_illum");
                Texture normal7 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_torpedo_launcher_arm_normal");
                Texture colorMask7 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_torpedo_launcher_arm_colorMask");
                Texture illum7 = AssetsHelper.Assets.LoadAsset<Texture>("Exosuit_torpedo_launcher_arm_illum");
                Texture normal8 = AssetsHelper.Assets.LoadAsset<Texture>("engine_power_cell_ion_normal");
                Texture spec8 = AssetsHelper.Assets.LoadAsset<Texture>("engine_power_cell_ion_spec");
                Texture illum8 = AssetsHelper.Assets.LoadAsset<Texture>("engine_power_cell_ion_illum");
                Texture normal9 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_normal");
                Texture spec9 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_torpedo_01_spec");
                Texture normal10 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_upgrade_slots_01_normal");
                Texture spec10 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_upgrade_slots_01_spec");
                Texture illum10 = AssetsHelper.Assets.LoadAsset<Texture>("seamoth_upgrade_slots_01_illum");
                Texture normal11 = AssetsHelper.Assets.LoadAsset<Texture>("submarine_engine_power_cells_01_normal");
                Texture spec11 = AssetsHelper.Assets.LoadAsset<Texture>("submarine_engine_power_cells_01_spec");
                Texture illum11 = AssetsHelper.Assets.LoadAsset<Texture>("submarine_engine_power_cells_01_illum");

                Renderer[] renderers = this.GameObject.GetAllComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    if (renderer.name.StartsWith("Exosuit_cabin_01_glass"))
                        renderer.material = glass;
                    else if (renderer.materials != null)
                    {
                        foreach (Material tmpMat in renderer.materials)
                        {
                            // Associate MarmosetUBER shader
                            if (tmpMat.name.CompareTo("exosuit_cabin_01_glass (Instance)") == 0)
                            {
                                tmpMat.EnableKeyword("MARMO_SIMPLE_GLASS");
                                tmpMat.EnableKeyword("WBOIT");
                            }
                            else if (tmpMat.name.CompareTo("exosuit_01_glass (Instance)") != 0)
                                tmpMat.shader = shader;

                            if (tmpMat.name.CompareTo("exosuit_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal);
                                tmpMat.SetTexture("_ColorMask", colorMask);
                                tmpMat.SetTexture("_SpecTex", spec);
                                tmpMat.SetTexture("_Illum", illum);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable color mask
                                tmpMat.EnableKeyword("MARMO_VERTEX_COLOR");
                                // Enable specular
                                //tmpMat.EnableKeyword("MARMO_SPECULAR_ON");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("exosuit_01_fp (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal2);
                                tmpMat.SetTexture("_Illum", illum2);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("Exosuit_Arm_Propulsion_Cannon (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal3);
                                tmpMat.SetTexture("_ColorMask", colorMask3);
                                tmpMat.SetTexture("_Illum", illum3);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable color mask
                                tmpMat.EnableKeyword("MARMO_VERTEX_COLOR");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("Exosuit_grappling_arm (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal4);
                                tmpMat.SetTexture("_ColorMask", colorMask4);
                                tmpMat.SetTexture("_Illum", illum4);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable color mask
                                tmpMat.EnableKeyword("MARMO_VERTEX_COLOR");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            /*
                            else if (tmpMat.name.CompareTo("exosuit_01_glass (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal5);

                                tmpMat.EnableKeyword("MARMO_SIMPLE_GLASS");
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                tmpMat.EnableKeyword("WBOIT");
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("exosuit_cabin_01_glass (Instance)") == 0)
                            {
                                tmpMat.EnableKeyword("MARMO_SIMPLE_GLASS");
                                tmpMat.EnableKeyword("WBOIT");
                            }
                            */
                            else if (tmpMat.name.CompareTo("exosuit_storage_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal6);
                                tmpMat.SetTexture("_ColorMask", colorMask6);
                                tmpMat.SetTexture("_Illum", illum6);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable color mask
                                tmpMat.EnableKeyword("MARMO_VERTEX_COLOR");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("Exosuit_torpedo_launcher_arm (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal7);
                                tmpMat.SetTexture("_ColorMask", colorMask7);
                                tmpMat.SetTexture("_Illum", illum7);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable color mask
                                tmpMat.EnableKeyword("MARMO_VERTEX_COLOR");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("power_cell_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal8);
                                tmpMat.SetTexture("_SpecTex", spec8);
                                tmpMat.SetTexture("_Illum", illum8);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable specular
                                tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("seamoth_torpedo_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal9);
                                tmpMat.SetTexture("_SpecTex", spec9);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable specular
                                tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("seamoth_upgrade_slots_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal10);
                                tmpMat.SetTexture("_SpecTex", spec10);
                                tmpMat.SetTexture("_Illum", illum10);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable specular
                                tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                            else if (tmpMat.name.CompareTo("submarine_engine_power_cells_01 (Instance)") == 0)
                            {
                                tmpMat.SetTexture("_BumpMap", normal11);
                                tmpMat.SetTexture("_SpecTex", spec11);
                                tmpMat.SetTexture("_Illum", illum11);
                                tmpMat.SetFloat("_EmissionLM", 0.75f); // Set always visible

                                // Enable specular
                                tmpMat.EnableKeyword("MARMO_SPECULAR_IBL");
                                tmpMat.EnableKeyword("MARMO_SPECULAR_DIRECT");
                                tmpMat.EnableKeyword("MARMO_SPECMAP");
                                tmpMat.EnableKeyword("MARMO_MIP_GLOSS");
                                // Enable normal map
                                tmpMat.EnableKeyword("MARMO_NORMALMAP");
                                // Enable emission map
                                tmpMat.EnableKeyword("MARMO_EMISSION");
                                // Enable Z write
                                tmpMat.EnableKeyword("_ZWRITE_ON");
                            }
                        }
                    }
                }

                // Add sky applier
                SkyApplier applier = this.GameObject.GetComponent<SkyApplier>();
                if (applier == null)
                    applier = this.GameObject.AddComponent<SkyApplier>();
                applier.renderers = renderers;
                applier.anchorSky = Skies.Auto;
                applier.updaterIndex = 0;
                SkyApplier[] appliers = this.GameObject.GetComponentsInChildren<SkyApplier>();
                if (appliers != null && appliers.Length > 0)
                {
                    foreach (SkyApplier ap in appliers)
                    {
                        ap.renderers = renderers;
                        ap.anchorSky = Skies.Auto;
                        ap.updaterIndex = 0;
                    }
                }

                // Add contructable
                var constructible = this.GameObject.AddComponent<Constructable>();
                constructible.allowedInBase = true;
                constructible.allowedInSub = true;
                constructible.allowedOutside = true;
                constructible.allowedOnCeiling = false;
                constructible.allowedOnGround = true;
                constructible.allowedOnConstructables = true;
#if BELOWZERO
                constructible.allowedUnderwater = true;
#endif
                constructible.controlModelState = true;
                constructible.deconstructionAllowed = true;
                constructible.rotationEnabled = true;
                constructible.model = model;
                constructible.techType = this.TechType;
                constructible.enabled = true;

                // Add constructable bounds
                var bounds = this.GameObject.AddComponent<ConstructableBounds>();
                bounds.bounds.position = new Vector3(bounds.bounds.position.x, bounds.bounds.position.y + 0.002f, bounds.bounds.position.z);

                // Add model controler
                var exosuitDollControler = this.GameObject.AddComponent<ExosuitDollController>();

#region Disable right arms (except hand arm)

                GameObject rightArm = model.FindChild("ExosuitArmRight");
                GameObject rightArmRig = rightArm.FindChild("exosuit_01_armRight 1").FindChild("ArmRig 1");
                GameObject rightTorpedoArm = rightArmRig.FindChild("exosuit_arm_torpedoLauncher_geo 1");
                GameObject rightDrillArm = rightArmRig.FindChild("exosuit_drill_geo 1");
                GameObject rightGrapplinArm = rightArmRig.FindChild("exosuit_grapplingHook_geo 1");
                GameObject rightGrapplinHand = rightArmRig.FindChild("exosuit_grapplingHook_hand_geo 1");
                GameObject rightHandArm = rightArmRig.FindChild("exosuit_hand_geo 1");
                GameObject rightPropulsionArm = rightArmRig.FindChild("exosuit_propulsion_geo 1");
                
                // Disable right torpedo arm
                List<Renderer> rightTorpedoArmRenderers = new List<Renderer>();
                rightTorpedoArm.GetComponentsInChildren<Renderer>(rightTorpedoArmRenderers);
                if (!rightTorpedoArmRenderers.Contains(rightTorpedoArm.GetComponent<Renderer>()))
                    rightTorpedoArmRenderers.Add(rightTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in rightTorpedoArmRenderers)
                {
                    rend.enabled = false;
                }

                // Disable right drill arm
                rightDrillArm.GetComponent<Renderer>().enabled = false;

                // Disable right grapplin arm
                List<Renderer> rightGrapplinArmRenderers = new List<Renderer>();
                rightGrapplinHand.GetComponentsInChildren<Renderer>(rightGrapplinArmRenderers);
                if (!rightGrapplinArmRenderers.Contains(rightGrapplinHand.GetComponent<Renderer>()))
                    rightGrapplinArmRenderers.Add(rightGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in rightGrapplinArmRenderers)
                {
                    rend.enabled = false;
                }
                rightGrapplinArm.GetComponent<Renderer>().enabled = false;

                // Disable right propulsion arm
                rightPropulsionArm.GetComponent<Renderer>().enabled = false;

#endregion
                
#region Disable left arms (except hand arm)

                GameObject leftArm = model.FindChild("ExosuitArmLeft");
                GameObject leftArmRig = leftArm.FindChild("exosuit_01_armRight").FindChild("ArmRig");
                GameObject leftTorpedoArm = leftArmRig.FindChild("exosuit_arm_torpedoLauncher_geo");
                GameObject leftDrillArm = leftArmRig.FindChild("exosuit_drill_geo");
                GameObject leftGrapplinArm = leftArmRig.FindChild("exosuit_grapplingHook_geo");
                GameObject leftGrapplinHand = leftArmRig.FindChild("exosuit_grapplingHook_hand_geo");
                GameObject leftHandArm = leftArmRig.FindChild("exosuit_hand_geo");
                GameObject leftPropulsionArm = leftArmRig.FindChild("exosuit_propulsion_geo");

                // Disable left torpedo arm
                List<Renderer> leftTorpedoArmRenderers = new List<Renderer>();
                leftTorpedoArm.GetComponentsInChildren<Renderer>(leftTorpedoArmRenderers);
                if (!leftTorpedoArmRenderers.Contains(leftTorpedoArm.GetComponent<Renderer>()))
                    leftTorpedoArmRenderers.Add(leftTorpedoArm.GetComponent<Renderer>());
                foreach (Renderer rend in leftTorpedoArmRenderers)
                {
                    rend.enabled = false;
                }

                // Disable left drill arm
                leftDrillArm.GetComponent<Renderer>().enabled = false;

                // Disable right grapplin arm
                List<Renderer> leftGrapplinArmRenderers = new List<Renderer>();
                leftGrapplinHand.GetComponentsInChildren<Renderer>(leftGrapplinArmRenderers);
                if (!leftGrapplinArmRenderers.Contains(leftGrapplinHand.GetComponent<Renderer>()))
                    leftGrapplinArmRenderers.Add(leftGrapplinHand.GetComponent<Renderer>());
                foreach (Renderer rend in leftGrapplinArmRenderers)
                {
                    rend.enabled = false;
                }
                leftGrapplinArm.GetComponent<Renderer>().enabled = false;

                // Disable right propulsion arm
                leftPropulsionArm.GetComponent<Renderer>().enabled = false;

#endregion
                
                // Add new TechType to the buildables
                SMLHelper.V2.Handlers.CraftDataHandler.AddBuildable(this.TechType);
                SMLHelper.V2.Handlers.CraftDataHandler.AddToGroup(TechGroup.Miscellaneous, TechCategory.Misc, this.TechType);

                // Set the buildable prefab
                SMLHelper.V2.Handlers.PrefabHandler.RegisterPrefab(this);

                // Set the custom sprite
                SMLHelper.V2.Handlers.SpriteHandler.RegisterSprite(this.TechType, SpriteManager.Get(TechType.Exosuit));

                // Associate recipe to the new TechType
                SMLHelper.V2.Handlers.CraftDataHandler.SetTechData(this.TechType, this.Recipe);

                this.IsRegistered = true;
            }
        }

        public override GameObject GetGameObject()
        {
            GameObject prefab = GameObject.Instantiate(this.GameObject);

            prefab.name = this.ClassID;
            prefab.transform.localScale *= 4.0f; // Scale prefab
            
            return prefab;
        }
    }
}
