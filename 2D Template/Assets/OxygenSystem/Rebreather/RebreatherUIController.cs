using UnityEngine;
using UnityEngine.UI;

public class RebreatherUIController : MonoBehaviour
{
    [SerializeField] private Image rebreatherIconImage;
    [SerializeField] private OxygenConsumer consumer;

    private void Update()
    {
        var rebreather = consumer.Rebreather;
        if (
            rebreather != null &&
            rebreather.icon != null &&
            (bool)(rebreather?.IsActive(consumer.depth,consumer.IsInWater)))
        {
            rebreatherIconImage.sprite = rebreather.icon;
            rebreatherIconImage.enabled = true;
        }
        else
        {
            rebreatherIconImage.enabled = false;
        }
    }
}
