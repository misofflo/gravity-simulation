using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CreatureAgent : MonoBehaviour {
	// Parameters
	private float energy = 100;
	[SerializeField]
	private float geneticValue;

	private NeuralNetwork neuralNetwork;
	private Sensor sensor;

	private Rigidbody rb;

	[SerializeField]
	private Transform targetPlanet;

	// Functions
	private void Awake() {
		sensor = new Sensor(transform, new Vector3(0, 0, 0), 1f);

		NeuralNetworkInit();
		UpdateInputs();

		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.freezeRotation = true;
	}

	private void FixedUpdate() {
		UpdateInputs();

		Move();
		TurnSensor();
		if (neuralNetwork.GetOutputNeurons()[3].GetValue() > 0.5f || neuralNetwork.GetOutputNeurons()[3].GetValue() < -0.5f) {
			Attack();
		} 
	}

	// Actions
	// energyCost = 
	private void Move() {
		// TODO
		// 2-dimensional x und y --> Input.GetAxis(...)
		Vector3 values = new Vector3(neuralNetwork.GetOutputNeurons()[0].GetValue() * Universe.physicsTimeStep, neuralNetwork.GetOutputNeurons()[1].GetValue() * Universe.physicsTimeStep, 0f);

		transform.LookAt(targetPlanet);
		transform.Translate(values);
	}

	// energyCost = 
	private void TurnSensor() {
		float value = neuralNetwork.GetOutputNeurons()[2].GetValue();

		sensor.Turn(new Vector3(0, 0, value));
	}

	// energyCost = 
	private void Attack() {
		float fixedDPS = 0.2f;
		if (sensor.Get() != null) {
			sensor.Get().energy -= fixedDPS;
			energy += fixedDPS;
		}
	}

	// energyCost = 
	private void Divide() {
		// TODO
		// create new creature from itself with half of the energy
	}

	private void NeuralNetworkInit() {
		neuralNetwork = new NeuralNetwork();

		// create inputs
		neuralNetwork.CreateInputNeuron(0);
		neuralNetwork.CreateInputNeuron(0);
		neuralNetwork.CreateInputNeuron(0);

		// create hiddenlayer
		neuralNetwork.CreateHiddenLayer(50);

		// create outputs
		neuralNetwork.CreateOutputNeuron();
		neuralNetwork.CreateOutputNeuron();
		neuralNetwork.CreateOutputNeuron();
		neuralNetwork.CreateOutputNeuron();

		float[] weights = new float[3*50+4*50];
		for (int i = 0; i < weights.Length; i++) {
			weights[i] = Random.Range(-1f, 1f);
		}

		neuralNetwork.Mesh(weights);
	}

	private void UpdateInputs() {
		neuralNetwork.GetInputNeurons()[0].SetValue(transform.position.x);
		neuralNetwork.GetInputNeurons()[1].SetValue(transform.position.y);

		float value = (sensor.Get() != null) ? sensor.Get().geneticValue : 0;
		neuralNetwork.GetInputNeurons()[2].SetValue(value);
	}

	private void CalculateGeneticValue() {
		float x = 0;
		float[] I_i = new float[3];
		for (int i = 0; i < I_i.Length; i++) {
			I_i[i] = 1;
			for (int j = 0; j < 50; j++) {
				I_i[i] *= neuralNetwork.GetWeights()[j];
			}
			x += I_i[i];
		}

		float y = 0;
		float[] H_i = new float[50];
		for (int i = 0; i < H_i.Length; i++) {
			H_i[i] = 1;
			for (int j = 0; j < 50; j++) {
				H_i[i] *= neuralNetwork.GetWeights()[j];
			}
			y += H_i[i];
		}

		geneticValue = x + y;
	}
}