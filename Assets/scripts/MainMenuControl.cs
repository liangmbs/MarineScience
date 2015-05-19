using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuControl : MonoBehaviour {

	//initialize the window
	public Renderer window;
	public Renderer button;

	//initialize Button
	public GameObject controler;

	public bool opened = false;
	
	public float start(){
		
		return window.transform.position.x;
	}
		
	void Update(){
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());
	}
	
	void OnGUI(){
		if (opened) {
			float starting = start ();
			button.transform.rotation = Quaternion.Euler(0,0,0);
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, 734.1f, Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
		}
		
		if (!opened) {
			float starting = start ();
			button.transform.rotation = Quaternion.Euler(0,0,180);
			window.transform.position = new Vector3
				(Mathf.Lerp ( starting,853.6f, Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
		}
	}
	
	
	public void Controler(){
		opened = !opened;
	}


}
