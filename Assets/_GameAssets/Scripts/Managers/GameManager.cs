using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;


    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private CatController _catController;
    [SerializeField] private PlayerHealtUI _playerHealthUI;



    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private float _delay;



    private int _currentEggCount;
    private GameState _currentGameState;
    private bool _isCatChatched;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        if (!_isCatChatched)
        {
            _playerHealthUI.AnimateDamageForAll();
            StartCoroutine(OnGameOver());
            CameraShake.Instance.ShakeCamera(1.5f, 1.5f, 0.5f);
            _isCatChatched = true;
        }
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver());
    }

    void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);

        if (_currentEggCount == _maxEggCount)
        {
            //WIN
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
    }

    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);

        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();

    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
