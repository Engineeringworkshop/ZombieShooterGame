using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileReadWrite
{
	// Guarda un objeto de cualquier tipo en una ruta de tu elección
	public static void WriteToBinaryFile<T>(string filePath, T objectToWrite)
	{
		// Abrimos el archivo, lo creamos si no existe o lo sobreescribimos si ya existe. using File.Open cierra el archivo automaticamente cuando termia el comando
		using (Stream stream = File.Open(filePath, FileMode.Create))
		{
			// Creamos el objeto de formato binario
			var binaryFormatter = new BinaryFormatter();
			// Convertimos el objeto en el formato binario
			binaryFormatter.Serialize(stream, objectToWrite);
		}
	}

	// Leemos un archivo de la ruta elegida y devuelve el objeto del formato correspondiente
	public static T ReadFromBinaryFile<T>(string filePath)
	{
		// Abre el archivo. using File.Open cierra el archivo automaticamente cuando termia el comando
		using (Stream stream = File.Open(filePath, FileMode.Open))
		{
			// creamos el objeto de formato binario
			var binaryFormatter = new BinaryFormatter();
			// Leemos el archivo usando el objeto anterior. Devolvemos lo leido como el objeto correspondiente
			return (T)binaryFormatter.Deserialize(stream);
		}
	}
}
