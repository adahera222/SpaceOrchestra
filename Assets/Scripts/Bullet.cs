using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float force = 0.0f;
	public float duration = 5.0f; /* Durée de vie de la munition, en secondes */
	public float collision_distance = 0.5f; /* Distance à laquelle on condidère qu'il y a impact */
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
		
		/* Vérification des collisions */
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.up,out hit)) {
			if(Vector3.Distance(hit.point, transform.position) < collision_distance) {
				GameObject target = hit.collider.gameObject;
				Instantiate(explosion, hit.point, transform.rotation);
				target.SendMessage("OnImpact",25);
				Destroy(gameObject);
			}
		}
	}
}
