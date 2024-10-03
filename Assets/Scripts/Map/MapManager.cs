using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public Transform[] wayPoints;
    public GameObject spawnPoint;
    public GameObject endPoint;
}
