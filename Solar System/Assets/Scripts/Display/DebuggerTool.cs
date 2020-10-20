using UnityEngine;

[ExecuteInEditMode]
public class DebuggerTool : MonoBehaviour {
    [Range(0, 10000)]
    public int iterations = 0;

	private void Start() {
        if (Application.isPlaying) {
            HideOrbits();
		}
	}

	private void Update() {
		if (!Application.isPlaying) {
            DrawOrbits();
            Debug.Log("update");
		}
	}

	private void DrawOrbits() {
        CelestialObject[] allbodies = FindObjectsOfType<CelestialObject>();
        
        foreach (CelestialObject body in allbodies) {
            Vector3[] positions = new Vector3[iterations];
            var lineRenderer = body.gameObject.GetComponentInChildren<LineRenderer>();
            for (int i = 0; i < iterations; i++) {
                body.UpdateVelocity(allbodies, Universe.physicsTimeStep);
                positions[i] = body.getCurrentVelocity() * i;
			}

            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
		}
	}

    private void HideOrbits() {
        CelestialObject[] allbodies = FindObjectsOfType<CelestialObject>();

        for (int i = 0; i < allbodies.Length; i++) {
            var lineRenderer = allbodies[i].gameObject.GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 0;
		}
    }
}
