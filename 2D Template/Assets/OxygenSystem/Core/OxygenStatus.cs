using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;

[CreateAssetMenu(menuName = "Oxygen System/Oxygen Status")]
public class OxygenStatus : ScriptableObject
{
    [SerializeField] private float baseMaxOxygen = 100f;
    [HideInInspector] public OxygenTank equippedLung = null;

    public float CurrentOxygen { get; private set; }

    public UnityEvent onDrowning;
    public List<OxygenWarning> oxygenWarnings = new()
    {
        new OxygenWarning { threshold = 0.1f, message = "Oxygen critical" }
    };

    public float MaxOxygen => baseMaxOxygen + (equippedLung?.capacity ?? 0f);

    public void Initialize()
    {
        CurrentOxygen = MaxOxygen;
        foreach (var w in oxygenWarnings)
            w.hasTriggered = false;
    }

    public void ChangeOxygen(float delta)
    {
        CurrentOxygen = Mathf.Clamp(CurrentOxygen + delta, 0f, MaxOxygen);
        float percent = CurrentOxygen / MaxOxygen;

        if (CurrentOxygen <= 0f)
        {
            onDrowning?.Invoke();
        }

        foreach (var warning in oxygenWarnings)
        {
            if (!warning.hasTriggered && percent <= warning.threshold)
            {
                warning.hasTriggered = true;
                warning.onTriggered?.Invoke(warning.message);
            }
            else if (warning.hasTriggered && percent > warning.threshold)
            {
                warning.hasTriggered = false;
            }
        }
    }

    public void EquipLung(OxygenTank lung)
    {
        equippedLung = lung;
        CurrentOxygen = Mathf.Min(CurrentOxygen, MaxOxygen);
    }

    [ContextMenu("Unequip lung")]
    public void UnequipLung()
    {
        equippedLung = null;
        CurrentOxygen = Mathf.Min(CurrentOxygen, MaxOxygen);
    }
}
