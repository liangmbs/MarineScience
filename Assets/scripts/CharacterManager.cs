using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    //species stats (these change during gameplay)
	public float speciesAmount = 0;
	public float performanceRate = 1;
    public float fedRate = 1;
    public float cost = 100;

    //object references
	public ThermalCurve thermalcurve;
	public PlayerManager player;
	/*
	 *  Species Properties
	 */
	public string uniqueName = "Nameless"; //a unique name for each species
    public float eatingAmount = 3; //the amount of fish I need to eat every day
    public int foodChainLevel = 1; //how high I am in the food chain (1 == bottom)
    public float reproductionMultiplier = .5f; //how many babies I have every day

    public float deathThreashold = .3f; //if performance gets too low, you start dying
    public float deathRate = .5f; //if I'm dying, population drops by this ratio every day
    public float minimumDeaths = 1; //if I'm dying, I will always lose at least this many fish!

    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public SwimmingHolder swimming;

    private float lastTemp

    public void updatePerformance(float temperature)
    {
        lastTemp = temperature;
        performanceRate = thermalcurve.getCurve(temperature + 273);
    }
    
	public float GetEatingRate(){
		return eatingAmount * performanceRate;
	}

    public float getFinalPerformance()
    {
        return fedRate * performanceRate;
    }

    //fish reproduce or die based on performance.
    public void ReproduceOrDie(float days)
    {
        //if performance is too low, fish die
        if (getFinalPerformance() < deathThreashold)
        {
            float deaths = speciesAmount * deathRate;
            deaths = Mathf.Max(minimumDeaths, deathRate);
            float newAmount = speciesAmount - deaths * days;
            int changeAmount = Mathf.FloorToInt(speciesAmount) - Mathf.FloorToInt(newAmount);
            if (changeAmount > 0)
            {
                if (performanceRate < deathThreashold)
                {
                    if(lastTemp + 273 <= thermalcurve.optimalTemp) {
                        swimming.KillCreatures(id, changeAmount, player.tooCold);
                    } 
                    else {
                        swimming.KillCreatures(id, changeAmount, player.tooHot);
                    }
                }
                else
                {
                    swimming.KillCreatures(id, changeAmount, player.starved);
                }
            }
            speciesAmount = Mathf.Min(0, newAmount);
        }
        else //otherwise, fish reproduce
        {
            speciesAmount = speciesAmount + speciesAmount * getFinalPerformance() * 
                (reproductionMultiplier) * days;
        }

        if (speciesAmount < 1)
        {
            speciesAmount = 0;
        }
    }
	
	//obtain the performance rate based on the optimal temperature
	void Update(){

	}
	
}
