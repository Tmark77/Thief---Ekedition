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
            creature.Targets[i] = Handles.PositionHandle(creature.Targets[i], Quaternion.identity);
            Handles.BeginGUI();
            var rectMin = Camera.current.WorldToScreenPoint(creature.Targets[i]);
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
    static List<string> conditionNames = new List<string>();

    void OnEnable()
    {
        LookForConditions();
        //damageProp = serializedObject.FindProperty("damage"); 
        
        c = (Creature)target;
    }

    private void LookForConditions()
    {
        conditions = ReflectiveEnumerator.GetEnumerableOfType<AbstractCondition>();
        for (int i = 0; i < conditions.Count; i++)
        {
            conditionNames.Add(conditions[i].ToString());
        }
        ActualCond = 0;
        CalmCond = 0;
        SuspiciousCond = 0;
        AlertedCond = 0;
        KnockedOutCond = 0;
        BlindCond = 0;
        SleepCond = 0;
        DeadCond = 0;
        ActualCond = 0;

    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        
        //EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));
        //if (!damageProp.hasMultipleDifferentValues)
        //    ProgressBarDarkening(damageProp.intValue / 100.0f, "Damage");

        serializedObject.ApplyModifiedProperties();

        ProgressBar(c.Suspicion / 200.0f, c.Suspicion.ToString()); //ezzel nem tökéletes, majd finomítok rajta

        GUI.changed = false;
        ActualCond = EditorGUILayout.Popup("Actual condition", ActualCond, conditionNames.ToArray());
        CalmCond = EditorGUILayout.Popup("Calm condition", CalmCond, conditionNames.ToArray());
        SuspiciousCond = EditorGUILayout.Popup("Suspicious condition", SuspiciousCond, conditionNames.ToArray());
        AlertedCond = EditorGUILayout.Popup("Alerted condition", AlertedCond, conditionNames.ToArray());
        BlindCond = EditorGUILayout.Popup("Blind condition", BlindCond, conditionNames.ToArray());
        KnockedOutCond = EditorGUILayout.Popup("Knocked Out condition", KnockedOutCond, conditionNames.ToArray());
        DeadCond = EditorGUILayout.Popup("Dead condition", DeadCond, conditionNames.ToArray());
        SleepCond = EditorGUILayout.Popup("Sleep condition", SleepCond, conditionNames.ToArray());

        if (GUI.changed) //fúj, ez de szar. Meg kéne listázni
        {
            if (!c.gameObject.GetComponent(conditions[ActualCond]))
            {
                c.gameObject.AddComponent(conditions[ActualCond]);
            }
            c.condition = (AbstractCondition)c.gameObject.GetComponent(conditions[ActualCond]);

            if (!c.gameObject.GetComponent(conditions[CalmCond]))
            {
                c.gameObject.AddComponent(conditions[CalmCond]);
            }
            c.condition_calm = (AbstractCondition)c.gameObject.GetComponent(conditions[CalmCond]);

            if (!c.gameObject.GetComponent(conditions[SuspiciousCond]))
            {
                c.gameObject.AddComponent(conditions[SuspiciousCond]);
            }
            c.condition_suspicious = (AbstractCondition)c.gameObject.GetComponent(conditions[SuspiciousCond]);

            if (!c.gameObject.GetComponent(conditions[AlertedCond]))
            {
                c.gameObject.AddComponent(conditions[AlertedCond]);
            }
            c.condition_alert = (AbstractCondition)c.gameObject.GetComponent(conditions[AlertedCond]);

            if (!c.gameObject.GetComponent(conditions[KnockedOutCond]))
            {
                c.gameObject.AddComponent(conditions[KnockedOutCond]);
            }
            c.condition_knockeddown = (AbstractCondition)c.gameObject.GetComponent(conditions[KnockedOutCond]);

            if (!c.gameObject.GetComponent(conditions[BlindCond]))
            {
                c.gameObject.AddComponent(conditions[BlindCond]);
            }
            c.condition_blind = (AbstractCondition)c.gameObject.GetComponent(conditions[BlindCond]);

            if (!c.gameObject.GetComponent(conditions[SleepCond]))
            {
                c.gameObject.AddComponent(conditions[SleepCond]);
            }
            c.condition_sleep = (AbstractCondition)c.gameObject.GetComponent(conditions[SleepCond]);

            if (!c.gameObject.GetComponent(conditions[DeadCond]))
            {
                c.gameObject.AddComponent(conditions[DeadCond]);
            }
            c.condition_dead = (AbstractCondition)c.gameObject.GetComponent(conditions[DeadCond]);

        }


    }

    int CalmCond;
    int SuspiciousCond;
    int AlertedCond;
    int KnockedOutCond;
    int BlindCond;
    int SleepCond;
    int DeadCond;
    int ActualCond;

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
