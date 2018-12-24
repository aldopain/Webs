using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartSimulation () {
		SceneManager.LoadScene ("Simulation");
	}

	public void OpenSettings () {
		SceneManager.LoadScene ("Settings");
	}

	public void OpenMenu () {
		SceneManager.LoadScene ("Menu");
	}
}