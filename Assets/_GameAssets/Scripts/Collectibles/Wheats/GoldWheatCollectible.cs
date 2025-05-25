using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesginSO _wheatDesignSO;

    [SerializeField] private PlayerControler _playerControler;


    public void Collect()
    {
        _playerControler.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
