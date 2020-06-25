using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipController : MonoBehaviour {
	public Vector2 mainInput;
	public Vector2 thrustersInput;
	public Vector2 mainThrustersInput;
	public float yaw;

	private Rigidbody rb;

	[Range(0, 10000)]
	public float mainThrustMultiplier;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate() {
		mainInput = new Vector2(Input.GetAxis("Roll"), Input.GetAxis("Pitch"));
		mainThrustersInput = new Vector2(Input.GetAxis("ThrustL") + 1, Input.GetAxis("ThrustR") + 1);
		thrustersInput = new Vector2(Input.GetAxis("ThrustersX"), Input.GetAxis("ThrustersY"));
		yaw = Input.GetAxis("Yaw");

		rb.AddTorque(new Vector3(mainInput.y, 0, mainInput.x).normalized * mainInput.magnitude * 3);
		rb.AddForce(transform.up * (mainThrustersInput.x + mainThrustersInput.y) * mainThrustMultiplier);
		rb.AddForce(new Vector3(thrustersInput.x, 0, thrustersInput.y) * thrustersInput.magnitude * 3);
		rb.AddTorque(new Vector3(0, yaw, 0) * 3);
	}
}
