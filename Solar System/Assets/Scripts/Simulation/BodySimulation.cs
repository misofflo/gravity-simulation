using UnityEngine;

public class BodySimulation : MonoBehaviour {
	private readonly float timeStep = Universe.physicsTimeStep;

	private CelestialObject[] allBodies;

	private void Awake() {
		allBodies = FindObjectsOfType<CelestialObject>();
	}

	// simulation
	private void FixedUpdate() {
		foreach (CelestialObject body in allBodies) {
			body.UpdateVelocity(allBodies, timeStep);
			body.UpdatePosition(timeStep);
		}
	}
}
