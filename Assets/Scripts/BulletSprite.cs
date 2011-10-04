using UnityEngine;
using System.Collections;

public class BulletSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		// Position de la camera dans l'espace du sprite
		Vector3 SpriteToCam = transform.InverseTransformPoint(Camera.mainCamera.transform.position);

		// Projection du vecteur pour obtenir forward
		SpriteToCam.x = 0.0f;
		
		transform.LookAt(transform.TransformPoint(SpriteToCam), transform.up);
		
	}
}
 