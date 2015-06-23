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

    //curve
    public CurveRenderer curveRender;

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

    private int selectedFish = 0;
    private int currentFishes = 0;

	// Use this for initialization
	void Start () {
		plus5.GetComponent<Button>().
			onClick.AddListener(()=> addfishes(5));
		plus10.GetComponent<Button> ().
			onClick.AddListener (() => addfishes (10));
		plus100.GetComponent<Button> ().
			onClick.AddListener (() => addfishes (100));
		tire1Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire1Selected, tire2Selected, tire3Selected, 1));
		tire2Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire2Selected, tire1Selected, tire3Selected, 2));
		tire3Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire3Selected, tire1Selected, tire2Selected, 3));
		selectionWindow.SetActive (false);
	}	

	// Update is called once per frame
	void Update () {

	}


	public void EnableWindow(GameObject SlectedTire, GameObject NonSelectedTire1, GameObject NonSelectedTire2, int tab){
		SlectedTire.SetActive (true);
		NonSelectedTire1.SetActive(false);
		NonSelectedTire2.SetActive(false);
        //currentTab = tab;
		selectionWindow.SetActive (false);

	}

    public void buttonPress(int fish)
    {
        //Debug.Log("click" + fish);
        //add 1 fish or switch to new fish type.
        if (selectedFish == fish)
        {
            currentFishes += 1;
        }
        else
        {
            selectedFish = fish;
            currentFishes = 1;
        }

        //cap purchasing based on money
        while (playerObj.species[selectedFish].cost * currentFishes > playerObj.moneys)
        {
            currentFishes--;
        }
        //update text
        totalfishes.text = currentFishes.ToString();
        //update thermal curve
        curveRender.curve = playerObj.species[selectedFish].thermalcurve;
    }

	public void SelectedWindow(GameObject selected){
		selectionWindow.SetActive (true);
		selectionWindow.transform.position = new Vector3 (selected.transform.position.x,
			 selected.transform.position.y, selected.transform.position.z);
	}


	public void addfishes(int number){
		currentFishes += number;

        //cap purchasing based on money
        while (playerObj.species[selectedFish].cost * currentFishes > playerObj.moneys)
        {
            currentFishes--;
        }

        totalfishes.text = currentFishes.ToString();
	}

    public void buyFishes()
    {
        playerObj.CreatureAmountChanged(selectedFish, currentFishes);
        playerObj.moneys = playerObj.moneys - playerObj.species[selectedFish].cost * currentFishes;
        currentFishes = 0;
        totalfishes.text = currentFishes.ToString();
    }
}
