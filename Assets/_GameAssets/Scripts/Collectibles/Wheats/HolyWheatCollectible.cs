using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesginSO _wheatDesignSO;

    [SerializeField] private PlayerControler _playerControler;

    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoostTransform;
    private Image _playerBoosterImage;


    void Awake()
    {
        _playerBoostTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoostTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        _playerControler.SetJumpingForce(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

         _playerStateUI.PlayerBoosterUIAnimations(_playerBoostTransform, _playerBoosterImage, _playerStateUI.GetHolyBoosterWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite, _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);

        Destroy(this.gameObject);
    }
}
