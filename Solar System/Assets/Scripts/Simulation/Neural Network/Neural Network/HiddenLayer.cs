using System.Collections.Generic;

public class HiddenLayer {
	private List<WorkingNeuron> hiddenNeurons = new List<WorkingNeuron>();
	private Bias bias = null;

	public HiddenLayer(int numHiddenNeurons) {
		for (int i = 0; i < numHiddenNeurons; i++) {
			hiddenNeurons.Add(new WorkingNeuron());
		}
	}

	public void AddBias() {
		bias = new Bias();
	}

	public bool HasBias() {
		if (bias != null) {
			return true;
		}
		return false;
	}

	// getter
	public List<WorkingNeuron> GetHiddenNeurons() {
		return hiddenNeurons;
	}
	public Bias GetBias() {
		return bias;
	}
}