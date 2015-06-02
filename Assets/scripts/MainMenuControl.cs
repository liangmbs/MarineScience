using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenuControl : MonoBehaviour {


	//initialize the window
	public GameObject window;
	//initialize Button
	public GameObject controler;
	public GameObject screen;

	public RectTransform sc;
	public RectTransform rt;
	
	public bool opened = false;

	public float start(){

		return window.transform.position.x;
	}
		
	void Start(){

		rt = (RectTransform)window.transform;
		sc = (RectTransform)screen.transform;
	}

	void Update(){
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());
	}
	
	void OnGUI(){
		if (opened == true) {
			float starting = start ();
			float bounds =rt.rect.width;
			controler.transform.rotation = Quaternion.Euler(0,0,0);
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, sc.rect.width - bounds/2
				             , Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
			GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = false;
		}
		
		if (opened == false) {
			float starting = start ();
			float bounds =rt.rect.width;
			controler.transform.rotation = Quaternion.Euler(0,0,180);
			window.transform.position = new Vector3
				(Mathf.Lerp ( starting,sc.rect.width + bounds/2
				             , Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
			GameObject.Find("Main Camera").GetComponent<MovingCamera>().enabled = true;
	
		}
	}
	
	
	public void Controler(){
		opened = !opened;
	}


}
