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

	public Pattern FindByName (string s) {
		foreach (var p in patterns) {
				if (p.name.Equals(s)) return p;
		}
		return null;
	}
	
	public void ChangeSelectedPattern (string name) {

	}
}
