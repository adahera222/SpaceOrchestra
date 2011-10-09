using UnityEngine;
using System.Collections;

public class TargetSprite : MonoBehaviour {

	Ship target;
	public bool useMainCamera = true;
	public Camera cameraToUse ; 
	
	float clampBorderSize = 0.05f;
	
	float rad_per_sec = 0.1f;
	float theta = 0.0f;
	
	Camera cam ;
	GUITexture texture;

    void Start () 
    {
	    if (useMainCamera)
	        cam = Camera.main;
	    else
	        cam = cameraToUse;
		
		texture = (GUITexture) transform.GetComponent("GUITexture");
    }
	
	public void setTarget(Ship target) {
		this.target = target;	
	}


    void Update() {
		
		if(target == null)
			texture.enabled = false;
		else
			texture.enabled = true;
		
		theta += rad_per_sec*Time.deltaTime;
				
		Vector3 relativePosition = cam.transform.InverseTransformPoint(target.transform.position);
		relativePosition.z =  Mathf.Max(relativePosition.z, 1.0f);
		transform.position = cam.WorldToViewportPoint(cam.transform.TransformPoint(relativePosition));
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampBorderSize, 1.0f - clampBorderSize),
		                         Mathf.Clamp(transform.position.y, clampBorderSize, 1.0f - clampBorderSize),
		                         transform.position.z);
		
		transform.Rotate(0.0f,10.0f,10.0f);
    }
}
