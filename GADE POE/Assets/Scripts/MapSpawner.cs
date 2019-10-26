using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject meleeUnitOr, meleeUnitBl, rangedUnitOr, rangedUnitBl ,wizardUnit, resourceBuildingBl, resourceBuildingOr, factoryBuildingOr , factoryBuildingBl, map;
    public int numUnits = 10;
    public int numBuildings = 5;
    public static GameObject[] units;
    public static GameObject[] buildings;
    // Start is called before the first frame update
    void Start()
    {
        units = new GameObject[numUnits];
        buildings = new GameObject[numBuildings];
        InitializeUnits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeUnits()
    {

        for (int i = 0; i < units.Length; i++)
        {
            int x = Random.Range(1, (int)map.transform.localScale.x);
            int z = Random.Range(1, (int)map.transform.localScale.z);
            int unitType = Random.Range(0, 5);//exclusive of the max value
            int faction = Random.Range(0, 2);// ''
            int hp =0;

            if (unitType == 0)
            {
                units[i] = meleeUnitOr;
                hp = 20;
            }
            else if (unitType == 1)
            {
                units[i] = meleeUnitBl;
                hp = 20;
            }
            else if (unitType == 2)
            {
                units[i] = rangedUnitOr;
                hp = 15;
            }
            else if (unitType == 3)
            {
                units[i] = rangedUnitBl;
                hp = 15;
            }
            else
            {
                units[i] = wizardUnit;
                hp = 10;
            }


            units[i].transform.position = new Vector3(x, (float)0.5, z);
            Instantiate(units[i]);//puts units on the map (one at a time)
            UnitController1 uc = units[i].GetComponent<UnitController1>();
            uc.hp = hp;
        }

        for (int j = 0; j < buildings.Length; j++)
        {
            int x = Random.Range(1, (int)map.transform.localScale.x);
            int z = Random.Range(1, (int)map.transform.localScale.z);
            int buildingType = Random.Range(0, 4);//exclusive of the max value
            int faction = Random.Range(0, 2);// ''
            int hp = 0;


            if (buildingType == 0)
            {
                buildings[j] = factoryBuildingOr;
                hp = 60;
            }
            else if (buildingType == 1)
            {
                buildings[j] = factoryBuildingBl;
                hp = 60;
            }
            else if(buildingType == 2)
            {
                buildings[j] = resourceBuildingOr;
                hp = 50;
            }
            else if (buildingType == 3)
            {
                buildings[j] = resourceBuildingBl;
                hp = 50;
            }

            buildings[j].transform.position = new Vector3(x, (float)0.5, z);
            Instantiate(buildings[j]);
            BuildingController bc = buildings[j].GetComponent<BuildingController>();
            bc.hp = hp;
        }



    }
}
