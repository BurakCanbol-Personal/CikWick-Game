using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    [SerializeField] private int _maxEggCount = 5;

    private int _currentEggCount;

    void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        if (_currentEggCount == _maxEggCount)
        {
            //TODO: WIN
            Debug.Log("You Win Son Of Bitch!!");
        }

        Debug.Log("Egg Count: " + _currentEggCount);
    }
}
