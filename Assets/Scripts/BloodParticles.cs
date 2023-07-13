using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public GameObject splatPrefab;
    private ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        // make list of all collisions to loop through
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        ParticlePhysicsExtensions.GetCollisionEvents(particle, other, collisionEvents);
        int count = collisionEvents.Count;

        // make splatPrefab for each collision
        for (int i = 0; i < count; i++)
        {
            if (collisionEvents[i].colliderComponent == null) continue;
            Vector3 splatLocation = collisionEvents[i].intersection;
            Quaternion splatAngle = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            //Debug.Log(collisionEvents[i].colliderComponent);
            //Debug.Log(collisionEvents[i]);
            GameObject parent = collisionEvents[i].colliderComponent.gameObject;
            Instantiate(splatPrefab, splatLocation, splatAngle, parent.transform);
        }
    }
}
