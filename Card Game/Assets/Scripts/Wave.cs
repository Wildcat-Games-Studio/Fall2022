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

    public Wave()
    {
        waveSegments = new WaveSegment[0];
    }

    public Wave(Wave template)
    {
        waveSegments = new WaveSegment[template.waveSegments.Length];
        for (int i = 0; i < waveSegments.Length; i++)
        {
            waveSegments[i] = new WaveSegment();

            waveSegments[i].enemyToSpawn = template.waveSegments[i].enemyToSpawn;
            waveSegments[i].enemyAmount = template.waveSegments[i].enemyAmount;
            waveSegments[i].timeBetweenEnemy = template.waveSegments[i].timeBetweenEnemy;
            waveSegments[i].startDelay = template.waveSegments[i].startDelay;
        }
    }
}
