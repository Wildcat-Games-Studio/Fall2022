using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Wave[] waves;

    [SerializeField] Vector2 enemyStartPos;

    // Current wave variables
    [SerializeField] int currentWave = -1;
    [SerializeField] int currentWaveSegment = 0;
    [SerializeField] float waveTime = 0f;
    [SerializeField] bool waveInProgress = false;

    [SerializeField] List<GenericEnemy> enemiesRemaining = new List<GenericEnemy>();

    // Wave segment variables
    [SerializeField] List<WaveSegment> waveSegmentsInProgress = new List<WaveSegment>();
    [SerializeField] List<int> waveSegmentsEnemiesRemaining = new List<int>();
    [SerializeField] List<float> waveSegmentsCurrentTime = new List<float>();

    private static WaveManager instance;
    public static WaveManager Instance { get { return instance; } set { } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        // Check if wave is in progress
        if (!waveInProgress)
            return;

        // Add to wave time
        waveTime += Time.deltaTime;

        // Check if all enemies have been spawned
        if (waves[currentWave].waveSegments.Length <= currentWaveSegment)
        {
            // End the wave if no enemies are alive and all wave segments are finished
            if (enemiesRemaining.Count == 0 && waveSegmentsInProgress.Count == 0)
                waveInProgress = false;

            return;
        }

        Debug.Log("Went through here");


        // Start next wave segment if delay is up
        if (waveTime >= waves[currentWave].waveSegments[currentWaveSegment].startDelay)
        {
            // Add next segment to current segment list
            waveSegmentsInProgress.Add(waves[currentWave].waveSegments[currentWaveSegment]);
            waveSegmentsEnemiesRemaining.Add(waves[currentWave].waveSegments[currentWaveSegment].enemyAmount);
            waveSegmentsCurrentTime.Add(0);
            currentWaveSegment++;
        }

        // Run wave segments
        for (int i = 0; i < waveSegmentsInProgress.Count; i++)
        {
            i = RunSegment(i) ? i - 1 : i;
        }
    }

    bool RunSegment(int segment)
    {
        // Check if spawn delay is up
        if (waveTime - waveSegmentsCurrentTime[segment] < waveSegmentsInProgress[segment].timeBetweenEnemy)
            return false;

        // Spawn new enemy
        GenericEnemy enemySpawned = Instantiate(waveSegmentsInProgress[segment].enemyToSpawn, enemyStartPos, Quaternion.identity);
        enemiesRemaining.Add(enemySpawned);
        waveSegmentsEnemiesRemaining[segment]--;

        // End segment if all enemies are spawned
        if (waveSegmentsEnemiesRemaining[segment] <= 0)
        {
            waveSegmentsInProgress.RemoveAt(segment);
            waveSegmentsEnemiesRemaining.RemoveAt(segment);
            waveSegmentsCurrentTime.RemoveAt(segment);

            return true;
        }

        return false;
    }

    public void StartWave()
    {
        // Go to next wave
        currentWave++;

        // Reset wave values
        currentWaveSegment = 0;
        waveTime = 0;
        waveInProgress = true;
        enemiesRemaining.Clear();
        waveSegmentsInProgress.Clear();
        waveSegmentsEnemiesRemaining.Clear();
        waveSegmentsCurrentTime.Clear();
    }
}
