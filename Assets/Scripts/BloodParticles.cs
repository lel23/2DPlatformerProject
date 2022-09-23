using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public GameObject splatPrefab;

    private ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Vector3 splatLocation = collisionEvents[i].intersection;
            Quaternion splatAngle = Quaternion.Euler(0, 0, Random.Range(0, 360f));
            Instantiate(splatPrefab, splatLocation, splatAngle, other.transform);
        }
    }
}
