using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{

    [SerializeField] float speed;
    [SerializeField] float triggerZ;
    [SerializeField] float respawnZ;
    [SerializeField] bool doDestroy = false;
    private Transform thisTransform;

    private PlanetSpawner[] planets;

    private void Start()
    {
        thisTransform = transform;
        planets = GetComponentsInChildren<PlanetSpawner>();
    }

    void FixedUpdate()
    {
        thisTransform.Translate(new Vector3(0, 0, speed), Space.World);

        if (thisTransform.position.z <= triggerZ)
        {
            if (!doDestroy)
                thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, respawnZ);
            else
                Destroy(gameObject);

            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].PlanetTrigger();
            }
        }
            
    }
}
