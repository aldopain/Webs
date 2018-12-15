using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
	public bool isSelected = false;
	Image img;
	public string Name = "KEK";
	
	private void Start() {
		img = GetComponent<Image>();
	}

	void ChooseColor () {
		img.color = isSelected ? Color.black : Color.white;
	}

	public void Select () {
		isSelected = !isSelected;
		ChooseColor ();
	}

	public void Select (bool b) {
		isSelected = b;
		ChooseColor ();
	}
}
