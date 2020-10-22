using UnityEngine;

[ExecuteInEditMode]
public class OrbitVisualizer : MonoBehaviour {
	private CelestialObject[] allBodies;

	private float timeStep = Universe.physicsTimeStep;
	[SerializeField]
	private int iterations = 1000;

	[SerializeField]
	private int focus = 0;

	private void Update() {
		if (focus != 0) {
			DrawOrbits(focus);
		} else {
			DrawOrbits();
		}
	}

	private void DrawOrbits() {
		allBodies = FindObjectsOfType<CelestialObject>();
		VirtualBody[] allVirtualBodies = new VirtualBody[allBodies.Length];
		Vector3[] drawPoint = new Vector3[iterations];

		// initialize virtual bodies
		for (int i = 0; i < allBodies.Length; i++) {
			allVirtualBodies[i] = new VirtualBody(allBodies[i]);
		}

		foreach (VirtualBody body in allVirtualBodies) {
			// simulate
			for (int i = 0; i < iterations; i++) {
				// updating velocity of selected body
				body.UpdateVelocity(allVirtualBodies, timeStep);

				// updating position
				body.UpdatePosition(timeStep);
				drawPoint[i] = body.position;
			}

			// draw orbit
			for (int i = 0; i < iterations - 1; i++) {
				Debug.DrawLine(drawPoint[i], drawPoint[i + 1], body.pathColor);
			}
		}
	}

	private void DrawOrbits(int index) {
		allBodies = FindObjectsOfType<CelestialObject>();
		VirtualBody[] allVirtualBodies = new VirtualBody[allBodies.Length];
		Vector3[] drawPoint = new Vector3[iterations];

		index -= 1;

		// initialize virtual bodies
		for (int i = 0; i < allBodies.Length; i++) {
			allVirtualBodies[i] = new VirtualBody(allBodies[i]);
		}

		// simulate
		for (int i = 0; i < iterations; i++) {
			// updating velocity of selected body
			allVirtualBodies[index].UpdateVelocity(allVirtualBodies, timeStep);

			// updating position
			allVirtualBodies[index].UpdatePosition(timeStep);
			drawPoint[i] = allVirtualBodies[index].position;
		}

		// draw orbit
		for (int i = 0; i < iterations - 1; i++) {
			Debug.DrawLine(drawPoint[i], drawPoint[i + 1], allVirtualBodies[index].pathColor);
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
