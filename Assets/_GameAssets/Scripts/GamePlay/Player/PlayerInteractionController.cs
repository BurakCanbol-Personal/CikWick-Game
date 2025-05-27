using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{

    [SerializeField] private Transform _playerTransform;

    private PlayerControler _playerController;
    private Rigidbody _playerRigidbody;
    
    void Awake()
    {
        _playerController = GetComponent<PlayerControler>();
        _playerRigidbody = GetComponent<Rigidbody>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerTransform);
            CameraShake.Instance.ShakeCamera(1f, 0.5f);
        }
    }
}
