using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    private PlayerControler _playerController;

    void Awake()
    {
        _playerController = GetComponent<PlayerControler>();
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
}
