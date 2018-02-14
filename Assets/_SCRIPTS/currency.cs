using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public double currency;
    private Item item;
    private PickupDrop range;
    private int selection;
    private bool transaction;

    // Use this for initialization
    void Start()
    {
        currency = 1000.00;
        //used to reference
        selection = -1; //setting to minus 1 as it cant represent an item id
        transaction = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //will need to create a new tag called vendor
            if (Physics.Raycast(ray, out hit, range.itemRange) && hit.transform.tag == "vendor")
            {
                transaction = true;
                print("Ya buying or sellin? b/s");
                if (Input.GetKeyDown("b") && transaction == true)
                {
                    buying(hit);
                }
                else if (Input.GetKeyDown("s") && transaction == true)
                {
                    selling();
                }
            }
        }
    }

    void buying(RaycastHit hit)
    {
        int total = hit.transform.GetComponent<VendorSupplies>().supplies.Length;
        print(total);
        print("Wanna buy an energy drink for 100? y/n");
        //outputs the players current funds
        Debug.Log(currency);
        //player wants to buys item
        if (Input.GetKeyDown("y") && transaction == true)
        {
            currency = currency - 100;
            transaction = false;
            //need to add code to add item to players inventory
        }
        //player doesnt want to buy an item
        else if (Input.GetKeyDown("n") && transaction == true)
        {
            print("Okie see ya later");
            transaction = false;
        }
    }

    void selling()
    {
        print("Selling is not currently available, sorry :(");
    }
}