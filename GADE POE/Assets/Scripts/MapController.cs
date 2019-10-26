using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour//game engine, control combat
{
    public int width = 1;
    public int length = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(width, 1, length);
        transform.position = new Vector3(width / 2, 0, length / 2);
        UnitController1 unitController = MapSpawner.units[0].GetComponent<UnitController1>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
