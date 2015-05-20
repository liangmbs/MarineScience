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

	
	public bool opened = false;

	public float bounds;
	public float end;


	public float start(){

		return window.transform.position.x;
	}
		
	void Start(){

		RectTransform rt = (RectTransform)window.transform;
		sc = (RectTransform)screen.transform;
		bounds =rt.rect.width;
	}

	void Update(){
		controler.GetComponent<Button>().
			onClick.AddListener (() => Controler());
	}
	
	void OnGUI(){
		if (opened == true) {
			float starting = start ();
			controler.transform.rotation = Quaternion.Euler(0,0,0);
			window.transform.position = new Vector3
				(Mathf.Lerp (starting, sc.rect.width - bounds/2
				             , Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
		}
		
		if (opened == false) {
			float starting = start ();
			controler.transform.rotation = Quaternion.Euler(0,0,180);
			window.transform.position = new Vector3
				(Mathf.Lerp ( starting,sc.rect.width + bounds/2
				             , Time.deltaTime * 0.8f), 
				 window.transform.position.y, window.transform.position.z);
		}
	}
	
	
	public void Controler(){
		opened = !opened;
	}


}
