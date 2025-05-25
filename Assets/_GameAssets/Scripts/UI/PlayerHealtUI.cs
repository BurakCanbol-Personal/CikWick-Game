
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealtUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;



    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealtySprite;
    [SerializeField] private Sprite _playerUnHealtySprite;



    [Header("Settings)")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthThransforms;

    private void Awake()
    {
        _playerHealthThransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthThransforms[i] = _playerHealthImages[i].GetComponent<RectTransform>();
        }
    }

    // For Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealtySprite)
            {
                AnimatesDamageSprites(_playerHealthImages[i], _playerHealthThransforms[i]);
                break;
            }
        }
    }

    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealtySprite)
            {
                AnimatesDamageSprites(_playerHealthImages[i], _playerHealthThransforms[i]);
            }
        }
        
    }

    private void AnimatesDamageSprites(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnHealtySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
