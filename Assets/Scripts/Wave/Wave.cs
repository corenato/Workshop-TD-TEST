using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave 
{
    public List<WaveEnemyEntry> enemies;

    public int duration = 10;

    public Transform[] allowedSpawnPoints;


}
