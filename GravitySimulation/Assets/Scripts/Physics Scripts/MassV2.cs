using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MassV2 : MonoBehaviour {
    public float mass;
    public Vector3 initialVelocity;

    [SerializeField]
    private float velocity, acceleration;

    [SerializeField]
    private MassV2[] masses;

    public Rigidbody rb;

    public Universe universe;

	private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = mass;

        rb.AddForce(initialVelocity);
	}

	private void FixedUpdate() {
        masses = FindObjectsOfType<MassV2>();
        foreach(MassV2 m in masses) {
            if (m != this)
                Accelerate(m.rb);
		}

        velocity = rb.velocity.magnitude;
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
}
