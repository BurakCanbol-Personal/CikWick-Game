
using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesginSO _wheatDesignSO;

    [SerializeField] private PlayerControler _playerControler;

    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoostTransform;
    private Image _playerBoosterImage;


    void Awake()
    {
        _playerBoostTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoostTransform.GetComponent<Image>();
    }

    public void Collect()
    {
        _playerControler.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseMultiplier, _wheatDesignSO.ResetBoostDuration);

        _playerStateUI.PlayerBoosterUIAnimations(_playerBoostTransform, _playerBoosterImage, _playerStateUI.GetGoldBoosterWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.PassiveSprite, _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.PassiveWheatSprite, _wheatDesignSO.ResetBoostDuration);

        //CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
