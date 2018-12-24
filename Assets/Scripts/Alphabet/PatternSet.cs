using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSet : ScriptableObject {

	public List<Pattern> patterns;
	public string name;

	public PatternSet (string s) {
		name = s;
		patterns = new List<Pattern>();
	}

	public void Add (Pattern p) {
		patterns.Add (p);
	}

	public void RemovePattern (Pattern p) {
		if (patterns.Contains (p))
			patterns.Remove (p);
	}

	public double[] ToVector () {
		var count = patterns.Count * patterns[0].vector.Length;
		var result = new double[count];
		for (int i = 0; i < count; i++)
			result[i] = patterns[i / count].vector[i % count];
		return result;
	}

	public List<double[]> Weight () {
		var result = new List<double[]>();
		foreach (var p in patterns)
			result.Add (p.vector);
		return result;
	}

	public Pattern FindByName (string s) {
		foreach (var p in patterns) {
				if (p.name.Equals(s)) return p;
		}
		return null;
	}
}
