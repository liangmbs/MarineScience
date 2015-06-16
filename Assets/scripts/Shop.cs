using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class Shop : MonoBehaviour {

    public PlayerManager playerObj;

	//initialize the prefab
	public GameObject selectionWindow;

	//text
	public Text totalfishes;

	//plus button
	public GameObject plus5;
	public GameObject plus10;
	public GameObject plus100;
	
	// Tires' button
	public GameObject tire1Button;
	public GameObject tire2Button;
	public GameObject tire3Button;

	//tires' selection
	public GameObject tire1Selected;
	public GameObject tire2Selected;
	public GameObject tire3Selected;

    private int currentTab = 1;

	// Use this for initialization
	void Start () {
		plus5.GetComponent<Button>().
			onClick.AddListener(()=> addfishes(5));
		plus10.GetComponent<Button> ().
			onClick.AddListener (() => addfishes (10));
		plus100.GetComponent<Button> ().
			onClick.AddListener (() => addfishes (100));
	}	

	// Update is called once per frame
	void Update () {
		tire1Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire1Selected, tire2Selected, tire3Selected, 1));
		tire2Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire2Selected, tire1Selected, tire3Selected, 2));
		tire3Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire3Selected, tire1Selected, tire2Selected, 3));
	}


	public void EnableWindow(GameObject SlectedTire, GameObject NonSelectedTire1, GameObject NonSelectedTire2, int tab){
		SlectedTire.SetActive (true);
		NonSelectedTire1.SetActive(false);
		NonSelectedTire2.SetActive(false);
        currentTab = tab;
	}

    public void buttonPress(int button)
    {
        switch(currentTab)
        {
            case 1:
                playerObj.Buy(0);
                break;
            case 2:
                playerObj.Buy(1);
                break;
            case 3:
                playerObj.Buy(2);
                break;
        }
    }

	public void SelectedWindow(GameObject selected){
		selectionWindow.SetActive (true);
		selectionWindow.transform.position = new Vector3
			(Mathf.Lerp (selectionWindow.transform.position.x, selected.transform.position.x, Time.deltaTime), 
			 selected.transform.position.y, selected.transform.position.z);
	}


	public void addfishes(int number){

		int totalnumber = Int16.Parse(totalfishes.text);
		totalnumber += number;
		totalfishes.text = totalnumber.ToString ();
	}
}
