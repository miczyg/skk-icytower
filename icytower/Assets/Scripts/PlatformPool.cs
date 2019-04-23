using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public int platformPoolSize;
    public GameObject platformPrefab;
    public float SpawnRate = 4f;
    public float platformXMin = -5f;
    public float platformXMax = 5f;
    public float platformYMin = 5f;
    public float platformYMax = 7f;
    //public float spawnYPos = 5;


    private GameObject[] platforms;
    private Vector2 poolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawn;
    private int currentPlatform;
    private float scaleMin = 0.8f;
    private float scaleMax = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = SpawnRate;
        platforms = new GameObject[platformPoolSize];
        for (int i = 0; i < platformPoolSize; i++)
        {
            platforms[i] = (GameObject)Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (!GameController.Instance.GameOver && timeSinceLastSpawn >= SpawnRate)
        {
            timeSinceLastSpawn = 0;
            var spawnXPos = Random.Range(platformXMin, platformXMax);
            var spawnYPos = Random.Range(platformYMin, platformYMax);
            var scale = Random.Range(scaleMin, scaleMax);

            platforms[currentPlatform].transform.position = new Vector2(spawnXPos, spawnYPos);
            platforms[currentPlatform].transform.localScale = new Vector3(scale, scale, 0);

            currentPlatform++;
            if(currentPlatform >= platformPoolSize)
            {
                currentPlatform = 0;
            }
        }
    }
}
