using UnityEngine;
using System.Collections;

public class Controler : MonoBehaviour {
	
	public float consigne_vitesse = 0.0f;
	public float vitesse_courante = 0.0f;
	public float vitesse_max = 27.0f;
	public float vitesse_min = -1.0f;
	
	public GameObject explosion;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		
		/* Gestion de l'accélération */
		float acceleration = Input.GetAxis("SpeedControl");
	
		vitesse_courante = rigidbody.velocity.magnitude;
		consigne_vitesse += acceleration*0.1f;
		
		if(consigne_vitesse > vitesse_max)
			consigne_vitesse = vitesse_max;
		if(consigne_vitesse < vitesse_min)
			consigne_vitesse = vitesse_min;
		
		rigidbody.AddForce(consigne_vitesse * transform.forward);
		
		/* Gestion du Pitch */
		float pitch = Input.GetAxis("Pitch");
		rigidbody.AddTorque(pitch * transform.right);
		
		/* Gestion du Roll */
		float roll = Input.GetAxis("Roll");
		rigidbody.AddTorque(roll * transform.forward);
		
		/* Gestion du Yaw */
		float yaw = Input.GetAxis("Yaw");
		rigidbody.AddTorque(yaw * transform.up);
	}
	
	void OnGUI () {
		GUI.Box (new Rect (20,20,200,60), "");
		GUI.Label (new Rect (25, 25, 200, 30), "Consigne vitesse:" + ((int)(consigne_vitesse*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (25, 40, 200, 30), "Vitesse courante:" + ((int)(vitesse_courante*3.6f)).ToString() + "km/h" );
	}
	
		
	void OnImpact(int damage) {
	    Instantiate(explosion, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
