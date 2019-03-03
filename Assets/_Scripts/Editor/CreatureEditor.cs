using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Creature), true)]
public class CreatureEditor : Editor
{
    void OnSceneGUI()
    {
        Creature creature = target as Creature;

        for (int i = 0; i < creature.Targets.Count; i++)
        {
            creature.Targets[i].position = Handles.PositionHandle(creature.Targets[i].position, Quaternion.identity);
            Handles.BeginGUI();
            var rectMin = Camera.current.WorldToScreenPoint(creature.Targets[i].position);
            var rect = new Rect();
            rect.xMin = rectMin.x;
            rect.yMin = SceneView.currentDrawingSceneView.position.height - rectMin.y;
            rect.width = 64;
            rect.height = 18;

            GUILayout.BeginArea(rect);
            GUILayout.Label("Target " + i);
            GUILayout.EndArea();
            Handles.EndGUI();

        }

        Vector3 StartPoint = Quaternion.AngleAxis(-creature.VisionAngle(), Vector3.up) * creature.transform.forward;
        Handles.color = new Color(0.8f, 0.5f, 0f, 0.1f);
        Handles.DrawSolidArc(creature.transform.position, creature.transform.up, StartPoint, 2 * creature.VisionAngle(), creature.RangeOfVision);
    }

    //nem tom hogy fog működni egy olyan creature-nél, akinek nincs sebzése
    //SerializedProperty damageProp;
    Creature c;
    List<Type> conditions;
    //static List<string> conditionNames = new List<string>();

    void OnEnable()
    {
        //LookForConditions();
        //damageProp = serializedObject.FindProperty("damage"); 
        
        c = (Creature)target;
    }

    //private void LookForConditions()
    //{
    //    conditions = ReflectiveEnumerator.GetEnumerableOfType<AbstractCondition>();
    //    SelectedConditions = new List<int>();

    //    for (int i = 0; i < conditions.Count; i++)
    //    {
    //        conditionNames.Add(conditions[i].ToString());
    //        SelectedConditions.Add(0);
    //    }


    //    //ActualCond = 0;
    //    //CalmCond = 0;
    //    //SuspiciousCond = 0;
    //    //AlertedCond = 0;
    //    //KnockedOutCond = 0;
    //    //BlindCond = 0;
    //    //SleepCond = 0;
    //    //DeadCond = 0;
    //    //ActualCond = 0;

    //}

    //List<int> SelectedConditions;


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //serializedObject.Update();

        


        serializedObject.ApplyModifiedProperties();

        ProgressBar(c.Suspicion / 200.0f, c.Suspicion.ToString()); //ezzel nem tökéletes, majd finomítok rajta

        GUI.changed = false;
        //SelectedConditions[0] = EditorGUILayout.Popup("Actual condition", SelectedConditions[0], conditionNames.ToArray());
        //SelectedConditions[1] = EditorGUILayout.Popup("Calm condition", SelectedConditions[1], conditionNames.ToArray());
        //SelectedConditions[2] = EditorGUILayout.Popup("Suspicious condition", SelectedConditions[2], conditionNames.ToArray());
        //SelectedConditions[3] = EditorGUILayout.Popup("Alerted condition", SelectedConditions[3], conditionNames.ToArray());
        //SelectedConditions[4] = EditorGUILayout.Popup("Blind condition", SelectedConditions[4], conditionNames.ToArray());
        //SelectedConditions[5] = EditorGUILayout.Popup("Knocked Out condition", SelectedConditions[5], conditionNames.ToArray());
        //SelectedConditions[6] = EditorGUILayout.Popup("Dead condition", SelectedConditions[6], conditionNames.ToArray());
        //SelectedConditions[7] = EditorGUILayout.Popup("Sleep condition", SelectedConditions[7], conditionNames.ToArray());

        //if (GUI.changed)
        //{

        //    for (int i = 0; i < SelectedConditions.Count; i++)
        //    {
        //        if (!c.gameObject.GetComponent(conditions[SelectedConditions[i]]))
        //        {
        //            c.gameObject.AddComponent(conditions[SelectedConditions[i]]);
        //        }
        //    }
           
        //    c.condition = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[0]]);
        //    c.condition_calm = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[1]]);
        //    c.condition_suspicious = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[2]]);
        //    c.condition_alert = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[3]]);
        //    c.condition_knockeddown = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[4]]);
        //    c.condition_blind = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[5]]);
        //    c.condition_dead = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[6]]);
        //    c.condition_sleep = (AbstractCondition)c.gameObject.GetComponent(conditions[SelectedConditions[7]]);

        //}
    }

    // Custom GUILayout progress bar.
    void ProgressBar(float value, string label)
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        switch ((int)(value / 0.3f))
        {
            case 0:
                GUI.backgroundColor = new Color(0.1f,0.6f,0.1f,0.7f); break;
            case 1:
                GUI.backgroundColor = new Color(1f, 0.9f, 0, 0.7f); break;
            default:
                GUI.backgroundColor = new Color(1f, 0.3f, 0.1f, 0.7f); break;
        }
        GUI.contentColor = Color.white;
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
        GUI.backgroundColor = Color.white;
    }

    void ProgressBarDarkening(float value, string label)//must take a value between 0-1
    {
        // Get a rect for the progress bar using the same margins as a textfield:
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        GUI.backgroundColor = new Color(1.0f, 0.0f, 0.0f, value);
        GUI.contentColor = Color.white;
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
        //GUI.backgroundColor = Color.grey;
    }

}
