using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {
	
	public Material blink_mat;
	private Material old_mat;
	private bool need_reset;
	private int reset_count;
	
	// Use this for initialization
	void Start () {
		old_mat = renderer.material;
		need_reset = false;
		reset_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(need_reset) {
			if(reset_count > 3) {
				renderer.material = old_mat;
				need_reset = false;
				reset_count = 0;
			} else {
				reset_count++;
			}
		}
	}
	
	void OnImpact() {
		renderer.material = blink_mat;
		need_reset = true;
	}
}
