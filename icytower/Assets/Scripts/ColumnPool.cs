using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize;
    public GameObject columnPrefab;
    public float SpawnRate = 4f;
    public float columnMin = -1f;
    public float columnMax = 3.5f;
    public float spawnXPos = 5;


    private GameObject[] columns;
    private Vector2 poolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawn;
    private int currentColumn;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f;
        columns = new GameObject[columnPoolSize];
        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, poolPosition, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (!GameController.Instance.GameOver && timeSinceLastSpawn >= SpawnRate)
        {
            timeSinceLastSpawn = 0;
            var spawnYPos = Random.Range(columnMin, columnMax);
            columns[currentColumn].transform.position = new Vector2(spawnXPos, spawnYPos);
            currentColumn++;
            if(currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }
}
