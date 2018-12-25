using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {

	NavigationNode startPosition;
	NavigationNode endPoint;
	UnitMovement um;

	// Use this for initialization
	void Start () {
		transform.position = startPosition.transform.position;
		um = GetComponent<UnitMovement>();
		um.Goto (endPoint.transform.position);
	}

	private void OnTriggerEnter(Collider other)  {
        if (other.GetComponent<NavigationNode>().Equals(endPoint))
			Destroy(gameObject);
    }
}
