using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Transform[] wayPoints;
    public GameObject spawnPoint;
    public GameObject endPoint;

    private void Awake()
    {
        GameManager.Instance.mapManager = this;
    }
}
