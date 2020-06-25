using UnityEngine;

public class Constants : MonoBehaviour {
    public float G = 0.0000000000667408f;
    [Range(0, 10000000)]
    public float gravMultiplier = 1000;
	public float timescale = 1;

	private void Update() {
		Time.timeScale = timescale;
	}
}
