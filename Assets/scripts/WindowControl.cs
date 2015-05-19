using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowControl : MonoBehaviour {

	//initialize the window
	public Renderer screen;
	public Renderer window;
	public Renderer Button;


	//initialize Button
	public GameObject controler;



	//define if it is open;
	public bool opened;

	public float start(){

		return window.transform.position.x;
	}

	public float end(){

		return -(screen.transform.position.x);
	}

	void Start(){
		print (Button.transform.position);
		print (window.transform.position);
	}

	void Update(){
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler(window));
	}

	void onGUI(){
		if (opened) {
			float starting = start ();
			float ending = end ();
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, ending, Time.deltaTime * 0.2f), window.transform.position.y, window.transform.position.z);
		}

	}


	public void Controler(Renderer window){
		opened = !opened;
	}



}
