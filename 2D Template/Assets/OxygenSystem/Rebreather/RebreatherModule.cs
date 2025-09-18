using UnityEngine;

[CreateAssetMenu(menuName = "Oxygen System/Rebreather Module")]
public class RebreatherModule : ScriptableObject
{
    public bool isEquipped = false;
    public float minDepthToActivate = 5f;
    public float consumptionMultiplier = 0.5f;

    public Sprite icon; // Add this line


    public bool IsActive(float depth,bool IsConsumeOxygen)
    {
        return isEquipped && depth > minDepthToActivate && IsConsumeOxygen;
    }
}
