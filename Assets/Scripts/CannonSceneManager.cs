using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSceneManager : MonoBehaviour
{
    public GameObject Terrain;
    public Material[] planetMaterials;


    enum Planets
    {
        Mercury = 0,
        Venus = 1,
        Earth = 2,
        Mars = 3,
        Jupiter = 4
    }

    // Start is called before the first frame update
    void Awake()
    {
        var var = PlayerPrefs.GetString("CurrentPlanet", "Venus");
        var ind = (int) Enum.Parse(typeof(Planets), var);
        Terrain.GetComponent<Renderer>().material = planetMaterials[ind];
    }

    // Update is called once per frame
    void Update()
    {
    }
}