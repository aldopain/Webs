using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour {

	public Signal s;

	public void Spawn () {
		Instantiate (s);
	}
}
