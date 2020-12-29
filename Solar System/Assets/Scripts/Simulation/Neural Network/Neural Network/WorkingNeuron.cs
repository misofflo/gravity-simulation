using System.Collections.Generic;

public class WorkingNeuron : Neuron {
	private List<Connection> connections = new List<Connection>();
	private ActivationFunction activationFunction = ActivationFunction.tangensHyperbolicus;

	private float value;

	public override float GetValue() {
		float sum = 0;
		foreach (Connection c in connections) {
			sum += c.GetValue();
		}
		value = activationFunction.Activation(sum);

		return value;
	}

	public void AddConnction(Neuron n, float weight) {
		connections.Add(new Connection(n, weight));
	}
}