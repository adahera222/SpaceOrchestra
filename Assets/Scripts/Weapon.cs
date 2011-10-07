using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public GameObject bullet;
	public float fire_rate = 0.5f; /* En secondes */
	public float force = 0.0f;
	
	private bool can_fire;
	private float time_elapsed;
		
	// Use this for initialization
	void Start () {
		can_fire = true;
		time_elapsed = 0.0f;
	}
	
	public bool CanFire() {
		return can_fire;
	}
	
	public void FireOneShot() {
		can_fire = false;
		time_elapsed = 0.0f;
		
		GameObject clone = (GameObject)Instantiate(bullet, transform.position, transform.rotation);
		Vector3 vect = new Vector3(0,0,0);
		Vector3 velocity = rigidbody.GetRelativePointVelocity(vect);
		
		clone.rigidbody.velocity = velocity;
		clone.rigidbody.AddForce(transform.up * force);
		
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
