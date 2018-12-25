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

	public double[] BipolarVector () {
		var res = new double[vector.Length];
		for (int i = 0; i < vector.Length; i++)
			res[i] = vector[i] == 1 ? 1 : -1;
		return res;
	}

}
