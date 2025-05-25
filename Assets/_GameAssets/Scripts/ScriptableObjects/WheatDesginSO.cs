using UnityEngine;

[CreateAssetMenu(fileName ="WheatDesginSO", menuName = "ScriptableObjects/WheatDesignSO")]
public class WheatDesginSO : ScriptableObject
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    [SerializeField] private float _resetBoostDuration;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    public float ResetBoostDuration => _resetBoostDuration;
}
