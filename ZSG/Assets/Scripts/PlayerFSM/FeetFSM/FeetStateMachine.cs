using UnityEngine;

public class FeetStateMachine
{
    // Variable que guardará la referencia al estado actual
    public FeetState CurrentState { get; private set; }

    // inicializa el estado inicial.
    public void Initialize(FeetState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Metodo que cambiará de estado, llama al exit del estado anterior, cambia el estado actual, y llama al Enter del nuevo estado actual.
    public void ChangeState(FeetState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
