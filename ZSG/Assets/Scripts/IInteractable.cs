// Interface para los elementos con los que se podr�n interactuar
public interface IInteractable
{
    bool IsTriggered { get; } // Defino solo la propiedad get para definir el set en la clase que lo implemente

    void InteractionMethod();
}
