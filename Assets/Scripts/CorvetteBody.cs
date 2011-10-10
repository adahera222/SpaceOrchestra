using UnityEngine;
using System.Collections;

public class CorvetteBody : MonoBehaviour {
	
	public GameObject ship;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnImpact(int damage) {
		ship.SendMessage("OnImpact",damage);
	}
}
