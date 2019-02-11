using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum enumMaterials
{
    Stone,
    SoftWood,
    HardWood,
    Marble,
    Glass,
    Ceramic,
    Metal,
    Dirt,
    Cloth
}


[DisallowMultipleComponent]
public abstract class ThiefObject : MonoBehaviour
{

	[HideInInspector] public abstract_Material material;
    private enumMaterials ImCrying = (enumMaterials)0;
    public enumMaterials cry
    {
        get { return ImCrying; }
        set
        {
            ImCrying = value;
            ChooseMaterial();
        }
    }

    public void ChooseMaterial()
    {
        abstract_Material aMat = this.gameObject.GetComponent<abstract_Material>();

        switch (cry)
        {
            case enumMaterials.Stone:
                if (!(aMat is StoneMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    this.gameObject.AddComponent<StoneMaterial>();
                }

                material = this.gameObject.GetComponent<StoneMaterial>();
                break;
            case enumMaterials.SoftWood:
                if (!(aMat is SoftWoodMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    this.gameObject.AddComponent<SoftWoodMaterial>();
                }
                
                material = this.gameObject.GetComponent<SoftWoodMaterial>();
                break;
            case enumMaterials.HardWood:
                if (!(aMat is HardWoodMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    this.gameObject.AddComponent<HardWoodMaterial>();
                }
                
                material = this.gameObject.GetComponent<HardWoodMaterial>();
                break;
            case enumMaterials.Marble:
                if (!(aMat is MarbleMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    gameObject.AddComponent<MarbleMaterial>();
                }
               
                material = gameObject.GetComponent<MarbleMaterial>();
                break;
            case enumMaterials.Glass:
                if (!(aMat is GlassMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    gameObject.AddComponent<GlassMaterial>();
                }
                
                material = gameObject.GetComponent<GlassMaterial>();
                break;
            case enumMaterials.Ceramic:
                //if (!(aMat is CeramicMaterial) || aMat == null)
                //{
                //    Destroy(aMat);
                //}
                //thiefObj.gameObject.AddComponent<CeramicMaterial>();
                //thiefObj.material = thiefObj.gameObject.GetComponent<CeramicMaterial>();
                break;
            case enumMaterials.Metal:
                if (!(aMat is MetalMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    gameObject.AddComponent<MetalMaterial>();
                }
               
                material = gameObject.GetComponent<MetalMaterial>();
                break;
            case enumMaterials.Dirt:
                if (!(aMat is DirtMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    gameObject.AddComponent<DirtMaterial>();
                }
                
                material = gameObject.GetComponent<DirtMaterial>();
                break;
            case enumMaterials.Cloth:
                if (!(aMat is ClotheMaterial) || aMat == null)
                {
                    DestroyImmediate(aMat);
                    gameObject.AddComponent<ClotheMaterial>();
                }
                
                material = gameObject.GetComponent<ClotheMaterial>();
                break;
            default:
                break;
        }
    }
}
