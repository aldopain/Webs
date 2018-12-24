using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : ScriptableObject {
	public string name;
	public double[] vector;

	public Pattern (string s) {
		name = s;
	}

	public Pattern (string s, double[] v) {
		name = s;
		vector = v;
	}

}
