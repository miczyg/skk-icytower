using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    #region Serialized
    [SerializeField] private int platformPoolSize;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float SpawnRate = 4f;
    [SerializeField] private float platformXMin = -5f;
    [SerializeField] private float platformXMax = 5f;
    [SerializeField] private float platformYMin = 5f;
    [SerializeField] private float platformYMax = 7f;
    [SerializeField] private float scaleMin = 0.8f;
    [SerializeField] private float scaleMax = 1.2f;
    #endregion Serialized

    #region Components
    private GameObject[] platforms;
    private Vector2 poolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawn;
    private int currentPlatform;
    #endregion Components

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
            var spawnXPos = Random.Range(platformXMin, platformXMax) / GameController.Instance.ScrollSpeed;
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
