using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSegment
{
    public GenericEnemy enemyToSpawn;
    public int enemyAmount;
    [Tooltip("How much time between an enemy being sent out (seconds)")]
    public float timeBetweenEnemy;
    [Tooltip("How much time after starting the wave to begin this segment (seconds)")] 
    public float startDelay;
}

[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    public WaveSegment[] waveSegments;
}
