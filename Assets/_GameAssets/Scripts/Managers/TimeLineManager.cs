using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        if (_playableDirector == null)
        {
            Debug.LogError("PlayableDirector component is missing on TimeLineManager.");
        }
    }

    private void OnEnable()
    {
        _playableDirector.Play();
        _playableDirector.stopped += OnTimeLineFinished;
    }

    private void OnTimeLineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play);
    }
}
