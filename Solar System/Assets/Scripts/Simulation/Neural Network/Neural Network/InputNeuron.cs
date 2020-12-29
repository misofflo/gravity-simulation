public class InputNeuron : Neuron {
	private float value;
	private ActivationFunction activationFunction = ActivationFunction.tangensHyperbolicus;

	public InputNeuron(float pValue) {
		value = pValue;
	}

	public override float GetValue() {
		return activationFunction.Activation(value);
	}

	public void SetValue(float pValue) {
		value = pValue;
	}
}