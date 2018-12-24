using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour {

	public List<PatternSet> patternSets;
	PatternSet currentSet;
	public Pattern currentPattern;
	DatabaseManager databaseManager;

	public Dropdown patternChoser;
	public Dropdown setChoser;
	public PatternController view;
	public InputField newSetName;
	public InputField newPatternName;

	// Use this for initialization
	void Start () {
		patternSets = new List<PatternSet>();
		patternSets.AddRange (Saver.ReadPatternSets());
		patternChoser.ClearOptions();
		setChoser.ClearOptions();
		var patBuf = new List <string>();
		var setBuf = new List <string>();
		foreach (var ps in patternSets)
			AddSetToChoser (ps.name);
		ChooseSet (patternSets[0]);
		databaseManager = new DatabaseManager ("test");
	}

	public void SavePattern () {
		if (currentPattern != null) {
			currentSet.FindByName (currentPattern.name).vector = view.ToVector ();
		}
	}

	public void SavePatternSet () {
		if (currentSet != null) {
			Saver.InsertPatternSet (currentSet);
		}
	}

	void ChooseSet (PatternSet ps) {
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

	void ChooseSet (string s) {
		var ps = FindSet (s);
		ChooseSet (ps);
	}

	public void ChooseSet () {
		ChooseSet (setChoser.options[setChoser.value].text);
	}

	void ChoosePattern (string s) {
		var p = FindPattern (s);
		if (p != null)
			currentPattern = p;
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

	void AddSetToChoser (string s) {
		var tmp = new List<string>();
		tmp.Add (s);
		setChoser.AddOptions(tmp);
	}

	public void AddSet () {
		if (newSetName.text != "") {
			AddSet (newSetName.text);
			AddSetToChoser (newSetName.text);
			newSetName.text = "";
		}
		if (patternSets.Count == 1)
			currentSet = patternSets[0];
	}

	// void AddSet (string s) {
	// 	if (s != null) {
			
	// 	}
	// }

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
