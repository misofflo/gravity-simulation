using System;

public class ActivationInput : ActivationFunction {
	public override float Activation(float input) {
		return 0.5f * (1 - 2 / (float)(Math.Pow(Math.E, 8 * input - 4) + 1));
	}
}
