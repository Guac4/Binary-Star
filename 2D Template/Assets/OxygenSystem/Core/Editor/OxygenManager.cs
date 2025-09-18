using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OxygenConsumer))]
public class OxygenManager : Editor
{
    SerializedProperty statusProp;
    SerializedProperty rebreatherProp;
    SerializedProperty tankProp;

    // Add these properties for the new fields
    SerializedProperty baseDrainProp;
    SerializedProperty regenRateProp;
    SerializedProperty depthConsumptionMultiplierMultiplierProp;
    SerializedProperty IsInWater;

    private void OnEnable()
    {
        statusProp = serializedObject.FindProperty("_status");
        rebreatherProp = serializedObject.FindProperty("_rebreather");
        tankProp = serializedObject.FindProperty("_oxygenTank");

        // Find the new fields in the script
        baseDrainProp = serializedObject.FindProperty("baseDrain");
        regenRateProp = serializedObject.FindProperty("regenRate");
        depthConsumptionMultiplierMultiplierProp = serializedObject.FindProperty("depthConsumptionMultiplierMultiplier");
        IsInWater = serializedObject.FindProperty("IsInWater");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Custom fields with headers, manually drawn
        EditorGUILayout.LabelField("Oxygen Modules", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(statusProp, new GUIContent("Oxygen Settings (Status)"));
        if (statusProp.objectReferenceValue == null)
        {
            EditorGUILayout.LabelField("⚠️ Oxygen Status Is NULL!", EditorStyles.boldLabel);
            EditorGUILayout.Space(2);
        }

        EditorGUILayout.PropertyField(rebreatherProp, new GUIContent("Rebreather"));
        EditorGUILayout.PropertyField(tankProp, new GUIContent("Oxygen Tank"));

        // Add the new fields here:
        EditorGUILayout.Space(10); // Add some space between groups of fields
        EditorGUILayout.LabelField("Oxygen Consumption", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(baseDrainProp, new GUIContent("Base Drain"));
        EditorGUILayout.PropertyField(regenRateProp, new GUIContent("Regen Rate"));
        EditorGUILayout.PropertyField(depthConsumptionMultiplierMultiplierProp, new GUIContent("Depth Consumption Multiplier"));


        //if (EditorApplication.isPlaying)
        //{
        //    EditorGUILayout.LabelField(IsInWater?.boolValue != null ? "Script is IN water!" : "Script is OUT of water!");
        //}

        serializedObject.ApplyModifiedProperties();
    }
}
