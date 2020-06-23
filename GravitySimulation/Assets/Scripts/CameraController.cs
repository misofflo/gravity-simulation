﻿using UnityEngine;

public class CameraController : MonoBehaviour {
	// VARIABLES
	public float Sensitivity = 3;
	public float dstFromTarget = 10;
	public Transform target;
	public Vector2 pitchMinMax = new Vector2(-20, 85);

	public float rotationSmoothTime = 0.12f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float yaw, pitch;

	// FUNCTIONS
	void LateUpdate() {
		yaw += Input.GetAxis("Mouse X") * Sensitivity;
		pitch -= Input.GetAxis("Mouse Y") * Sensitivity;
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, target.localRotation.eulerAngles + new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

		//currentRotation = Vector3.SmoothDamp(currentRotation, target.rotation.eulerAngles + new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		transform.localEulerAngles = currentRotation;

		transform.position = target.position - transform.forward * dstFromTarget;
	}
}