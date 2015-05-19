using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Shop : MonoBehaviour {

	// Tires' button
	public GameObject tire1Button;
	public GameObject tire2Button;
	public GameObject tire3Button;

	//tires' selection
	public GameObject tire1Selected;
	public GameObject tire2Selected;
	public GameObject tire3Selected;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tire1Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire1Selected, tire2Selected, tire3Selected));
		tire2Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire2Selected, tire1Selected, tire3Selected));
		tire3Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire3Selected, tire1Selected, tire2Selected));
	}


	public void EnableWindow(GameObject SlectedTire, GameObject NonSelectedTire1, GameObject NonSelectedTire2){
		SlectedTire.SetActive (true);
		NonSelectedTire1.SetActive(false);
		NonSelectedTire2.SetActive(false);	
	}





}
