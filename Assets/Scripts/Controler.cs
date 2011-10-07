using UnityEngine;
using System.Collections;

public class Controler : MonoBehaviour {
	
	public float desired_speed = 0.0f;
	public float current_speed = 0.0f;

	public Ship ship;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		Screen.lockCursor = true; 
		
		ship.transform.Find("Camera").gameObject.SetActiveRecursively(true);
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {	
		/* Gestion de l'accélération */
		float acceleration = Input.GetAxis("SpeedControl");
	
		current_speed = ship.rigidbody.velocity.magnitude;
		desired_speed += acceleration*0.1f;
		
		if(desired_speed > ship.max_speed)
			desired_speed = ship.max_speed;
		if(desired_speed < ship.min_speed)
			desired_speed = ship.min_speed;
		
		/* Gestion du Pitch */
		ship.setDesiredSpeed(desired_speed);
		ship.setPitch(Input.GetAxis("Pitch"));
		ship.setRoll(Input.GetAxis("Roll"));
		ship.setYaw(Input.GetAxis("Yaw"));
		
		// Gestion des tirs
		if (Input.GetButton("Fire 1")) {
			ship.Fire(0);
			ship.Fire(1);
	    }
		
		/* Surement pas le bon endroit: */
		if (Input.GetButton("Quit")) {
			Application.Quit();
		}
		
	}
	
	void OnGUI () {
		GUI.Box (new Rect (20,20,200,70), "");
		GUI.Label (new Rect (25, 25, 200, 30), "Consigne vitesse:" + ((int)(desired_speed*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (25, 40, 200, 30), "Vitesse courante:" + ((int)(ship.rigidbody.velocity.magnitude*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (25, 55, 200, 30), "Hull:" + ((int)(ship.current_hull)).ToString()+"/"+((int)(ship.max_hull)).ToString());
		GUI.Label (new Rect (25, 70, 200, 30), "Energy:" + ((int)(ship.current_energy)).ToString()+"/"+((int)(ship.max_energy)).ToString());
	}
}
