using UnityEngine;

public class Sensor : MonoBehaviour {
	private Transform creature;
	private Vector3 currentRotation;
	private float length = 1f;
	private CreatureAgent other;

	// contructor
	public Sensor(Transform _creature, Vector3 _rotation, float _length) {
		creature = _creature;
		currentRotation = _rotation;
		length = _length;
	}

	// scanner
	private void FixedUpdate() {
		RaycastHit hitInfo;
		if (Physics.Raycast(creature.position + currentRotation * creature.localScale.x / 2, currentRotation, out hitInfo, length)) {
			// as soon as planet has relief then add if statement
			other = hitInfo.collider.gameObject.GetComponent<CreatureAgent>();
		} else {
			other = null;
		}
	}

	// getter
	public CreatureAgent Get() {
		return other;
	}

	public void Turn(Vector3 _amount) {
		currentRotation += _amount;
	}
}
