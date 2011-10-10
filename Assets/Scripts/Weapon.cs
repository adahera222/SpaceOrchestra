using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public float fire_rate = 0.5f; /* En secondes */
	public Vector3 force;
	public Vector3 offset;
	
	private bool can_fire;
	private float time_elapsed;
	
	private Transform root;
		
	// Use this for initialization
	void Start () {
		can_fire = true;
		time_elapsed = 0.0f;
		root = transform.root;
	}
	
	public bool CanFire() {
		return can_fire;
	}
	
	public void FireOneShot() {
		can_fire = false;
		time_elapsed = 0.0f;
		
		GameObject clone = (GameObject)Instantiate(bullet, transform.position+(0.5f*transform.up), transform.rotation);
		Vector3 vect = new Vector3(0,0,0);
		Vector3 velocity = root.rigidbody.GetRelativePointVelocity(vect);
		
		clone.rigidbody.velocity = velocity;
		clone.rigidbody.AddForce((transform.right * force.x) + 
		                         (transform.up * force.y) +
		                         (transform.forward * force.z));
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
		/* Si on ne peut pas tirer, on compte le temps jusqu'a ce qu'on atteigne fire_rate */
		if(!can_fire) {
			time_elapsed += Time.deltaTime;
			
			if(time_elapsed > fire_rate) {
				can_fire = true;	
			}
		}
	}
}
