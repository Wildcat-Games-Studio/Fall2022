using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSegment
{
    public GenericEnemy enemyToSpawn;
    public int enemyAmount;
    public float timeBetweenEnemy;
    public float startDelay;
}

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    public WaveSegment[] waveSegments;
}
