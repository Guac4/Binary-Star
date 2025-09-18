using UnityEngine;
using UnityEngine.UI;

public class OxygenUIController : MonoBehaviour
{
    [SerializeField] private OxygenStatus status;
    [SerializeField] private Slider uiSlider;

    private void OnEnable()
    {
        status.onDrowning.AddListener(OnDrowning);
        uiSlider.maxValue = status.MaxOxygen;
    }

    private void Update()
    {
        uiSlider.maxValue = status.MaxOxygen;
        uiSlider.value = status.CurrentOxygen;
    }

    private void OnDrowning()
    {
        Debug.Log("Player has drowned");
    }
}
