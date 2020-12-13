using UnityEngine;

[ExecuteInEditMode]
public class OrbitVisualizer : MonoBehaviour {
	private float timeStep = Universe.physicsTimeStep;
	[SerializeField]
	private int iterations = 1000;

	private void Update() {
		DrawOrbits();
	}

	private void DrawOrbits() {
		CelestialObject[] allBodies = FindObjectsOfType<CelestialObject>();
		VirtualBody[] allVirtualBodies = new VirtualBody[allBodies.Length];
		Vector3[] drawPoints = new Vector3[iterations];

		// Update virtual bodies
		for (int i = 0; i < allBodies.Length; i++) {
			allVirtualBodies[i] = new VirtualBody(allBodies[i]);
		}

		foreach (VirtualBody vBody in allVirtualBodies) {
			// simulate
			for (int i = 0; i < iterations; i++) {
				// updating velocity of selected body
				vBody.UpdateVelocity(allVirtualBodies, timeStep);

				// updating position
				vBody.UpdatePosition(timeStep);
				drawPoints[i] = vBody.position;
			}

			// draw orbit
			for (int i = 0; i < iterations - 1; i++) {
				Debug.DrawLine(drawPoints[i], drawPoints[i + 1], vBody.pathColor);
			}
		}
	}

	class VirtualBody {
		public Vector3 position, velocity;
		public float mass;
		public Color pathColor;

		public VirtualBody(CelestialObject body) {
			position = body.transform.position;
			velocity = body.GetVelocity();
			mass = body.GetMass();
			pathColor = body.gameObject.GetComponent<Renderer>().sharedMaterial.color;
		}

		public void UpdateVelocity(VirtualBody[] allBodies, float timeStep) {
			foreach (VirtualBody other in allBodies) {
				if (other != this) {
					float distanceSquared = (other.position - position).sqrMagnitude;
					Vector3 forceDirection = (other.position - position).normalized;
					Vector3 acceleration = forceDirection * Universe.gravitationalConstant * other.mass / distanceSquared;
					
					velocity += acceleration / timeStep;
				}
			}
		}

		public void UpdatePosition(float timeStep) {
			position += velocity * timeStep;
		}
	}
}
