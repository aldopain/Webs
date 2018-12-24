using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parse : ScriptableObject {

	public static string PatternSetToString (PatternSet ps) {
		string result = "";
		for (int i = 0; i < ps.patterns.Count; i++) {
			if (i != 0)
				result +=  "|";
			result += ps.patterns[i].name + "-" + Parse.VectorToString(ps.patterns[i].vector);
		}
		return result;
	}

	public static PatternSet StringToPatternSet (string name, string vector) {
		var ps = new PatternSet (name);
		string[] patterns = vector.Split('|');
		foreach (var s in patterns) {
			string[] args = s.Split('-');
			ps.patterns.Add (new Pattern (args[0], Parse.StringToVector(args[1])));
		}
		return ps;
	}

	public static string VectorToString (double[] v) {
		string result = "";
		for (int i = 0; i < v.Length; i++)
			result += v[i];
		return result;
	}

	public static double[] StringToVector (string s) {
		var result = new double[s.Length];
		for (int i = 0; i < s.Length; i++)
			double.TryParse (s[i].ToString(), out result[i]);
		return result;
	}
	
}
