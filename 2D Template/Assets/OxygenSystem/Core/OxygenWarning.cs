using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class OxygenWarning
{
    [Range(0f, 1f)]
    public float threshold; // e.g., 0.1 = 10%

    public string message;  // e.g., "Oxygen Critical"
    public UnityEvent<string> onTriggered; // message passed as parameter

    [HideInInspector] public bool hasTriggered;
}
