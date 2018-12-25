using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vizualizator : MonoBehaviour {

	List<GameObject> currentPack;
	public GameObject neuron;


	// Use this for initialization
	void Start () {
		InitHamming();
	}

	void InitHamming() {
		InitNeuronPack(new Vector3(0,0,0), "S");
		InitNeuronPack(new Vector3(0,0,6), "Z");
		InitNeuronPack(new Vector3(0,0,12), "A");
		InitNeuronPack(new Vector3(0,0,18), "Y");
	}

	void InitHabb() {
		InitNeuronPack(new Vector3(0,0,0), "S");
		InitNeuronPack(new Vector3(0,0,6), "A");
		InitNeuronPack(new Vector3(0,0,12), "Y");
	}

	void InitS() {
		
	}

	void InitA() {

	}

	void InitY() {

	}

	void InitNeuronPack (Vector3 offset, string newTag) {
		for (int i = 0; i < 6; i++) {
			var buf = Instantiate (neuron);
			buf.transform.position = new Vector3 (i * 5, 0, 0) + offset;
			buf.tag = newTag;
		}
	}

	// void SToZ () {
	// 	for (int i = 0; i < 6; i++) {
	// 		Instantiate()
	// 	}
	// }
	
	// Update is called once per frame
	void Update () {
		
	}
}
