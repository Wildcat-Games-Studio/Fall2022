using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] Wave[] waves;

    [Header("UI")]
    [SerializeField] TMP_Text waveText;
    [SerializeField] Button nextWaveButton;
    [SerializeField] GameObject winScreen;

    // Current wave variables
    int currentWave = -1;
    int currentWaveSegment = 0;
    float waveTime = 0f;
    bool waveInProgress = false;

    List<GenericEnemy> enemiesRemaining = new List<GenericEnemy>();

    // Wave segment variables
    List<WaveSegment> waveSegmentsInProgress = new List<WaveSegment>();
    List<int> waveSegmentsEnemiesRemaining = new List<int>();
    List<float> waveSegmentsCurrentTime = new List<float>();

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

    private void Start()
    {
        waveText.text = "Wave: 1";
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
            {
                waveInProgress = false;

                // Update UI
                waveText.text = $"Wave: {currentWave + 2}";

                // Check if all waves have been complete
                if (currentWave + 1 >= waves.Length)
                {
                    // Win screen
                    winScreen.SetActive(true);
                }
                else
                {
                    // Enable next wave button
                    nextWaveButton.interactable = true;
                }
            }
        }
        else
        {
            // Start next wave segment if delay is up
            if (waveTime >= waves[currentWave].waveSegments[currentWaveSegment].startDelay)
            {
                // Add next segment to current segment list
                waveSegmentsInProgress.Add(waves[currentWave].waveSegments[currentWaveSegment]);
                waveSegmentsEnemiesRemaining.Add(waves[currentWave].waveSegments[currentWaveSegment].enemyAmount);
                waveSegmentsCurrentTime.Add(0);
                currentWaveSegment++;
            }
        }

        // Run wave segments
        for (int i = 0; i < waveSegmentsInProgress.Count; i++)
        {
            i = RunSegment(i) ? i - 1 : i;
        }

        // Debug
        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 10f;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Time.timeScale = 1f;
        }
    }

    bool RunSegment(int segment)
    {
        // Check if spawn delay is up
        if (waveTime - waveSegmentsCurrentTime[segment] < waveSegmentsInProgress[segment].timeBetweenEnemy)
            return false;

        // Spawn new enemy
        GenericEnemy enemySpawned = Instantiate(waveSegmentsInProgress[segment].enemyToSpawn, transform);
        enemiesRemaining.Add(enemySpawned);
        waveSegmentsEnemiesRemaining[segment]--;
        waveSegmentsCurrentTime[segment] = waveTime;

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
        // Check if wave is in progress
        if (waveInProgress || currentWave + 1 >= waves.Length)
            return;

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

        // Disable next wave button
        nextWaveButton.interactable = false;
    }

    public void EnemyKilled(GenericEnemy enemyKilled)
    {
        enemiesRemaining.Remove(enemyKilled);
    }
}
