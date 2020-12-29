using System;

public class ActivationTangensHyperbolicus : ActivationFunction {
	public override float Activation(float input) {
		return 1 - 2 / (float)(Math.Pow(Math.E, 8 * input - 4) + 1);
	}
}