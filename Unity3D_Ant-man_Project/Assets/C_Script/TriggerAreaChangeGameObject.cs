using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaChangeGameObject : MonoBehaviour
{
    public Collider targetCollider;
    public GameObject spawnObjectOnCollision;
    public bool colorSpawnedObject = true;
    public bool destroyOnTargetCollision = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            Vector3 contactP = other.transform.position;

            if (colorSpawnedObject)
            {
                GameObject spawned = (GameObject)Instantiate(spawnObjectOnCollision, contactP, transform.rotation);
            }
        

            if (destroyOnTargetCollision)
                Destroy(other.gameObject);

        }
    }


}

