using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Position de la camera dans l'espace du sprite
		Vector3 SpriteToCam = transform.InverseTransformPoint(Camera.mainCamera.transform.position);
		transform.LookAt(transform.TransformPoint(SpriteToCam), transform.up);
	
	}
}
