using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour {

	List<PatternSet> patternSets;
	public Dropdown patternChoser;
	public Dropdown setChoser;
	PatternSet currentSet;
	Pattern currentPattern;

	public InputField newSetName;
	public InputField newPatternName;

	// Use this for initialization
	void Start () {
		patternSets = new List<PatternSet>();
		patternChoser.ClearOptions();
		setChoser.ClearOptions();
	}

	public void SavePattern () {
		if (currentPattern != null) {

		}
	}

	void ChooseSet (string s) {
		var ps = FindSet (s);
		if (ps != null) {
			currentSet = ps;
			patternChoser.ClearOptions ();
			var tmp = new List<string>();
			for (int i = 0; i < ps.patterns.Count; i++) {
				tmp.Add(ps.patterns[i].name);
			}
			patternChoser.AddOptions (tmp);
			if (currentSet.patterns.Count > 0)
				currentPattern = currentSet.patterns[0];
			else
				currentPattern = null;
		}
	}

	public void ChooseSet () {
		ChooseSet (setChoser.options[setChoser.value].text);
	}

	void ChoosePattern (string s) {
		var p = FindPattern (s);
		if (p != null)
			currentPattern = p;
		print (currentPattern.name);
	}

	public void ChoosePattern () {
		ChoosePattern (patternChoser.options[patternChoser.value].text);
	}
	
	void AddSet (string s) {
		patternSets.Add (new PatternSet(s));
	}

	void AddPattern (string s) {
		currentSet.Add (new Pattern(s));
	}

	PatternSet FindSet (string s) {
		for (int i = 0; i < patternSets.Count; i++)
			if (patternSets[i].name.Equals(s))
				return patternSets[i];
		return null;
	}

	Pattern FindPattern (string s) {
		if (currentSet != null)
			for (int i = 0; i < currentSet.patterns.Count; i++)
				if (currentSet.patterns[i].name.Equals(s))
					return currentSet.patterns[i];
		return null;
	}

	public void RemovePattern () {
		if (currentSet != null) {
			var n = currentSet.name;
			patternSets.Remove (currentSet);
			if (patternSets.Count > 0) {
				var ind = -1;
				for (int i = 0; i < setChoser.options.Count; i++)
					if (setChoser.options[i].text.Equals (n)) {
						ind = i;
						break;
					}
				if (ind != -1) setChoser.options.RemoveAt (ind);
				setChoser.value = 0;
				ChooseSet (patternSets[0].name);
			} else {
				setChoser.ClearOptions();
				patternChoser.ClearOptions();
			}
		}
	}

	public void RemoveSet () {
		var n = currentSet.name;
		patternSets.Remove (currentSet);
		if (patternSets.Count > 0) {
			var ind = -1;
			for (int i = 0; i < setChoser.options.Count; i++)
				if (setChoser.options[i].text.Equals (n)) {
					ind = i;
					break;
				}
			if (ind != -1) setChoser.options.RemoveAt (ind);
			setChoser.value = 0;
			ChooseSet (patternSets[0].name);
		} else {
			setChoser.ClearOptions();
			patternChoser.ClearOptions();
		}
	}

	public void AddSet () {
		if (newSetName.text != "") {
			AddSet (newSetName.text);
			var tmp = new List<string>();
			tmp.Add (newSetName.text);
			setChoser.AddOptions(tmp);
			newSetName.text = "";
		}
		if (patternSets.Count == 1)
			currentSet = patternSets[0];
	}

	public void AddPattern () {
		if (currentSet != null && newPatternName.text != "") {
			AddPattern (newPatternName.text);
			var tmp = new List<string>();
			tmp.Add (newPatternName.text);
			patternChoser.AddOptions(tmp);
			newPatternName.text = "";
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
