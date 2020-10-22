using UnityEngine;

public class CelestialObject : MonoBehaviour {
    // attributes
    [SerializeField]
    private float mass = 1;

    [SerializeField]
    private Vector3 initialVelocity = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 velocity;

	private void Awake() {
        velocity = initialVelocity;
	}

	// functions
	public void UpdateVelocity(CelestialObject[] allBodies, float timeStep) {
        foreach (CelestialObject other in allBodies) {
            if (other != this) {
                float distanceSquared = (other.transform.position - transform.position).sqrMagnitude;
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                Vector3 force = forceDirection * Universe.gravitationalConstant * mass * other.mass / distanceSquared;
                Vector3 acceleration = force / mass;

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
