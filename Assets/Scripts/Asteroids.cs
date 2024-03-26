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

    private void Start()
    {
        thisTransform = transform;
    }
    void FixedUpdate()
    {
        thisTransform.Translate(new Vector3(0, 0, speed), Space.World);

        if (thisTransform.position.z <= triggerZ)
            if (!doDestroy)
                thisTransform.position = new Vector3(thisTransform.position.x, thisTransform.position.y, respawnZ);
            else
                Destroy(gameObject);
    }
}
