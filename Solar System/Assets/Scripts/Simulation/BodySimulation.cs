using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySimulation : MonoBehaviour {
	private CelestialObject[] allBodies;

	private void Awake() {
		allBodies = FindObjectsOfType<CelestialObject>();
	}

	private void FixedUpdate() {
		foreach (CelestialObject body in allBodies) {
			body.UpdateVelocity(allBodies, Universe.physicsTimeStep);
			body.UpdatePosition(Universe.physicsTimeStep);
		}
	}
}
