using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObject : MonoBehaviour {
    // FIELDS

	// rigidbody of this object applying the built-in physics system from Unity
    private Rigidbody rb;

	public Vector3 initialVelocity;

	// FUNCTIONS

	// called once at the first frame
	private void Start() {
		SetupRigidbody();
		rb.AddForce(initialVelocity, ForceMode.Force);
	}

	// called every physics timestep
	private void FixedUpdate() {
		ApplyGravitationalField();
	}

	// applying the gravitational field of this object to every other celestial object in the scene
	private void ApplyGravitationalField() {
		// finding all celestial objects in the scene and storing them in an array
		CelestialObject[] objects = FindObjectsOfType<CelestialObject>();
		// looping over all objects and attracting them the this object
		foreach (CelestialObject obj in objects) {
			if (obj != this) {
				// attract object
				AttractObject(obj);
			}
		}
	}

	// attracting a single given celestial object
	private void AttractObject(CelestialObject other) {
		// calculating the direction of the gravitational force
		Vector3 gravDir = transform.position - other.transform.position;
		// calculating the magnitude of the force
		float gravitationalForce = Universe.gravitationalConstant * rb.mass * other.rb.mass / Mathf.Pow(gravDir.magnitude, 2);

		// applying the force to the other object
		other.rb.AddForce(gravDir.normalized * gravitationalForce, ForceMode.Force);
	}

	// setting up all initatial values of the rigidbody
	private void SetupRigidbody() {
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.angularDrag = 0;
		rb.drag = 0;
	}

	// GETTER
	public Rigidbody GetRigidbody() {
		return rb;
	}

	// SETTER
	public void SetMass(float m) {
		rb.mass = m;
	}
}
