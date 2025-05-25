using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesginSO _wheatDesignSO;

    [SerializeField] private PlayerControler _playerControler;


    public void Collect()
    {
        _playerControler.SetJumpingForce(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
