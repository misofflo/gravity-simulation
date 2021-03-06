﻿using UnityEngine;

public class CelestialObject : MonoBehaviour {
    // attributes
    [SerializeField]
    private float mass = 1;

    [SerializeField]
    private Vector3 velocity;

	// functions
	public void UpdateVelocity(CelestialObject[] allBodies, float timeStep) {
        foreach (CelestialObject other in allBodies) {
            if (other != this) {
                float distanceSquared = (other.transform.position - transform.position).sqrMagnitude;
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                Vector3 acceleration = forceDirection * Universe.gravitationalConstant * other.mass / distanceSquared;

                velocity += acceleration / timeStep;
            }
		}
	}

    public void UpdatePosition(float timeStep) {
        transform.Translate(velocity * timeStep);
	}

    public float GetMass() {
        return mass;
	}

    public Vector3 GetVelocity() {
        return velocity;
	}
}
