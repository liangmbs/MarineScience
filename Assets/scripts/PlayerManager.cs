using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	/*
	 *  Player's species
	 */
	public List<CharacterManager> species = new List<CharacterManager> ();

	/*
	 *  Player's data
	 */
	public float level1amount = 0;
	public float level2amount = 0;
	public float level3amount = 0;

	public int days;
	public float moneys;
	public float currentmperature;

	/*
	 * Initialize with three species at each level
	 */
	void Awake(){
		species.Add (new CharacterManager (0,1,20.0f,"yelloo"));
		species.Add (new CharacterManager (2,2,20.0f,"Hexapod"));
		species.Add (new CharacterManager (4,3,20.0f,"Copepod"));
	}


	public void Levelsamount(){
		foreach (CharacterManager c in species) {
			int specieslevel = c.SpeciesLevel();
			switch (specieslevel) 
			{
			case 1:
				level1amount = level1amount + c.speciesamount;
				break;

			case 2:
				level2amount = level2amount + c.speciesamount;
				break;

			case 3:
				level3amount = level3amount + c.speciesamount;
				break;
			}
		}
	}

}
