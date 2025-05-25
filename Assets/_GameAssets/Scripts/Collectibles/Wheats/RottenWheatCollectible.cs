using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerControler _playerControler;

    [SerializeField] private float _movemenDecreaseSpeed;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControler.SetMovementSpeed(_movemenDecreaseSpeed, _resetBoostDuration);
        Destroy(this.gameObject);
    }
}
