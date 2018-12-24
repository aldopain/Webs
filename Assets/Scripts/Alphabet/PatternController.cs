using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternController : MonoBehaviour {
	public GameObject cell;
	public int cellCount;
	public List<Cell> cells;

	private void Start() {
		var glp = gameObject.GetComponent<GridLayoutGroup>();
		var collumnCount = (int) Mathf.Sqrt (cellCount);
		glp.constraintCount = collumnCount;
		cellCount = (int) Mathf.Pow (collumnCount, 2);
		for (int i = 0; i < cellCount; i++)
			cells.Add (Instantiate (cell, transform).GetComponent<Cell>());
	}

	public double[] ToVector () {
		double[] v = new double[cellCount];
		for (int i = 0; i < cellCount; i++)
			v[i] = cells[i].isSelected ? 1 : 0;
		return v;
	}

	public void FromVector (double[] v) {
		if (v == null) Clean();
		else
			for (int i = 0; i < cellCount; i++) {
				cells[i].Select (v[i] == 1);
			}
	}

	public void Clean () {
		for (int i = 0; i < cellCount; i++)
			cells[i].Select (false);
	}
}