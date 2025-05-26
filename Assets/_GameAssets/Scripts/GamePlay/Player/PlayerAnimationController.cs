using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private PlayerControler _playerControler;
    private StateController _stateController;

    private void Awake()
    {
        _playerControler = GetComponent<PlayerControler>();
        _stateController = GetComponent<StateController>();
    }

    private void Start()
    {
        _playerControler.OnPlayerJumped += PlayerController_onPlayerJumped;
    }

    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume) { return; }

        SetPlayerAnimation();
    }

    private void PlayerController_onPlayerJumped()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJumping), 0.5f);
    }

    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }


    private void SetPlayerAnimation()
    {
        var currentState = _stateController.GetCurrentState();

        switch (currentState)
        {
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;

            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;

            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;

            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
