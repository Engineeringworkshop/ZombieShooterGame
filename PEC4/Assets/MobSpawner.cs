using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject prefab;

    [Header("Spawn configuration")]
    
    [SerializeField] public int SpawnAmount = 10;

    [SerializeField] public float minSpawnRadius = 2;

    [SerializeField] public float maxSpawnRadius = 5;


    List<GameObject> entities;

    // Start is called before the first frame update
    void Start()
    {
        entities = new List<GameObject>();
        //entities.Add(player);
        InvokeRepeating("Spawn", 0f, 1.0f / 1000.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if (entities.Count < SpawnAmount)
        {
            GameObject instance = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);

            entities.Add(instance);
        }
        else
        {
            CancelInvoke();
        }
    }

    // Genera una posición aleatoria dentro de un circulo
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;

        float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
        float angle = Random.Range(0, 2 * Mathf.PI);

        randomPosition = transform.position + radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0.0f);

        return randomPosition;
    }
}
