using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Pages : MonoBehaviour {

	//Buttons
	public GameObject rightButton;
	public GameObject leftButton;

	//Panels
	public GameObject Panel1;
	public GameObject Panel2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		rightButton.GetComponent<Button> ().
			onClick.AddListener (() => Windows (rightButton, leftButton, Panel1, Panel2));
		leftButton.GetComponent<Button> ().
			onClick.AddListener (() => Windows (leftButton, rightButton, Panel2, Panel1));
	}
	
	public void Windows(GameObject selectedButton, GameObject nonselectedButton,GameObject selectedPanel,GameObject nonselectedPanel){
		selectedPanel.SetActive (false);
		nonselectedPanel.SetActive (true);
		selectedButton.SetActive (false);
		nonselectedButton.SetActive (true);
	}



}
