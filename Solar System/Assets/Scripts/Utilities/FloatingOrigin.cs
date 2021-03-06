﻿using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class FloatingOrigin : MonoBehaviour {
    public float threshold = 1000.0f;

	private void LateUpdate() {
		// getting camera offset from world origin
		Vector3 cameraOffset = gameObject.transform.position;

		// if offset surpasses predefined threshold, move origin to camera position
		if (cameraOffset.magnitude > threshold) {
			for (int i = 0; i < SceneManager.sceneCount; i++) {
				foreach (GameObject g in SceneManager.GetSceneAt(i).GetRootGameObjects()) {
					g.transform.position -= cameraOffset;
				}
			}
		}
	}
}
