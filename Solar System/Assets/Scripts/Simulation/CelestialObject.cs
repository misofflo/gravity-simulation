using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CelestialObject : MonoBehaviour {
    // attributes
    public float mass;
    public float radius;
    public float surfaceGravity;

    public Vector3 initialVelocity = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 currentVelocity;

    [SerializeField]
    private new Rigidbody rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.angularDrag = 0;
        rigidbody.drag = 0;
        mass = (mass == 0) ? CalculateMass() : mass;
        surfaceGravity = (surfaceGravity == 0) ? CalculateSurfaceGravity() : surfaceGravity;

        rigidbody.mass = mass;
        currentVelocity = initialVelocity;
    }

	// functions
	public void UpdateVelocity(CelestialObject[] allBodies, float timeStep) {
        foreach (CelestialObject other in allBodies) {
            if (other != this) {
                float distanceSquared = (other.rigidbody.position - rigidbody.position).sqrMagnitude;
                Vector3 forceDirection = (other.rigidbody.position - rigidbody.position).normalized;
                Vector3 force = forceDirection * Universe.gravitationalConstant * rigidbody.mass * other.rigidbody.mass / distanceSquared;
                Vector3 acceleration = force / mass;

                currentVelocity += acceleration / timeStep;
            }
		}
	}

    public void UpdateVelocity(Vector3 acceleration, float timeStep) {
        currentVelocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep) {
        rigidbody.position += currentVelocity * timeStep;
	}

    private float CalculateMass() {
        return surfaceGravity * radius * radius / Universe.gravitationalConstant;
	}

    private float CalculateSurfaceGravity() {
        return Universe.gravitationalConstant * rigidbody.mass / (radius * radius);
	}

    public Vector3 getCurrentVelocity() {
        return currentVelocity;
	}
}
