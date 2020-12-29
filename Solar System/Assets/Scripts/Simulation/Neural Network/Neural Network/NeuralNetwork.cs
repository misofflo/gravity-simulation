using System.Collections.Generic;

public class NeuralNetwork {
	private List<InputNeuron> inputNeurons = new List<InputNeuron>();
	private List<WorkingNeuron> outputNeurons = new List<WorkingNeuron>();
	private List<HiddenLayer> hiddenLayers = new List<HiddenLayer>();

	private float[] weights;

	// create network
	public InputNeuron CreateInputNeuron(float value) {
		InputNeuron inputNeuron = new InputNeuron(value);
		inputNeurons.Add(inputNeuron);
		return inputNeuron;
	}
	public WorkingNeuron CreateOutputNeuron() {
		WorkingNeuron workingNeuron = new WorkingNeuron();
		outputNeurons.Add(workingNeuron);
		return workingNeuron;
	}
	public void CreateHiddenLayer(int numHiddenNeurons) {
		hiddenLayers.Add(new HiddenLayer(numHiddenNeurons));
	}
	public void CreateBias(int layerIndex) {
		if (layerIndex == 0) {
			// creating input neuron as bias
			CreateInputNeuron(1);
		} else if (layerIndex > 0) {
			hiddenLayers[layerIndex - 1].AddBias();
		}
	}

	// mesh network
	public void Mesh(float[] weights) {
		this.weights = weights;

		int index = 0;
		if (hiddenLayers.Count == 0) {
			// meshing input layer with output layer
			foreach (WorkingNeuron wn in outputNeurons) {
				foreach (InputNeuron i in inputNeurons) {
					wn.AddConnction(i, weights[index++]);
				}
			}
		} else {
			// meshing input layer with first hidden layer
			foreach (WorkingNeuron hn in hiddenLayers[0].GetHiddenNeurons()) {
				foreach (InputNeuron i in inputNeurons) {
					hn.AddConnction(i, weights[index++]);
				}
			}

			// meshing hidden layers together
			for (int i = 0; i < hiddenLayers.Count - 1; i++) {
				foreach (WorkingNeuron hn2 in hiddenLayers[i].GetHiddenNeurons()) {
					foreach (WorkingNeuron hn1 in hiddenLayers[i + 1].GetHiddenNeurons()) {
						hn2.AddConnction(hn1, weights[index++]);
					}
					if (hiddenLayers[i].HasBias()) {
						hn2.AddConnction(hiddenLayers[i].GetBias(), weights[index++]);
					}
				}
			}
	
			// meshing output layer with last hidden layer
			foreach (WorkingNeuron on in outputNeurons) {
				foreach (WorkingNeuron hn in hiddenLayers[hiddenLayers.Count - 1].GetHiddenNeurons()) {
					on.AddConnction(hn, weights[index++]);
				}
				if (hiddenLayers[hiddenLayers.Count - 1].HasBias()) {
					on.AddConnction(hiddenLayers[hiddenLayers.Count - 1].GetBias(), weights[index++]);
				}
			}
		}
	}

	// getter
	public List<WorkingNeuron> GetOutputNeurons() {
		return outputNeurons;
	}
	public List<InputNeuron> GetInputNeurons() {
		return inputNeurons;
	}
	public float[] GetWeights() {
		return weights;
	}
}