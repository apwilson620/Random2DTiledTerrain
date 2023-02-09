using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUnloadTerrain : MonoBehaviour
{

    //private GameObject tile = new GameObject();
    public GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameVisible()
    {
        //Debug.Log("visible");
        tile.SetActive(true);
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Invisible");
        tile.SetActive(false);
    }
}
