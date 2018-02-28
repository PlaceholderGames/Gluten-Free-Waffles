using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funds : MonoBehaviour
{
    public double money;
	// Use this for initialization
	void Start ()
    {
        money = 1000;	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void addingFunds(double add)
    {
        money = money + add;
    }

    void removingFunds(double sub)
    {
        money = money - sub;
    }
}
