using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopWindowControl : MonoBehaviour {

	//initialize the window
	public GameObject window;
	//public Renderer button;


	//initialize Button
	public GameObject controler;
	RectTransform rt;

	
	//define if it is open;
	public bool open = false;
	public float bounds;

	public float start(){

		return window.transform.position.x;
	}
	

	void Start(){
		rt = (RectTransform)window.transform;
	}

	void Update(){
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());
	}

	void OnGUI(){
		if (open == true) {
			float starting = start ();
			bounds =rt.rect.width;
			controler.transform.rotation = Quaternion.Euler(0,0,180);
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, bounds/2, Time.deltaTime), 
				 window.transform.position.y, window.transform.position.z);
			GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = false;

		}

		if (open == false) {
			float starting = start ();
			bounds =rt.rect.width;
			controler.transform.rotation = Quaternion.Euler(0,0,0);
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, -bounds/2,  Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
			GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = true;

		}
	}


	public void Controler(){
		open = !open;
	}

}
