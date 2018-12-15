using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour {
	[System.Serializable]
	public struct Stop{
		public NavigationNode node;
	}

	public List<Stop> Stops;
	public SpriteRenderer StatusSprite;
	public bool ReverseOnCompletion;

	private NavMeshAgent agent;
	private int dest;
	private int direction = 1;
	
	void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(agent.enabled){
			if(agent.remainingDistance < 0.05f){
				if(Stops.Count != 0){
					GotoNextNode();
				}
			}
		}
	}


	void GotoNextNode(){
		int oldDest = dest;

		if(ReverseOnCompletion){
			if(dest == Stops.Count - 1){
				direction = -1;
			}else if(dest == 0){
				direction = 1;
			}
			dest += direction;
		}else{
			dest = (dest + 1) % Stops.Count;
		}

		if(Stops[oldDest].node.Neighbours.Contains(Stops[dest].node)){
			agent.SetDestination(Stops[dest].node.transform.position);
		}else{
			agent.isStopped = true;
			StatusSprite.gameObject.SetActive(true);
		}
	}

	public void Goto(Vector3 point){
		agent.SetDestination(point);	
	}

	public void SetSpeed(float s){
		agent.speed = s;
	}

	public void WaitInTown(){
		Destroy(gameObject);
	}
}
