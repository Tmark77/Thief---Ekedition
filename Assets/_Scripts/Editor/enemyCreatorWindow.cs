using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;
using UnityEngine.AI;

public class enemyCreatorWindow : EditorWindow
{
    [MenuItem("Thief/EnemyCreator")]
    [MenuItem("GameObject/EnemyCreator", false, 1)]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(enemyCreatorWindow), false, "Enemy Creator");
    }

    public void Awake()
    {
        LookForCreatures();
        obj = new GameObject();
        obj.hideFlags = HideFlags.HideInHierarchy;
        ChosenCreature = 0;
        SetUpNewCreatureType();
    }

    GameObject obj;
    List<Type> creatureTypes;
    string nameOfThiefObject = "name";
    ThiefObject creature;
    public int ChosenCreature;
    static List<string> scriptsNames = new List<string>();
    

    public void OnGUI()
    {
       
        GUI.changed = false;
        ChosenCreature = EditorGUILayout.Popup("Creature", ChosenCreature, scriptsNames.ToArray());
        
        if (GUI.changed)
        {
            SetUpNewCreatureType();
        }
        nameOfThiefObject = GUILayout.TextField(nameOfThiefObject);

        if (creature != null)
        {
            var editor = Editor.CreateEditor(creature);
            editor.OnInspectorGUI();
        }


        if (GUILayout.Button("Create"))
        {
            GameObject parent = new GameObject();
            parent.name = nameOfThiefObject;
            parent.AddComponent<NavMeshAgent>();
            obj.name = nameOfThiefObject.ToUpper();
            obj.hideFlags = HideFlags.None;
            obj.transform.parent = parent.transform;
            
        }

        
    }

    private void SetUpNewCreatureType()
    {
        nameOfThiefObject = scriptsNames[ChosenCreature];
        if (obj.GetComponent<ThiefObject>())
        {
            GameObject.DestroyImmediate(obj.GetComponent<ThiefObject>());
        }
        obj.AddComponent(creatureTypes[ChosenCreature]);
        creature = obj.GetComponent(creatureTypes[ChosenCreature]) as ThiefObject;
    }

    private void LookForCreatures()
    {
        creatureTypes = ReflectiveEnumerator.GetEnumerableOfType<Creature>();
        for (int i = 0; i < creatureTypes.Count; i++)
        {
            scriptsNames.Add(creatureTypes[i].ToString());
        }
        ChosenCreature = 0;

    }

    private void OnDestroy()
    {
        if(obj.hideFlags == HideFlags.HideInHierarchy)
            GameObject.DestroyImmediate(obj);
    }

}

public static class ReflectiveEnumerator
{
    static ReflectiveEnumerator() { }

    public static List<Type> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
    {
        List<Type> types = new List<Type>();
        foreach (Type type in
            Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
        {
            types.Add(type);
        }
        return types;
    }
}
