
// Contendr� las referencias de cualquier estado en el que est�
public class Zombie1_StateMachine
{
    // Variable que guardar� la referencia al estado actual
    public Zombie1_State CurrentState { get; private set; }

    // inicializa el estado inicial.
    public void Initialize(Zombie1_State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    // Metodo que cambiar� de estado, llama al exit del estado anterior, cambia el estado actual, y llama al Enter del nuevo estado actual.
    public void ChangeState(Zombie1_State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}