using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerControler _playerControler;

    [SerializeField] private float _forceIncrease;
    [SerializeField] private float _resetBoostDuration;

    public void Collect()
    {
        _playerControler.SetJumpingForce(_forceIncrease, _resetBoostDuration);
        Destroy(this.gameObject);
    }
}
