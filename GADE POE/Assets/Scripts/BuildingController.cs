using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public int hp = 50;

    [SerializeField] int health, maxHealth;
    [SerializeField] string team;





    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHaelth
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

   
}
