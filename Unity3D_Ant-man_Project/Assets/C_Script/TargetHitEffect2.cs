//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem.Sample
{
    public class TargetHitEffect2 : MonoBehaviour
    {
        public Collider targetCollider;

        public GameObject spawnObjectOnCollision;

        public bool colorSpawnedObject = true;

        public bool destroyOnTargetCollision = true;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider == targetCollider)
            {
                ContactPoint contact = collision.contacts[0];
                RaycastHit hit;

                float backTrackLength = 1f;
                Ray ray = new Ray(contact.point - (-contact.normal * backTrackLength), -contact.normal);
                if (collision.collider.Raycast(ray, out hit, 2))
                {
                    if (colorSpawnedObject)
                    {
                      
                        GameObject spawned = GameObject.Instantiate(spawnObjectOnCollision);
                        spawned.transform.position = contact.point;
                        spawned.transform.forward = ray.direction;

                        Renderer[] spawnedRenderers = spawned.GetComponentsInChildren<Renderer>();
                       
                    }
                }

                if (destroyOnTargetCollision)
                    Destroy(this.gameObject);
            }
        }
    }
}