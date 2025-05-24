using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlaterState = PlayerState.Idle;


    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    public void ChangeState(PlayerState newPlayerState)
    {
        if (_currentPlaterState == newPlayerState) { return; }

        _currentPlaterState = newPlayerState;
    }

    public PlayerState GetCurrentState()
    {
        return _currentPlaterState;
    }
}
