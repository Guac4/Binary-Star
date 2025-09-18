using UnityEngine;

public class OxygenConsumer : MonoBehaviour
{
    [Header("Managing Oxygen")]
    [SerializeField] private OxygenStatus _status;

    [Header("Rebreather")]
    [SerializeField] private RebreatherModule _rebreather;

    [Header("Oxygen Tank")]
    [SerializeField] private OxygenTank _oxygenTank;

    // Public accessors
    public OxygenStatus Status => _status;
    public RebreatherModule Rebreather => _rebreather;

    public OxygenTank OxygenTank
    {
        get => _status?.equippedLung;
        private set
        {
            if (_status != null)
                _status.equippedLung = value;
        }
    }

    private void OnValidate()
    {
        // Optional: sync the value from field to Status during editing
        if (_status != null)
        {
            _status.equippedLung = _oxygenTank;
        }
    }

    [Tooltip("The base of how much does the oxygen decreases every seconds when underwater.")]
    [SerializeField] private float baseDrain = 1f;
    [Tooltip("How much does the oxygen increases every seconds when has oxygen.")]
    [SerializeField] private float regenRate = 1f;
    [Tooltip("How much does the oxygen consumption increases by depth. \n For Instance, if it is '0.1', the player is in 100m depth and the baseDrain is 1, then 10 oxygen will be drained by every second without rebreather.")]
    [SerializeField] private float depthConsumptionMultiplierMultiplier = 0.1f;


    private IWaterDetector detector;

    private void Awake()
    {
        detector = GetComponent<IWaterDetector>();
    }

    private void OnEnable()
    {
        Status.Initialize();
    }

    [HideInInspector]
    public float depth { get; private set; }
    public bool IsInWater { get; private set; } = false;

    private void Update()
    {
        IsInWater = detector.IsUnderwater(transform.position, out float _depth);
        depth = _depth;

        if (!IsInWater)
        {
            // Above water: regenerate
            float regen = regenRate + (Status.equippedLung?.regenRate ?? 0f);
            Status.ChangeOxygen(regen * Time.deltaTime);
        }
        else
        {
            // Underwater: drain
            float drain = GetOxygenDrain(depth);
            Status.ChangeOxygen(-drain * Time.deltaTime);
        }
    }

    private float GetOxygenDrain(float depth)
    {
        float depthEffect = depth * depthConsumptionMultiplierMultiplier;
        float drain = baseDrain + depthEffect;

        if (Rebreather != null && Rebreather.IsActive(depth, IsInWater))
            drain *= Rebreather.consumptionMultiplier;

        if (Status.equippedLung != null)
            drain *= Status.equippedLung.depletionModifier;

        return drain;
    }

    public void EquipRebreather(RebreatherModule module)
    {
        _rebreather = module;
        module.isEquipped = true;
    }

    public void UnequipRebreather()
    {
        if (Rebreather != null)
            Rebreather.isEquipped = false;
        _rebreather = null;
    }
}
