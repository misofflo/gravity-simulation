using UnityEngine;

public class Movement : MonoBehaviour {
    private Vector2 input;
	[Range(1, 1000)]
	public float speed = 1;

	private void FixedUpdate() {
		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		transform.Translate(new Vector3(input.x, 0, input.y) * speed);
	}
}
