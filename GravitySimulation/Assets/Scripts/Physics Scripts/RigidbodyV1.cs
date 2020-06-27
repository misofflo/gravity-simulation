using UnityEngine;

public class RigidbodyV1 : MonoBehaviour {
    private float deltaTime;

    [SerializeField]
    private float mass, radius;

    private Vector3 acceleration;
    private Vector3 velocity;

    private Vector3 angularAcceleration;
    private Vector3 angularVelocity;

    private void Start() {
        deltaTime = Time.fixedDeltaTime;
    }

    private void FixedUpdate() {
        transform.Translate(velocity * deltaTime);
        transform.Rotate(angularVelocity * deltaTime * 180 / Mathf.PI);
    }

    public void AddForce(Vector3 force) {
        // calculation of acceleration and velocity with Newton's basic formulas of mechanics: F = m * a   and   a = dv / dt
        acceleration = force / mass;
        velocity += acceleration * deltaTime;
    }
    public void AddTorque(Vector3 force) {
        // calculation of angular acceleration and velocity with Newton's formulas for torque and rotations: M = r * F   and   M = I * a   and   a = dw / dt
        Vector3 torque = radius * force;
        angularAcceleration = torque / (mass * (radius * radius));
        angularVelocity += angularAcceleration * deltaTime;
    }

    // getter
    public Vector3 GetVelocity() {
        return velocity;
	}
    public Vector3 GetAcceleration() {
        return acceleration;
	}
    public Vector3 GetAngularVelocity() {
        return angularVelocity;
	}
    public Vector3 GetAngularAcceleration() {
        return angularAcceleration;
	}

    // setter
    public void SetMass(float m) {
        mass = m;
	}
    public void SetRadius(float r) {
        radius = r;
	}
}
