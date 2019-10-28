using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController1 : MonoBehaviour
{
    public int hp = 20;

    [SerializeField] int health, maxHealth, speed, attack;
    [SerializeField] string team;
    [SerializeField] float range;


    float dis, tempDis;
    UnitController1 closestUnit, targetedUnit;
    BuildingController closestBuilding, targetedBuilding;

    public  int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public string Team
    {
        get { return team; }
    }



    // Start is called before the first frame update
    void Start()
    {

        if (name == "MeleeUnitBl(Clone)")
        {
            maxHealth = 30;
            speed = 1;
            attack = 10;
            range = 1.9f;
            team = "Blue";

        }
        if (name == "MeleeUnitOr(Clone)")
        {
            maxHealth = 30;
            speed = 1;
            attack = 10;
            range = 1.9f;
            team = "Orange";

        }
        if (name == "RangeUnitBl(Clone)")
        {
            maxHealth = 15;
            speed = 3;
            attack = 15;
            range = 3f;
            team = "Blue";

        }
        if (name == "RangeUnitOr(Clone)")
        {
            maxHealth = 15;
            speed = 3;
            attack = 15;
            range = 3f;
            team = "Orange";
        }
        if (name == "WizardUnit(Clone)")
        {
            maxHealth = 10;
            speed = 2;
            attack = 25;
            range = 1.9f;
            team = "Wizard";
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();

        if (targetedBuilding != null)
        {
            tempDis = (targetedBuilding.transform.position - transform.position).magnitude;
            if (tempDis <= range)
            {
                Attack(targetedBuilding);
            }
            else
            {
                MoveToBuilding(targetedBuilding);
            }
        }
        else if(targetedUnit != null)
        {
            tempDis = (targetedUnit.transform.position - transform.position).magnitude;
            if (tempDis <= range)
            {
                Attack(targetedUnit);
            }
            else
            {
                MoveToUnit(targetedUnit);
            }
        }
        else
        {
            if (name == "RangeUnitOr(Clone)" || name == "MeleeUnitOr(Clone)")
            {
                Debug.Log("The Orange team wins!");
            }
            else if (name == "RangeUnitBl(Clone)" || name == "MeleeUnitBl(Clone)")
            {
                Debug.Log("The Blue team wins!");

            }
            else
            {
                Debug.Log("The Wizard team wins!");
            }
        }

        StayInBounds();
    }


    void Attack(BuildingController target)
    {
        target.Health -= attack;
        //Debug.Log(target + " has" + Health + " health.");

        if (target.Health <= 0)
        {
            target.Dead();
            //Debug.Log(target + " has died.");

        }
    }

    void Attack(UnitController1 target)
    {
        target.Health -= attack;
        //Debug.Log(target + " has"+Health+" health.");


        if (target.Health <= 0)
        {
            target.Dead();
            //Debug.Log(target + " has died.");
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void MoveToUnit(UnitController1 unit)
    {
        int xDis, zDis, moves;

        xDis = (int)Mathf.Abs(unit.transform.position.x - transform.position.x);
        zDis = (int)Mathf.Abs(unit.transform.position.z - transform.position.z);

        if (xDis>=zDis)
        {
            if (xDis > 0)
            {
                moves = 1;
            }
            else
            {
                moves = -1;
            }

            transform.position = new Vector3((transform.position.x + moves), transform.position.y, transform.position.z);
        }
        else
        {
            if (zDis > 0)
            {
                moves = 1;
            }
            else
            {
                moves = -1;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + moves));

        }
    }

    private void MoveToBuilding(BuildingController building)
    {
        int xDis, zDis, moves;

        xDis = (int)Mathf.Abs(building.transform.position.x - transform.position.x);
        zDis = (int)Mathf.Abs(building.transform.position.z - transform.position.z);

        if (Mathf.Abs(xDis) >= Mathf.Abs(zDis))
        {
            if (xDis > 0)
            {
                moves = 1;
            }
            else
            {
                moves = -1;
            }

            transform.position = new Vector3((transform.position.x + moves), transform.position.y, transform.position.z);
        }
        else
        {
            if (zDis > 0)
            {
                moves = 1;
            }
            else
            {
                moves = -1;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + moves));

        }
    }

    void FindTarget()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        dis = float.MaxValue;
        targetedUnit = null;
        targetedBuilding = null;

        if (buildings !=null)
        {
            for (int a = 0; a < buildings.Length; a++)
            {
                closestBuilding = buildings[a].GetComponent<BuildingController>();

                if (closestBuilding == null || closestBuilding.Team == Team)
                {
                    continue;
                }

                tempDis = (buildings[a].transform.position - transform.position).magnitude;

                if (tempDis < dis)
                {
                    targetedBuilding = closestBuilding;
                    dis = tempDis;
                }
            }
        }

        if (units != null)
        {
            for (int b = 0; b < units.Length; b++)
            {
                closestUnit = units[b].GetComponent<UnitController1>();

                if (closestUnit == null || closestUnit.Team == Team)
                {
                    continue;
                }

                tempDis = (units[b].transform.position - transform.position).magnitude;

                if (tempDis < dis)
                {
                    targetedUnit = closestUnit;
                    dis = tempDis;
                }
            }
        }
    }

    void StayInBounds()
    {


        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        if (transform.position.x > MapController.Width - 1)
        {
            transform.position = new Vector3(MapController.Width - 1, transform.position.y, transform.position.z);
        }

        if (transform.position.z > MapController.Length - 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, MapController.Length - 1);
        }

    }
}
