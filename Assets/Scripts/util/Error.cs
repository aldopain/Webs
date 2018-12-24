using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Forms;
using System;

public class Error : ScriptableObject {
	public static void WrongStringMessage () {
		MessageBox.Show ("Only A-Za-z0-9 are allowed!", "Wrong name format");
	}

	public static void EmptySetMessage () {
		MessageBox.Show ("Please, add at least one pattern to set or remove set at all", "Empty set");
	}

	public static void UnknowErrorMessage (Exception e) {
		MessageBox.Show (e.ToString (), "Unknown error");
	}

	public static void NoSetMessage () {
		MessageBox.Show ("Please, create at least one set", "No set chosen");
	}
}
