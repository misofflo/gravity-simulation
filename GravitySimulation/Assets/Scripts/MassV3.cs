using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody))]
public class MassV3 : MonoBehaviour {
    public Vector3 initialVelocity;

    public float density;
    public float diameter;
    public int trajectorySegments = 1000;

    [SerializeField]
    private float velocity, acceleration, mass;

    private MassV3[] masses;

    private Rigidbody rb;
    public LineRenderer lineRenderer;
    public Constants consts;

    private void Start() {
        mass = CalculateMass(density, diameter);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = trajectorySegments;

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
        Debug.DrawLine(transform.position, transform.position + rb.velocity.normalized * rb.velocity.magnitude, Color.blue);
    }

    public void Accelerate(Rigidbody rbToAccelerate) {
        float m1 = mass;
        float m2 = rbToAccelerate.mass;

        Vector3 gravDir = transform.position - rbToAccelerate.transform.position;
        float distance = gravDir.magnitude;

        // gravitational force between masses
        float Fg = consts.G * m1 * m2 / Mathf.Pow(distance, 2) * consts.gravMultiplier;

        acceleration = Fg / mass;
        rbToAccelerate.AddForce(gravDir * Fg);
    }

    private float CalculateMass(float pDensity, float pDiameter) {
        return pDensity * 4 / 3 * Mathf.PI * Mathf.Pow(pDiameter/2, 3);
	}

    private Vector3 CalculateTrajectorySegmentPosition(Vector3 pInitVelocity, float time) {
        Vector3 trajectorySegment = transform
	}
}
