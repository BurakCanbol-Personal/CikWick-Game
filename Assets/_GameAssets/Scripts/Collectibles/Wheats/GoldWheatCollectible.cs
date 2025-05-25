using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerControler _playerControler;

    [SerializeField] private float _movementIncreaseSpeed;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControler.SetMovementSpeed(_movementIncreaseSpeed, _resetBoostDuration);
        Destroy(this.gameObject);
    }
}
