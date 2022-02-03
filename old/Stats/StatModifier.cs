namespace Pablo.CharacterStats
{
    // Tipos de modificadores de estadisticas. Cantidad fija o porcentaje
    // Sobreescribimos los valores base de 0,1,2... Para dar más flexibilidad en caso de querer añadir un orden distinto.
    // Podemos añadir un Flat con orden 250, que se aplicará despues de los PercentAdd, por ejemplo.
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200, // Porcentajes aditivos (10% al daño)
        PercentMult = 300, // Porcentajes multiplicativos (Daño x2)
    }

    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source; // Podrá ser cualquier objeto programable. Util para jugadores y desarrolladores.

        public StatModifier(float value, StatModType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        // Este es el constructor que llamaremos para crear los modificadores
        // Llama al constructor de 3 parametros usando el valor numerico del enum.
        // De esta forma, asignará orden 0 a los Flat y orden 1 a los Percent.
        // Esto nos permitirá calcular el valor por orden. Primero sumando los Flat, y luego los Percent.
        // Así maximizaremos el valor del estat, independizando el resultado del orden de equipamiento.

        // He añadido todos los contructores para facilitar el desarrollo del juego.
        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}