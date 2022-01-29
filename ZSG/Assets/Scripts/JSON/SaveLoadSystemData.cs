using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// hacemos la clase static para que pueda acceder cualqueir script
public static class SaveLoadSystemData
{
    // metodo para guardar archivos Json
    public static void SaveData<T>(T data, string path, string fileName)
    {
        // Creamos el path, el de la aplicación más el que le digamos para el archivo
        string fullPath = Application.persistentDataPath + "/" + path + "/";

        // Comprobamos que el directorio existe, si no le creamos
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }

        // creamos el objeto json a aprtir del data
        string json = JsonUtility.ToJson(data);

        // escribimos el archivo
        File.WriteAllText(fullPath + fileName + ".json", json);

        // Debug
        Debug.Log("Save data ok on: " + fullPath);
    }

    // metodo para leer archivo json
    public static T LoadData<T>(string path, string fileName)
    {
        // Creamos el path con el nombre del archivo y la extensión
        string fullPath = Application.persistentDataPath + "/" + path + "/" + fileName + ".json";

        // Comprobamos que el archivo existe y lo leemos
        if (File.Exists(fullPath))
        {
            // Leemos el archivo como texto
            string textJson = File.ReadAllText(fullPath);

            // Convertimos el texto en objeto
            var obj = JsonUtility.FromJson<T>(textJson);

            return obj;
        }
        else
        {
            Debug.Log("Not file found on load data");
            
            // En caso de que no encuentre el archivo devuelve default, por que no sabemos que es T. NO PODEMOS DEVOVLER null SI NO SABEMOS QUE TIPO ES.
            return default;
        }

    }
}
