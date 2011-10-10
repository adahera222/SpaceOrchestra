using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	
	public Transform target;
	public float max_thrust = 10.0f;
	public float torque = 10.0f;
	public float max_angle = 45;
	
	public float collision_distance = 0.5f; /* Distance à laquelle on condidère qu'il y a impact */
	public GameObject explosion;
	
	private float previous_angle;
	private float integrale;
	
	/* Constantes du PID */
	public float Ap = 1.0f;
	public float Ai = 1.0f;
	public float Ad = 1.0f;
	
	public float epsilon = 0.01f;
	
	// Use this for initialization
	void Start () {
		
		previous_angle = 0.0f;
		integrale = 0.0f;
		Controler ctrl=(Controler) GameObject.Find("Controler").GetComponent("Controler");
		target = ctrl.target.transform;
	
	}

	// Update is called once per frame
	void Update () {
		
		/* Tentative d'asservissement PID */
		/*
		float dt = Time.deltaTime;

		Vector3 targetDelta = target.position - transform.position;
		float delta_angle = Vector3.Angle(transform.forward, targetDelta);
		
		float derivee = (delta_angle - previous_angle)/dt;
		previous_angle = delta_angle;
		
		if(Mathf.Abs(delta_angle) > epsilon) {
			integrale = integrale + delta_angle*dt;
		}
		float commande = Ap*delta_angle + Ai*integrale + Ad*derivee;
		*/
		/* Saturation */
		/*
		if(delta_angle > max_angle) {
			delta_angle = max_angle;	
		}
		
		Vector3 cross = Vector3.Cross(transform.forward, targetDelta);
		
		
		rigidbody.AddForce(transform.forward * max_thrust);
		rigidbody.AddTorque(cross * commande);
		*/


		transform.LookAt(target);

		rigidbody.AddForce(transform.forward * max_thrust);
		
		/* Vérification des collisions */
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.up,out hit)) {
			if(Vector3.Distance(hit.point, transform.position) < collision_distance) {
				GameObject impact = hit.collider.gameObject;
				Instantiate(explosion, hit.point, transform.rotation);
				impact.SendMessage("OnImpact",80);
				Destroy(gameObject);
			}
		}
	}
}
