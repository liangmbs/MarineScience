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
    public Text totalPrice;
    public Text currentName;

    public Text sellingfishes;
    public Text sellingPrice;
    public Text sellingName;

    //curve
    public CurveRenderer curveRender;

	//sell slider
    public Scrollbar slider;
	
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
    private int currentSellingFishes = 0;

	// Use this for initialization
	void Start () {
		tire1Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire1Selected, tire2Selected, tire3Selected, 1));
		tire2Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire2Selected, tire1Selected, tire3Selected, 2));
		tire3Button.GetComponent<Button>().
			onClick.AddListener (() => EnableWindow (tire3Selected, tire1Selected, tire2Selected, 3));
		selectionWindow.SetActive (false);

        buttonPress(0);
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
        totalPrice.text = (playerObj.species[selectedFish].cost * currentFishes).ToString();
        currentName.text = playerObj.species[selectedFish].uniqueName;
        sellingName.text = currentName.text;
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
        if (currentFishes < 0)
        {
            currentFishes = 0;
        }

        //cap purchasing based on money
        while (playerObj.species[selectedFish].cost * currentFishes > playerObj.moneys)
        {
            currentFishes--;
        }

        totalfishes.text = currentFishes.ToString();
        totalPrice.text = (playerObj.species[selectedFish].cost * currentFishes).ToString();
	}

    public void addSellingFishes(int number)
    {
        currentSellingFishes += number;
        if (currentSellingFishes < 0)
        {
            currentSellingFishes = 0;
        }

        //cap purchasing based on money
        if (playerObj.species[selectedFish].speciesAmount < currentSellingFishes)
        {
            currentSellingFishes = Mathf.FloorToInt(playerObj.species[selectedFish].speciesAmount);
        }

        sellingfishes.text = currentSellingFishes.ToString();
        sellingPrice.text = (playerObj.species[selectedFish].cost * currentSellingFishes * playerObj.sellRate).ToString();
    }

    public void buyFishes()
    {
        playerObj.CreatureAmountChanged(selectedFish, currentFishes);
        playerObj.moneys = playerObj.moneys - playerObj.species[selectedFish].cost * currentFishes;
        currentFishes = 0;
        totalfishes.text = "0";
        totalPrice.text = "0";
    }

    public void sellFishes()
    {
        playerObj.CreatureAmountChanged(selectedFish, -currentSellingFishes);
        playerObj.moneys = playerObj.moneys + playerObj.species[selectedFish].cost * playerObj.sellRate * currentSellingFishes;
        currentSellingFishes = 0;
        sellingfishes.text = "0";
        sellingPrice.text = "0";
    }

    public void sliderSlide()
    {
        currentSellingFishes = Mathf.RoundToInt(playerObj.species[selectedFish].speciesAmount * slider.value);

        sellingfishes.text = currentSellingFishes.ToString();
        sellingPrice.text = (playerObj.species[selectedFish].cost * currentSellingFishes * playerObj.sellRate).ToString();
    }
}
