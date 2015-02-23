using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;


public class Mussel : MonoBehaviour {
	/*
	// Setting Parameter
		//temeprature correction
		double T1    = 295.15;// 1, K, Reference temperature / T_opt ; 
		double T_A   = 4258; // 2, K, Arrhenius temperature ;
		double T_AL  = 7457;  // 3, K, Arrhenius temperature for lower boundary   
		double	T_AH  = 19664; // 4, K, Arrhenius temperature for upper boundary
		double	T_L   = 286;   // 5, Lower temperature boundary
		double	T_H   = 298;   // 6, Upper temperature boundary

		//feeding & assimilation
		double F_m   = 128/24f;       // 7, l/d.cm^2, {F_m} max spec searching rate
		double kap_X = 0.8f;          // 8, -, digestion efficiency of food to reserve
		double p_Am  = 28.4982/24f;   // 9, J/cm^2/d, maximum surface-specific assimilation rate p_Am = z * p_M/ kap


		// mobilisation, maintenance, growth & reproduction
			double v     = 0.01431/24; // 10, cm/d, energy conductance
			double kap   = 0.9885f;     // 11, -, allocation fraction to soma = growth + somatic maintenance
			double kap_R = 0.9165f;     // 12, -, reproduction efficiency
			double p_M   = 15.15/24f;   // 13, J/d.cm^3, [p_M], vol-specific somatic maintenance
			double p_T   = 0f;          // 14,  J/d.cm^2, {p_T}, surface-specific som maintenance
			double k_J   = 0.002/24f;   // 15, 1/d, maturity maint rate coefficient
			double E_G   =  3140f;      // 16, J/cm^3, [E_G], spec cost for structure
			
		// life stages: E_H is the cumulated energy from reserve invested in maturation
			double E_Hb = 2.125e-6;    // 17, J, E_H^b, maturity at birth
			double E_Hp = 74.55f;       // 18, J, E_H^p, maturity at puberty
			
		// param to compute observable quantities
			double del_M = 0.2417f;  // 19, -, shape coefficient to convert vol-length to physical length
			
			double d_V  = 0.12f; 	   // 20, g/cm^3, specific density of structure (dry weight)
			double mu_V = 500000f;   // 21, J/mol
			double mu_E = 550000f;   // 22, J/mol
			double w_V  = 23.9f;     // 23, g/mol
			double w_E  = 23.9f;     // 24, g/mol
			double w    = 12f;       // 25  wet / dry weight coefficient

}*/

	/*public void Read()
	{
		StreamReader sr = new StreamReader ("/Users/liang.chis/Documents/Marine/Script Test/Assets/Script/WaterTemps_sine_02032015.txt", Encoding.Default);
		string line;
		while ((line = sr.ReadLine()) != null) {

			Debug.Log(line.ToString ());
		}

	}*/

	protected FileInfo     theSourceFile = null;
	protected StreamReader reader = null;
	protected string text1 = " "; // To Read the Water Temperature
	protected string text2 = " "; // To Read the Food Concentration
	char[] delimiterchar = {' ', '/','\t'};
	string[] words1; // temptorary variable to load the data
	string[] words2; // temptorary variable to load the data
	List<double> WaterTemplist = new List<double>();
	List<double> FoodConcentlist = new List<double>();

	void Start () {
		inputwatertemperature ();
		inputfoodconcentration ();
		//testing the result to see if the data are remained in thelist
		print (WaterTemplist [0]*2);
		print (FoodConcentlist [0] * 3);	
	}

	public void inputwatertemperature()
	{
	//try{
		//open file
		theSourceFile = new FileInfo ("/Users/liang.chis/Documents/Marine/Script Test/Assets/Script/WaterTemps_sine_02032015.txt");
		reader = theSourceFile.OpenText();
		int i = -1;
		do {
			//skip the first line
			if(i<0)
			{
				reader.ReadLine ();
				i++;
			}
			text1 = reader.ReadLine ();
			words1 = text1.Split (delimiterchar);
			string watertemp= words1 [7];   //obtain the data from the txt
			double WaterD = double.Parse (watertemp);  // convert to double
			WaterTemplist.Add(WaterD); // add the data to the list

			} while(reader.Peek() > -1) ;
		//}
		//catch(Exception ex){
		//	print (ex.ToString());
		//}
	}

	void inputfoodconcentration()
	{

		theSourceFile = new FileInfo ("/Users/liang.chis/Documents/Marine/Script Test/Assets/Script/Food_sine_02032015.txt");
		reader = theSourceFile.OpenText();
		int i = -1;
		do {
			if(i<0)
			{
				reader.ReadLine ();
				i++;
			}
			text2 = reader.ReadLine ();
			words2 = text2.Split (delimiterchar);
			string foodtemp = words2 [7];
			double FoodC = double.Parse(foodtemp);
			FoodConcentlist.Add(FoodC);
		} while(reader.Peek () > -1) ;
	}
		
}
