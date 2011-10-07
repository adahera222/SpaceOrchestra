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
	
	private Material shield_material;
	private float shield_timer;
	private bool shield_activated;
	
	
	/* Public */
	public float max_speed = 27.0f;
	public float min_speed = -1.0f;
	public float max_energy = 100.0f;
	public float max_hull = 100.0f;
	public float eps = 10.0f; /* Energy per seconds */
	
	public GameObject explosion;
	public AudioSource engine;
	
	public int nb_in_team;
	public bool friend;
	
	// Use this for initialization
	void Start () {
		Transform shield = transform.Find("Shield");
		if(shield != null) {
			MeshRenderer shield_renderer = shield.gameObject.GetComponent<MeshRenderer>();
			shield_material = shield_renderer.material;
		} else {
			shield_material = null;
		}
		shield_activated = false;
		shield_timer = 0;

		if(shield_material != null) {
			Color col = Color.white;
			col.a = 0;
			shield_material.SetColor("_TintColor", col);
		}
		
		current_hull = max_hull;
		current_energy = max_energy;
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
		if(current_energy > max_energy) {
			current_energy = max_energy;
		}
		
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
		
		if(shield_activated) {
			Color col = Color.white;			
			shield_timer -= Time.deltaTime;
			
			if(shield_timer > 0) {
				if(shield_timer > 0.7f) {
					col.a = 10 * (1-shield_timer);
				} else {
					col.a = shield_timer / 0.7f;
				}
			} else {
				shield_timer = 0;
				shield_activated = false;
				col.a = 0;
			}
			shield_material.SetColor("_TintColor", col);
		}
	}
	
	void OnGUI () {
		int x_offset = friend ? 0:300;
		int y_offset = nb_in_team * 100;
		GUI.Box (new Rect (  x_offset + 20, y_offset + 20,200,80), "");
		GUI.Label (new Rect (x_offset + 25, y_offset + 25, 200, 30), "Consigne vitesse:" + ((int)(desired_speed*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (x_offset + 25, y_offset + 40, 200, 30), "Vitesse courante:" + ((int)(current_speed*3.6f)).ToString() + "km/h" );
		GUI.Label (new Rect (x_offset + 25, y_offset + 55, 200, 30), "Hull:" + ((int)(current_hull)).ToString()+"/"+((int)(max_hull)).ToString());
		GUI.Label (new Rect (x_offset + 25, y_offset + 70, 200, 30), "Energy:" + ((int)(current_energy)).ToString()+"/"+((int)(max_energy)).ToString());
	}
	
		
	void OnImpact(int damage) {
		current_energy -= (float)damage;
		
		if(current_energy <= 0.0f) {
			current_hull += current_energy;
			current_energy = 0.0f;
		} else {
			shield_activated = true;
			shield_timer = 1;
		}
		if(current_hull <= 0.0f) {	
	    	Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}