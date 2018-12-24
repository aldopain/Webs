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

	public InputField newSetName;
	public InputField newPatternName;
	public PatternController view;

	// Use this for initialization
	void Start () {
		StartCoroutine(Init ());
	}

	IEnumerator Init () {
		yield return new WaitForSeconds(0.01f);
		patternSets = new List<PatternSet>();
		patternSets.AddRange (Saver.ReadPatternSets());
		patternChoser.ClearOptions();
		setChoser.ClearOptions();
		var patBuf = new List <string>();
		var setBuf = new List <string>();
		foreach (var ps in patternSets)
			AddSetToChoser (ps.name);
		ChooseSet ();
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

	void CleanPattern () {
		currentPattern = null;
		view.Clean ();
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
			if (currentSet.patterns.Count > 0) {
				ChoosePattern (currentSet.patterns[0]);
			}
			else CleanPattern();
		}
	}

	void ChooseSet (string s) {
		var ps = FindSet (s);
		ChooseSet (ps);
	}

	public void ChooseSet () {
		ChooseSet (setChoser.options[setChoser.value].text);
	}

	void ChoosePattern (Pattern p) {
		if (p != null) {
			currentPattern = p;
			view.FromVector (p.vector);
		} else print ("error");
	}

	void ChoosePattern (string s) {
		ChoosePattern (FindPattern (s));
	}

	public void ChoosePattern () {
		ChoosePattern (patternChoser.options[patternChoser.value].text);
	}
	
	void AddSet (string s) {
		if (Parse.CheckString (s))
			patternSets.Add (new PatternSet(s));
		else Error.WrongStringMessage ();
	}

	void AddPattern (string s) {
		if (Parse.CheckString (s))
			currentSet.Add (new Pattern(s));
		else Error.WrongStringMessage ();
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
		else print ("set is null");
		print ("cannot find this pattern in the set");
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
		if (Parse.CheckString (s)) {
			var tmp = new List<string>();
			tmp.Add (s);
			setChoser.AddOptions(tmp);
		} else Error.WrongStringMessage ();
	}

	public void AddSet () {
		if (Parse.CheckString (newSetName.text)) {
			AddSet (newSetName.text);
			var tmp = new List<string>();
			tmp.Add (newSetName.text);
			setChoser.AddOptions(tmp);
			newSetName.text = "";
			if (patternSets.Count == 1)
				currentSet = patternSets[0];
		} else Error.WrongStringMessage ();
	}

	public void AddPattern () {
		if (Parse.CheckString (newSetName.text)) {
			if (currentSet != null) {
				AddPattern (newPatternName.text);
				var tmp = new List<string>();
				tmp.Add (newPatternName.text);
				patternChoser.AddOptions(tmp);
				newPatternName.text = "";
			} else Error.NoSetMessage ();
		} else Error.WrongStringMessage ();
	}
}
