using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Para usar List
using TMPro; // Para usar TextMeshPro

public class WaveManager : MonoBehaviour
{
    // --- Configurações da Horda ---
    [Header("Configurações de Horda")]
    public List<GameObject> enemyPrefabs; // Lista de prefabs de inimigos comuns
    public List<GameObject> bossPrefabs;  // <--- MUDANÇA AQUI: Agora é uma lista de prefabs de Bosses
    public Transform[] spawnPoints;       // Array de pontos de spawn (arraste GameObjects vazios aqui)

    public float baseEnemiesPerWave = 3f;
    public float enemyIncreasePerWave = 1f;
    public float baseWaveInterval = 5f;
    public float waveIntervalDecreasePerWave = 0.2f;
    public float minWaveInterval = 2f;
    public int bossWaveInterval = 10;

    // --- Referências UI ---
    [Header("Interface do Usuário")]
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI waveTimerText;
    public GameObject waveCompletePanel;

    // --- Estado Interno ---
    private int currentWave = 0;
    private float waveCountdown;
    private int enemiesAlive;
    private bool spawningWave = false;

    void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Nenhum Ponto de Spawn atribuído ao Wave Manager! Por favor, adicione GameObjects vazios aos Spawn Points.");
            enabled = false;
            return;
        }

        waveCountdown = baseWaveInterval;
        UpdateWaveUI();
        if (waveCompletePanel != null) waveCompletePanel.SetActive(false);
    }

    void Update()
    {
        if (!spawningWave)
        {
            waveCountdown -= Time.deltaTime;
            UpdateWaveTimerUI();

            if (waveCountdown <= 0f)
            {
                StartCoroutine(StartNextWave());
            }
        }
    }

    IEnumerator StartNextWave()
    {
        spawningWave = true;
        currentWave++;
        UpdateWaveUI();

        if (waveCompletePanel != null) waveCompletePanel.SetActive(false);

        if (currentWave % bossWaveInterval == 0)
        {
            yield return StartCoroutine(SpawnBoss());
        }
        else
        {
            yield return StartCoroutine(SpawnRegularWave());
        }

        waveCountdown = Mathf.Max(minWaveInterval, baseWaveInterval - (currentWave * waveIntervalDecreasePerWave));
        spawningWave = false;
    }

    IEnumerator SpawnRegularWave()
    {
        Debug.Log("Iniciando Horda " + currentWave + "...");

        int enemiesToSpawn = Mathf.RoundToInt(baseEnemiesPerWave + (currentWave - 1) * enemyIncreasePerWave);
        enemiesAlive = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        while (enemiesAlive > 0)
        {
            yield return null;
        }

        Debug.Log("Horda " + currentWave + " completa!");
        if (waveCompletePanel != null) waveCompletePanel.SetActive(true);
    }

    IEnumerator SpawnBoss()
    {
        Debug.Log("Horda de BOSS! Horda " + currentWave + "!");

        // --- MUDANÇA AQUI: Escolhe um Boss aleatório da lista ---
        if (bossPrefabs == null || bossPrefabs.Count == 0)
        {
            Debug.LogWarning("Nenhum Prefab de Boss atribuído à lista bossPrefabs! Pulando Horda de Boss.");
            yield break;
        }

        GameObject bossToSpawn = bossPrefabs[Random.Range(0, bossPrefabs.Count)];
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject boss = Instantiate(bossToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation);
        boss.tag = "Enemy"; // Garante que o boss tenha a tag "Enemy" para o sistema de saúde

        enemiesAlive = 1; // Apenas o boss está vivo nesta horda

        while (boss != null && enemiesAlive > 0)
        {
            yield return null;
        }

        Debug.Log("Boss da Horda " + currentWave + " derrotado!");
        if (waveCompletePanel != null) waveCompletePanel.SetActive(true);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
        Debug.Log("Inimigos restantes: " + enemiesAlive);
    }

    void UpdateWaveUI()
    {
        if (waveNumberText != null)
        {
            waveNumberText.text = "Horda: " + currentWave;
        }
    }

    void UpdateWaveTimerUI()
    {
        if (waveTimerText != null)
        {
            if (!spawningWave)
            {
                waveTimerText.text = Mathf.CeilToInt(waveCountdown).ToString();
            }
            else
            {
                waveTimerText.text = "";
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Count == 0)
        {
            Debug.LogWarning("Nenhum prefab de inimigo atribuído à lista enemyPrefabs!");
            return;
        }

        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject spawnedEnemy = Instantiate(enemyToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation);
        spawnedEnemy.tag = "Enemy";
    }
}