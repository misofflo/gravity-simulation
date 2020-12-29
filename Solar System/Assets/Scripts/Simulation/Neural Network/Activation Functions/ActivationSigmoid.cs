using System;

public class ActivationSigmoid : ActivationFunction {
	public override float Activation(float input) {
		return 1 / (1 + (float)Math.Pow(Math.E, -input));
	}
}