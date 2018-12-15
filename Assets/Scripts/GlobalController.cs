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

	void ChooseSet (string s) {
		var ps = FindSet (s);
		if (ps != null) {
			print (s);
			currentSet = ps;
			patternChoser.ClearOptions ();
			var tmp = new List<string>();
			print (ps.patterns.Count);
			for (int i = 0; i < ps.patterns.Count; i++) {
				tmp.Add(ps.patterns[i].name);
			}
			patternChoser.AddOptions (tmp);
		}
	}

	public void ChooseSet () {
		ChooseSet (setChoser.options[setChoser.value].text);
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

	public void RemoveSet () {
		patternSets.Remove (currentSet);
		if (patternSets.Count > 0) {
			setChoser.value = 0;
			ChooseSet (patternSets[0].name);
		} else
			setChoser.ClearOptions();
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
