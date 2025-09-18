using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public interface IWaterDetector
{
    bool IsUnderwater(Vector3 position, out float depth);
}

public class WaterDetector : MonoBehaviour, IWaterDetector
{
    [SerializeField] private WaterSurface targetSurface;
    [SerializeField] private string waterTag, noWaterTag;

    private int waterColliderCount = 0;

    public bool IsUnderwater(Vector3 position, out float depth)
    {
        depth = 0f;
        if (targetSurface == null || waterColliderCount <= 0)
            return false;

        var p = position;
        var search = new WaterSearchParameters
        {
            startPositionWS = p,
            targetPositionWS = p,
            error = 0.01f,
            maxIterations = 8
        };

        if (targetSurface.ProjectPointOnWaterSurface(search, out var result))
        {
            float waterY = result.projectedPositionWS.y;
            depth = Mathf.Max(0f, waterY - position.y);
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag(waterTag))
            waterColliderCount++;
        if(c.CompareTag(noWaterTag))
            waterColliderCount--;
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.CompareTag(waterTag))
            waterColliderCount--;
        if (c.CompareTag(noWaterTag))
            waterColliderCount++;
    }
}
