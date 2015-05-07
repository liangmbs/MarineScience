using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	/*
	 *  Player's species
	 */
	public List<CharacterManager> species = new List<CharacterManager> ();
    //Highest level in the species list
    public int lowestLevel = 1;
    public int highestLevel = 3;

	/*
	 *  Player's data
	 */

	public float days = 0;
	public float moneys = 0;
	public float currentTemperature = 20;

	/*
	 * Initialize with three species at each level
	 */
	void Awake(){

	}

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StepEcosystem(1);
        }
    }

    //step the ecosystem forward by a given number or fraction of days
    public void StepEcosystem(float dayStep)
    {
        days += dayStep;
        //TODO: update temperature
        foreach (CharacterManager c in species)
        {
            c.updatePerformance(currentTemperature);
        }
        Predation(dayStep);
        foreach (CharacterManager c in species)
        {
            c.ReproduceOrDie(dayStep);
        }
    }

    //gets the total amount of all species for a given level
    private float getTotalAmountAtLevel(int level)
    {
        float amount = 0;
        foreach (CharacterManager c in species)
        {
            if (c.foodChainLevel == level)
            {
                amount += c.speciesAmount;
            }
        }
        return amount;
    }

    //eats a certain amount of creatures sampled evenly within that level
    //negative things will happen if eatAmount is > the amount of creatures
    //^^^^^^^^ pun intended.
    private void eatAtLevel(int level, float eatAmount)
    {
        //make a list of all creatures in this level
        float totalAmount = 0;
        List<CharacterManager> eatenCreatures = new List<CharacterManager>();
        foreach (CharacterManager c in species)
        {
            if (c.foodChainLevel == level)
            {
                eatenCreatures.Add(c);
                totalAmount += c.speciesAmount;
            }
        }
        //eat them... EAT THEM!!! 
        foreach (CharacterManager c in eatenCreatures)
        {
            float ratio = c.speciesAmount / totalAmount;
            c.speciesAmount -= ratio * eatAmount;
        }
    }

    // Food obtain
    private void Predation(float dayStep)
    {
        //step through each species level, skipping the first one (they don't eat anything)
        for (int i = lowestLevel + 1; i <= highestLevel; i++ )
        {
            List<CharacterManager> leveliCharacters = new List<CharacterManager>();
            float leveliAmount = 0;
            float foodRequestAmount = 0;
            foreach (CharacterManager c in species)
            {
                if (c.foodChainLevel == i)
                {
                    leveliCharacters.Add(c);
                    leveliAmount += c.speciesAmount;
                    foodRequestAmount += c.speciesAmount * c.GetEatingRate() * dayStep;
                }
            }

            //if we don't have enough food to go around
            if (foodRequestAmount > getTotalAmountAtLevel(i - 1))
            {
                //set fed performance to the reduced amount. 
                float subOptimalFoodRate = getTotalAmountAtLevel(i-1) / foodRequestAmount;
                foreach (CharacterManager c in leveliCharacters)
                {
                    c.fedRate = subOptimalFoodRate;
                }
                eatAtLevel(i - 1, getTotalAmountAtLevel(i - 1));
            }
            else
            {
                foreach (CharacterManager c in leveliCharacters)
                {
                    c.fedRate = 1;
                }
                eatAtLevel(i - 1, foodRequestAmount);
            }
        }
    }

}
