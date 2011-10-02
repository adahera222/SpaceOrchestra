using UnityEngine;
using System.Collections;

public class CorvetteBody : MonoBehaviour {
	
	public GameObject controller;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnImpact(int damage) {
		controller.SendMessage("OnImpact",damage);
	}
}
