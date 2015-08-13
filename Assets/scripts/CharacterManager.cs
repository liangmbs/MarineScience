using UnityEngine;
using System.Collections.Generic;

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

    public enum DeathCause { Cold, Hot, Starve, Eaten, Sold};
    
    public Queue<DeathCause> deathList = new Queue<DeathCause>();

    public enum BirthCause { Reproduction, Bought};
    
    public Queue<BirthCause> birthList = new Queue<BirthCause>();

    private float lastTemp = 0;

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
            speciesAmount -= deaths * days;
            speciesAmount = Mathf.Min(0, speciesAmount);
            Debug.Log(deaths * days);
            //figure out why we're dying (starve, too hot, or too cool)
            if (fedRate < deathThreashold)
            {
                float starved = deaths * days;
                for (int i = 0; i < starved; i++)
                {
                    deathList.Enqueue(DeathCause.Starve);
                    Debug.Log("die starve");
                }
            }
            else
            {
                if (lastTemp + 273 < thermalcurve.optimalTemp)
                {
                    float cooled = deaths * days;
                    for (int i = 0; i < cooled; i++)
                    {
                        deathList.Enqueue(DeathCause.Cold);
                        Debug.Log("die cold");
                    }
                }
                else
                {
                    float heated = deaths * days;
                    for (int i = 0; i < heated; i++)
                    {
                        deathList.Enqueue(DeathCause.Hot);
                        Debug.Log("die hot");
                    }
                }
            }
        }
        else //otherwise, fish reproduce
        {
            float reproduced = speciesAmount * getFinalPerformance() * 
                (reproductionMultiplier) * days;
            speciesAmount = speciesAmount + reproduced;
            for(int i = 0; i < reproduced - .9f; i++) {
                birthList.Enqueue(BirthCause.Reproduction);
            }
        }

        if (speciesAmount < 1)
        {
            speciesAmount = 0;
            //clear out any straggler fish
            deathList.Enqueue(DeathCause.Starve);
            deathList.Enqueue(DeathCause.Starve);
            deathList.Enqueue(DeathCause.Starve);
            deathList.Enqueue(DeathCause.Starve);
        }
    }



	
}
