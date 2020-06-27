using UnityEngine;

public class Universe : MonoBehaviour {
    public static float G = 0000000.0000667408f;
	public static float c = 299792458;

    [Range(0, 10000000)]
    public float gravMultiplier = 1000;
	public float timescale = 1;

	private void FixedUpdate() {
		Time.timeScale = timescale;
	}
}
