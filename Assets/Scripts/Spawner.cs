using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Asteroids[] asteroidsPrfbs;
    [SerializeField] float spawnrate;
    [SerializeField, Range(0, 1)] float spawnrateMargin;
    [SerializeField] private int asteroidBySpawn = 3;
    [SerializeField] private float spawnIncreaseOverTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float delayByAsteroid;

    [HideInInspector] public float speed;
    [HideInInspector] public float boostSpeed = 1;
    Vector3 spawnPositionMin, spawnPositionMax;
    [SerializeField] Vector2 size;
    float t;
    float spawnTime;
    int xAsteroidsSpawned;
    float secondsElapsed;

    void Start()
    {
        spawnPositionMin = transform.position - (transform.localScale);
        spawnPositionMax = transform.position + (transform.localScale);
        SetSpawnTime();
    }

    void FixedUpdate()
    {
        t += Time.deltaTime * boostSpeed;
        secondsElapsed += Time.deltaTime * boostSpeed;

        if(t >= spawnTime)
        {
            StartCoroutine(InstantiateAsteroid());
            SetSpawnTime();
        }

    }

    IEnumerator InstantiateAsteroid()
    {
       for (int i = 0; i < asteroidBySpawn; i++)
        {
            float posX = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
            float posY = Random.Range(spawnPositionMin.x, spawnPositionMax.x);
            Vector3 pos = new Vector3(posX, posY, transform.position.z);

            int x = Random.Range(0, asteroidsPrfbs.Length);
            Asteroids obj = Instantiate(asteroidsPrfbs[x], pos, Quaternion.identity);
            obj.speed = speed;
            obj.transform.localScale = Vector3.one * Random.Range(size.x, size.y);

            xAsteroidsSpawned++;
            yield return new WaitForSeconds(delayByAsteroid);
       }
    }

    void SetSpawnTime()
    {
        t = 0;
        spawnTime = Mathf.Max(((spawnrate * (1 + Random.Range(-spawnrateMargin, spawnrateMargin))) - (int)secondsElapsed * spawnIncreaseOverTime), minSpawnTime);
        
    }
}
