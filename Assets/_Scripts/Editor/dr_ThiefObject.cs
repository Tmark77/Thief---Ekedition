using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public enum enumMaterials
{
    None,
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


[CustomEditor(typeof(ThiefObject), true)]
public class dr_ThiefObject : Editor
{
    enumMaterials selectedMat;
    Dictionary<enumMaterials, Type> EnumMaterial_Dictionary = new Dictionary<enumMaterials, Type>()
    {
        { enumMaterials.Stone, typeof(StoneMaterial)},
        { enumMaterials.SoftWood, typeof(SoftWoodMaterial)},
        { enumMaterials.HardWood, typeof(HardWoodMaterial)},
        { enumMaterials.Marble, typeof(MarbleMaterial)},
        { enumMaterials.Glass, typeof(GlassMaterial)},
        //{ enumMaterials.Ceramic, typeof(CeramicMaterial)},
        { enumMaterials.Metal, typeof(MetalMaterial)},
        { enumMaterials.Dirt, typeof(DirtMaterial)},
        { enumMaterials.Cloth, typeof(ClothMaterial)}
    };


    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        ThiefObject myTarget = (ThiefObject)target;
        SetSelectedMat(myTarget);
        selectedMat = (enumMaterials)EditorGUILayout.EnumPopup("Material", selectedMat);
        if (GUI.changed)
        {
            ChooseMaterial(myTarget);
            EditorGUIUtility.ExitGUI();
        }
    }

    public void SetSelectedMat(ThiefObject thiefobj)
    {
        if (thiefobj.material is StoneMaterial)
            selectedMat = enumMaterials.Stone;
        if (thiefobj.material is SoftWoodMaterial)
            selectedMat = enumMaterials.SoftWood;
        if (thiefobj.material is HardWoodMaterial)
            selectedMat = enumMaterials.HardWood;
        if (thiefobj.material is MetalMaterial)
            selectedMat = enumMaterials.Metal;
        if (thiefobj.material is GlassMaterial)
            selectedMat = enumMaterials.Glass;
        //if (thiefobj.material is CeramicMaterial)
        //selectedMat = enumMaterials.Ceramic;
        if (thiefobj.material is DirtMaterial)
            selectedMat = enumMaterials.Dirt;
        if (thiefobj.material is ClothMaterial)
            selectedMat = enumMaterials.Cloth;
        if (thiefobj.material is MarbleMaterial)
            selectedMat = enumMaterials.Marble;
        if (thiefobj.material == null)
        {
            selectedMat = enumMaterials.None;
        }
    }

    public void ChooseMaterial(ThiefObject thiefObj)
    {
        abstract_Material aMat = thiefObj.gameObject.GetComponent<abstract_Material>();

        if (aMat == null || !(aMat.GetType().Name == EnumMaterial_Dictionary[selectedMat].Name ))
        {
            DestroyImmediate(aMat);
            thiefObj.gameObject.AddComponent(EnumMaterial_Dictionary[selectedMat]);
        }

        thiefObj.material = thiefObj.gameObject.GetComponent(EnumMaterial_Dictionary[selectedMat]) as abstract_Material;
    }

}
