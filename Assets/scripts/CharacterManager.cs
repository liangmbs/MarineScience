using UnityEngine;
using System.Collections;

[System.Serializable]	
public class CharacterManager{

	/*
	 *  Species action resutls
	 */

	public float speciesamount ;
	public float performancerate;
	public ThermalCurve thermalcurve;
	public PlayerManager player;
	/*
	 *  Species Property
	 */
	protected string speciesname;
	protected float eatingrate;
	protected int specieslevel;
	protected float reproductiomulti;
	/*
	 *  Adding fish information when try to buy/create fish
	 */
	public CharacterManager (int eat, int level, float temperature, string name){
		speciesname = name;
		eatingrate = eat;
		specieslevel = level;
	}
	
	public string SpeciesName(){
		return speciesname;
	}

	public float EatingRate(){
		return eatingrate;
	}

	public int SpeciesLevel(){
		return specieslevel;
	}

	public float Reproductionrate(){
		return reproductiomulti;
	}
	
	/*
	 *  obtain the performance rate based on the optimal temperature
	 */
	void Update(){
		performancerate = thermalcurve.getCurve (player.currentmperature);
		Death ();
	}

	/*
	 *  As long as the performance rate is below 0.3f, the species is death
	 */
	private void Death(){
		if (performancerate < 0.3) {
			speciesamount = speciesamount - 1;
			if(speciesamount <= 0){
				Debug.Log("the species is death");
				speciesamount = 0;
			}
		} 
		player.Levelsamount ();
	}

	// Reproduce the new fish
	public void Reproduce(){
			speciesamount =  speciesamount* performancerate *Reproductionrate(); 
			player.Levelsamount ();
	}
	
}
