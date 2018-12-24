using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simulation : MonoBehaviour {

	public PatternController view;
	public Dropdown algoChoser;
	public Dropdown setChoser;

	const int HABB_CHOSEN = 0;
	const int HAMMING_CHOSEN = 1;
	const string HABB_OPTION = "Habb";
	const string HAMMING_OPTION = "Hamming";

	List<PatternSet> patternSets;

	private void Start() {
		algoChoser.ClearOptions();
		var buf = new List<string>();
		buf.Add (HABB_OPTION); buf.Add (HAMMING_OPTION);
		algoChoser.AddOptions (buf);

		patternSets = new List<PatternSet>();
		patternSets.AddRange (Saver.ReadPatternSets());

		setChoser.ClearOptions();
		buf = new List<string>();
		foreach (var ps in patternSets)
			buf.Add (ps.name);
		setChoser.AddOptions (buf);
	}

	public void Process () {
		double[] p = view.ToVector ();
		var chosenSet = patternSets[setChoser.value];
		switch (algoChoser.value) {
			case HABB_CHOSEN:
				print (new Habb().Process(chosenSet, p));
				break;
			case HAMMING_CHOSEN:
				// new Hamming().Process(chosenSet, p);
				break;
		}
	}

	void Habb (PatternSet ps, double[] vector) {
	}

	void Hamming (PatternSet ps, double[] vector) {

	}
}
