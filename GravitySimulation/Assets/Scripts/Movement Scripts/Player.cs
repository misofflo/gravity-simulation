using UnityEngine;

public class Player : MonoBehaviour {
    // Fields
    private Camera playerCamera;
    private Transform[] physicsObjects;
    private Rigidbody rb;
    private MassV3 gravity;

    public float distanceThreshold = 1000;
    [Range(1, 10000)]
    public float forceMultiplier;

    // Functions
    // Start is called before the first frame update
    private void Start() {
        gravity = GetComponent<MassV3>();
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        physicsObjects = FindObjectsOfType<Transform>();
    }

	private void Update() {
        UpdateFloatingOrigin();
    }

	private void FixedUpdate() {
        MovePlayer();
	}

	private void UpdateFloatingOrigin() {
        Vector3 originOffset = playerCamera.transform.position;
        float dstFromOrigin = originOffset.magnitude;

        if (dstFromOrigin > distanceThreshold) {
            foreach (Transform t in physicsObjects) {
                t.position -= originOffset;
			}
		}
	}

    private void MovePlayer() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 inputDir = input.normalized;

        transform.Translate(new Vector3(inputDir.x, 0, inputDir.y) * input.magnitude);

        if (Input.GetKeyDown(KeyCode.R)) {
            transform.Translate(transform.up * 1);
		}
	}
}
