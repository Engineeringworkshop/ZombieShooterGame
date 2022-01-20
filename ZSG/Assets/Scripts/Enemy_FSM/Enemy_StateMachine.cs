
// Contendr� las referencias de cualquier estado en el que est�
public class Enemy_StateMachine
{
    // Variable que guardar� la referencia al estado actual
    public Enemy_State CurrentState { get; private set; }

    // inicializa el estado inicial.
    public void Initialize(Enemy_State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Metodo que cambiar� de estado, llama al exit del estado anterior, cambia el estado actual, y llama al Enter del nuevo estado actual.
    public void ChangeState(Enemy_State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}