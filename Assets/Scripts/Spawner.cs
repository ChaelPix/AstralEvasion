using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidsPrfbs;
    [SerializeField] float spawnrate;
    [SerializeField, Range(0, 1)] float spawnrateMargin;

    [SerializeField] private float spawnIncreaseOverTime;

    Vector3 spawnPositionMin, spawnPositionMax;
    [SerializeField] Vector2 size;
    float t;
    float spawnTime;
    int xAsteroidsSpawned;

    void Start()
    {
        spawnPositionMin = transform.position - (transform.localScale);
        spawnPositionMax = transform.position + (transform.localScale);
        SetSpawnTime();
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;

        if(t >= spawnTime)
        {
            InstantiateAsteroid();
            SetSpawnTime();
        }

    }

    void InstantiateAsteroid()
    {
        float posX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
        float posY = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
        Vector3 pos = new Vector3(posX, posY, transform.position.z);

        int x = Random.Range(0, asteroidsPrfbs.Length);
        Transform obj = Instantiate(asteroidsPrfbs[x], pos, Quaternion.identity).transform;
        obj.localScale = Vector3.one * Random.Range(size.x, size.y);

        xAsteroidsSpawned++;
    }

    void SetSpawnTime()
    {
        t = 0;
        spawnTime = (spawnrate * (1 + Random.Range(-spawnrateMargin, spawnrateMargin))) - xAsteroidsSpawned * spawnIncreaseOverTime;
        
    }
}
