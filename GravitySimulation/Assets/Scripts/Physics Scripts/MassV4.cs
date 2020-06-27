using UnityEngine;

[RequireComponent(typeof(RigidbodyV1))]
public class MassV4 : MonoBehaviour {
    [SerializeField]
    private float mass, radius;

    [SerializeField]
    private Vector3 initialVelocity, initialTorque;

    [SerializeField]
    private float gravitationMultiplier;

    private RigidbodyV1 rb;
    
    private MassV4[] masses;

    private void Start() {
        rb = GetComponent<RigidbodyV1>();

        rb.SetMass(mass);
        rb.SetRadius(radius);

        rb.AddForce(initialVelocity);
        rb.AddTorque(initialTorque);
    }

    private void FixedUpdate() {
        masses = FindObjectsOfType<MassV4>();
        foreach (MassV4 m in masses) {
            if (m != this)
                Accelerate(m);
        }

        Debug.DrawLine(transform.position + rb.GetVelocity().normalized * radius, transform.position + rb.GetVelocity().normalized * rb.GetVelocity().magnitude, Color.blue);
    }

    public void Accelerate(MassV4 massToAccelerate) {
        Vector3 gravDir = transform.position - massToAccelerate.transform.position;
        float distance = gravDir.magnitude;

        // gravitational force between masses
        float Fg = Universe.G * mass * massToAccelerate.mass / Mathf.Pow(distance, 2) * gravitationMultiplier;

        massToAccelerate.rb.AddForce(gravDir.normalized * Fg);
    }
}
