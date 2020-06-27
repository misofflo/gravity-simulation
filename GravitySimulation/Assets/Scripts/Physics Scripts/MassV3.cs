using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MassV3 : MonoBehaviour {
    public Vector3 initialVelocity;

    public float density;
    public float diameter;
    public float mass;

    [SerializeField]
    private float velocity, acceleration;
    private Vector3 velocity3D;

    private MassV3[] masses;

    private Rigidbody rb;
    public Universe universe;

	private void Start() {
        if (!(density == 0 && diameter == 0)) {
            mass = CalculateMass(density, diameter);
        }

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;

        rb.AddForce(initialVelocity);
    }

    private void FixedUpdate() {
        masses = FindObjectsOfType<MassV3>();
        foreach (MassV3 m in masses) {
            if (m != this)
                Accelerate(m.rb);
        }

        velocity = rb.velocity.magnitude;
        velocity3D = rb.velocity;
        Debug.DrawLine(transform.position, transform.position + rb.velocity.normalized * rb.velocity.magnitude, Color.blue);
    }

    public void Accelerate(Rigidbody rbToAccelerate) {
        float m1 = mass;
        float m2 = rbToAccelerate.mass;

        Vector3 gravDir = transform.position - rbToAccelerate.transform.position;
        float distance = gravDir.magnitude;

        // gravitational force between masses
        float Fg = Universe.G * m1 * m2 / Mathf.Pow(distance, 2) * universe.gravMultiplier;

        acceleration = Fg / mass;
        rbToAccelerate.AddForce(gravDir * Fg);
    }

    private float CalculateMass(float pDensity, float pDiameter) {
        return pDensity * 4 / 3 * Mathf.PI * Mathf.Pow(pDiameter/2, 3);
	}

    public Vector3 GetVelocity() {
        return velocity3D;
	}
}
