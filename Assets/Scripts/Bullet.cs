using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float force = 0.0f;
	public float duration = 5.0f; /* Durée de vie de la munition, en secondes */
	public GameObject explosion;
	
	private float age = 0.0f;
	
	void Start () {
		rigidbody.AddForce(transform.up * force);
		age = 0.0f;
	}
	
	void Update () {
		
		/* On fait vieillir la munition */
		age += Time.deltaTime;
		
		/* On la détruit si elle est trop vieille */
		if(age >= duration)
			Destroy (gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
    	// Rotate the object so that the y-axis faces along the normal of the surface
	    ContactPoint contact = collision.contacts[0];
	    Vector3 pos = contact.point;
	    Instantiate(explosion, pos, transform.rotation);
		GameObject target = collision.collider.gameObject;
		target.SendMessage("OnImpact",25);
		Destroy(gameObject);
	}
}
