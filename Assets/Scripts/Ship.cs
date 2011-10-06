using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
	
	/* Private */
	private float desired_speed = 0.0f;
	private float current_speed = 0.0f;
	private float current_hull;
	private float current_energy;
	
	private float pitch;
	private float roll;
	private float yaw;
	
	
	/* Public */
	public float max_speed = 27.0f;
	public float min_speed = -1.0f;
	public float max_energy = 100.0f;
	public float max_hull = 100.0f;
	public float eps = 10.0f; /* Energy per seconds */
	
	public GameObject explosion;
	public AudioSource engine;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void setDesiredSpeed(float speed) {
		desired_speed = speed;
		if(desired_speed > max_speed)
			desired_speed = max_speed;
		if(desired_speed < min_speed)
			desired_speed = min_speed;
	}
	
	public void setPitch(float pitch) {
		this.pitch = pitch;
	}
	
	public void setRoll(float roll) {
		this.roll = roll;
	}
	
	public void setYaw(float yaw) {
		this.yaw = yaw;
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		/* Récupération de l'énergie */
		current_energy += eps * Time.deltaTime;
		if(current_energy > max_energy)
			current_hull = max_energy;
		
		/* Cette partie devrait se trouver dans le script ship controler */
		float acceleration = Input.GetAxis("SpeedControl");
	
		current_speed = rigidbody.velocity.magnitude;
		desired_speed += acceleration*0.1f;
		
		setDesiredSpeed(desired_speed);
		setPitch(Input.GetAxis("Pitch"));
		setRoll(Input.GetAxis("Roll"));
		setYaw(Input.GetAxis("Yaw"));

		rigidbody.AddForce(desired_speed * transform.forward);
		rigidbody.AddTorque(pitch * transform.right);
		rigidbody.AddTorque(roll * transform.forward);		                    
		rigidbody.AddTorque(yaw * transform.up);
		
		/* Reglage du pitch du son en fonction de la consigne vitesse */
		engine.pitch = 1+ current_speed*(2/max_speed);
		
		/* Surement pas le bon endroit: */
		if (Input.GetButton("Quit")) {
			print("EXIT!");
			Application.Quit();
		}
	}
	
	void OnGUI () {
		GUI.Box (new Rect (20,20,200,80), "");
		GUI.Label (new Rect (25, 25, 200, 30), "Consigne vitesse:" + ((int)(desired_speed*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (25, 40, 200, 30), "Vitesse courante:" + ((int)(current_speed*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (25, 55, 200, 30), "Hull:" + ((int)(current_hull)).ToString());
		GUI.Label (new Rect (25, 70, 200, 30), "Energy:" + ((int)(current_hull)).ToString());
	}
	
		
	void OnImpact(int damage) {
		current_energy -= (float)damage;
		if(current_energy <= 0.0f) {
			current_hull += current_energy;
			current_energy = 0.0f;
		}
		if(current_hull <= 0.0f) {	
	    	Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}