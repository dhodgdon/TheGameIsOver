using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializeable]
public class Tower {

    public string name;
    public int cost;
    public GameObject prefab;

    public Tower ( string _name, int _cost, GameObject _prefab){
        _name = name;
        _cost = cost;
        _prefab = prefab;
    }
}
