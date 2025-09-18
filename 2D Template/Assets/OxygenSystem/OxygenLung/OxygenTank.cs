using UnityEngine;

[CreateAssetMenu(menuName = "Oxygen System/Oxygen Lung")]
public class OxygenTank : ScriptableObject
{
    public float capacity = 75f;
    public float regenRate = 1f;
    public float depletionModifier = 1f;
}
