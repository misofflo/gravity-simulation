using UnityEngine;

[ExecuteInEditMode]
public class OrbitVisualizer : MonoBehaviour {
	private CelestialObject[] allBodies;

	[SerializeField]
	private int selectedBodyIndex = 0;

	private float timeStep = Universe.physicsTimeStep;
	[SerializeField]
	private int iterations = 1000;

	private void Update() {
		DrawOrbit(selectedBodyIndex);
	}

	private void DrawOrbit(int bodyIndex) {
		allBodies = FindObjectsOfType<CelestialObject>();
		VirtualBody[] allVirtualBodies = new VirtualBody[allBodies.Length];
		Vector3[] drawPoint = new Vector3[iterations];

		// initialize virtual bodies
		for (int i = 0; i < allBodies.Length; i++) {
			allVirtualBodies[i] = new VirtualBody(allBodies[i]);
		}

		// simulate
		for (int step = 0; step < iterations; step++) {
			// updating velocity of selected body
			allVirtualBodies[bodyIndex].UpdateVelocity(allVirtualBodies, timeStep);

			// updating position
			allVirtualBodies[bodyIndex].UpdatePosition(timeStep);
			drawPoint[step] = allVirtualBodies[bodyIndex].position;
		}

		// draw orbit
		for (int i = 0; i < iterations - 1; i++) {
			Debug.DrawLine(drawPoint[i], drawPoint[i + 1], Color.red);
		}
	}

	class VirtualBody {
		public Vector3 position, velocity;
		public float mass;

		public VirtualBody(CelestialObject body) {
			position = body.transform.position;
			velocity = body.GetVelocity();
			mass = body.GetMass();
		}

		public void UpdateVelocity(VirtualBody[] allBodies, float timeStep) {
			foreach (VirtualBody other in allBodies) {
				if (other != this) {
					float distanceSquared = (other.position - position).sqrMagnitude;
					Vector3 forceDirection = (other.position - position).normalized;
					Vector3 force = forceDirection * Universe.gravitationalConstant * mass * other.mass / distanceSquared;
					Vector3 acceleration = force / mass;

					velocity += acceleration / timeStep;
				}
			}
		}

		public void UpdatePosition(float timeStep) {
			position += velocity * timeStep;
		}
	}
}
