using UnityEngine;

public class CatStatesController : MonoBehaviour
{
    [SerializeField] private CatState _currentCatState = CatState.Walking;


    private void Start()
    {
        ChangeState(CatState.Walking);
    }

    public void ChangeState(CatState newCatState)
    {
        if (_currentCatState == newCatState) { return; }

        _currentCatState = newCatState;
    }

    public CatState GetCurrentState()
    {
        return _currentCatState;
    }
}
