using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] List<Wave> waves;
    [SerializeField] Wave waveTemplate;

    [Header("Wave Generation")]
    [SerializeField] int numWaves;
    [SerializeField] bool randomizeSeed;
    [SerializeField] int fixedSeed;

    [Header("UI")]
    [SerializeField] TMP_Text waveText;
    [SerializeField] Button nextWaveButton;
    [SerializeField] GameObject winScreen;

    // Current wave variables
    [SerializeField] int currentWave = -1;
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

        if (randomizeSeed)
            Random.InitState(fixedSeed);

        for (int i = waves.Count + 1; i <= numWaves; i++)
        {
            waves.Add(GenerateWave(i));
        }
    }

    void Update()
    {
        // Speed up (For debug purposes)
        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                Time.timeScale = 10f;
            else
                Time.timeScale = 5f;
        }
        else
            Time.timeScale = 1f;

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
                if (currentWave + 1 >= waves.Count)
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
    }

    bool RunSegment(int segment)
    {
        // Check if segment is empty
        if (waveSegmentsInProgress[segment].enemyAmount == 0)
        {
            waveSegmentsInProgress.RemoveAt(segment);
            waveSegmentsEnemiesRemaining.RemoveAt(segment);
            waveSegmentsCurrentTime.RemoveAt(segment);

            return true;
        }

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
        if (waveInProgress || currentWave + 1 >= waves.Count)
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

    Wave GenerateWave(int score)
    {
        int numEnemies = score * 5;

        Wave wave = new Wave(waveTemplate);

        for (int i = 0; i < numEnemies; i++)
        {
            wave.waveSegments[Random.Range(0, wave.waveSegments.Length)].enemyAmount++;
        }

        for (int i = 0; i < wave.waveSegments.Length; i++)
        {
            Debug.Log($"i: {i}, Wave Segment Length: {wave.waveSegments.Length}. Max Spacing: {5f - ((wave.waveSegments.Length - i) * .3f)}");
            float spaceBetweenEnemy = Random.Range(0.5f, 5f - ((wave.waveSegments.Length - i) * .33f));
            float spacing = 1f / wave.waveSegments[i].enemyToSpawn.GetSpeed() * spaceBetweenEnemy;
            wave.waveSegments[i].timeBetweenEnemy = spacing;

            if (i == 0)
                continue;

            float currentWaveTime = wave.waveSegments[i - 1].startDelay;
            float maxDelay = currentWaveTime + wave.waveSegments[i - 1].enemyAmount * wave.waveSegments[i - 1].timeBetweenEnemy;
            if (Random.Range(0, 1f) <= 0.1f)
                wave.waveSegments[i].startDelay = Random.Range(currentWaveTime, maxDelay);
            else
                wave.waveSegments[i].startDelay = maxDelay;
        }

        return wave;
    }
}
