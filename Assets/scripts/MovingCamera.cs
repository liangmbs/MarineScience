using UnityEngine;
using System.Collections;

public class MovingCamera: MonoBehaviour {

protected float HorizontalSpeed =5.0f;
protected float VerticalSpeed =5.0f;

public BoxCollider Background;
private Vector3 min,max; 


void Start(){
	min = Background.bounds.min;
	max = Background.bounds.max;
}


	void update(){
		//SetActive ();
	}



void LateUpdate(){
	
	if (Input.GetButton ("Fire1")) {
		
		float h = HorizontalSpeed * Input.GetAxis ("Mouse Y");
		float v = VerticalSpeed * Input.GetAxis ("Mouse X");
		
		transform.Translate (v, h, 0);	
	}
	
	if (Input.GetAxis ("Mouse ScrollWheel") >0) {
		if(Camera.main.orthographicSize < 5)
			Camera.main.orthographicSize++;
		else{
			
		}
	}
	
	if (Input.GetAxis ("Mouse ScrollWheel") <0) {
		if(Camera.main.orthographicSize > 2)
			Camera.main.orthographicSize --;
		else{
			
		}
	}

	transform.position = new Vector3 (
		Mathf.Clamp (transform.position.x, min.x , max.x ),
		Mathf.Clamp (transform.position.y, min.y , max.y),
		Mathf.Clamp (transform.position.z, min.z, max.z));
}


	/*
	public void SetActive(){
		bool open1 = GameObject.Find ("ShopCanvas").GetComponent<ShopWindowControl>().open;
		bool open2 = GameObject.Find ("MainMenuCanvas").GetComponent<ShopWindowControl>().open;

		if (open1 == true || open2 == true) {
			gameObject.GetComponent<MovingCamera> ().enabled = false;
		} else {
			gameObject.GetComponent<MovingCamera> ().enabled = true;

		}

	}
*/




}
