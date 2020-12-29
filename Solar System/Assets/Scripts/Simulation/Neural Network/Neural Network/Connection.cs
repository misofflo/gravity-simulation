public class Connection {
	private float weight;
	private Neuron neuron;

	public Connection(Neuron pNeuron, float pWeight) {
		neuron = pNeuron;
		weight = pWeight;
	}

	public float GetValue() {
		return neuron.GetValue() * weight;
	}

	public float GetWeight() {
		return weight;
	}

	public Neuron GetNeuron() {
		return neuron;
	}
}