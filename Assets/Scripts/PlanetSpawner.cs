using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour 
{
    [SerializeField] private GameObject[] planetPrfb;
    [SerializeField] private Material[] planetMaterials;
    [Space]
    [SerializeField, Range(0, 100)] private float probToSpawn;
    [SerializeField] private float randomY;
    [SerializeField, Range(0, 0.9f)] private float randomSize;

    GameObject planet;

    private void Start()
    {
        PlanetTrigger();
    }
    public void PlanetTrigger()
    {
        if(planet != null)
            Destroy(planet);

        int x = Random.Range(0, 100);
        if(x <= probToSpawn)
        {
            SpawnPlanet();
        }
    }

    void SpawnPlanet()
    {
        planet = Instantiate(planetPrfb[Random.Range(0, planetPrfb.Length)], transform);
        planet.GetComponent<MeshRenderer>().material = planetMaterials[Random.Range(0, planetMaterials.Length)];
        planet.transform.Translate(new Vector3(0, Random.Range(-randomY, randomY), 0));
        planet.transform.localScale += Vector3.one *  Random.Range(-randomSize, randomSize);
    }

}