using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour {

	/*
	 *  Reference the database that we can modify species amount, performance rate. etc
	 */
	public List <CharacterManager> characters = new List <CharacterManager>();
	public PlayerManager database;

	/*
	 *  Initialize Species Object
	 */
	public GameObject Yello;
	public GameObject Hexapod;
	public GameObject Copepod;


	void Start(){

		// Reference the player database and adding species into the database
 		database = GameObject.FindGameObjectWithTag ("PlayerDatabase").GetComponent <PlayerManager> ();
		characters.Add (database.species [0]);
		characters.Add (database.species [1]);
		characters.Add (database.species [2]);
	}


	void Update(){

		database.species [0].Reproduce ();
	}



	// Food obtain
	private void Predation(){

		//not enough food obtain
		if (database.level2amount > 
			database.level1amount * database.species [1].EatingRate() 
			* database.species [1].performancerate) {

			float ratio = database.level1amount/database.level2amount/database.species[2].EatingRate();

			database.species [1].speciesamount =database.level2amount * ratio;
			database.level1amount= 0;
			database.species[0].speciesamount = 0;
		} else {

			database.level1amount = database.level1amount 
				- (database.level2amount * database.species [1].EatingRate() 
				   * database.species [1].performancerate);
			database.species [1].Reproduce ();
		}

		if (database.level3amount > 
			database.level2amount * database.species [2].EatingRate() 
			* database.species [2].performancerate) {

			float ratio = database.level2amount/database.level3amount/database.species[2].EatingRate();

			database.species [2].speciesamount =database.level2amount * ratio;
			database.level2amount = 0;
			database.species[0].speciesamount = 0;

		} else {

			database.level2amount = database.level2amount 
				- (database.level3amount * database.species [2].EatingRate() 
				   * database.species [2].performancerate);
			database.species [2].Reproduce ();
			
		}
	}



}
