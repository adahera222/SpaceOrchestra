using UnityEngine;
using System.Collections;

public class DummyIA : MonoBehaviour {

	public float speed;
	public float torque;
	
	public Ship ship;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		ship.setDesiredSpeed(speed);
		ship.setPitch(torque);
	}
}
