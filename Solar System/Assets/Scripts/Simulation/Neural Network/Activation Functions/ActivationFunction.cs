public abstract class ActivationFunction {
	public static ActivationSigmoid sigmoid = new ActivationSigmoid();
	public static ActivationIdentity identity = new ActivationIdentity();
	public static ActivationTangensHyperbolicus tangensHyperbolicus = new ActivationTangensHyperbolicus();
	public static ActivationInput input = new ActivationInput();

	public abstract float Activation(float input);
}