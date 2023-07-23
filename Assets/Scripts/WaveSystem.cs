using System;
using Enums;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public float duration;
        public int enemiesCounter;
        public float nextWaveDelay;
        public ColorClass color; 
        
        [HideInInspector]
        public float spawnRate = 0.0f;
        [HideInInspector]
        public float spawnTimer = 0.0f;

        public void Initialise()
        {
            spawnRate = duration / enemiesCounter;
            
            // in order to remove initial wait
            spawnTimer = spawnRate;
        }
    }

    public class AllWavesEndEventArgs: EventArgs
    {
        public float timeToComplete;
    }
    
    public class WaveSystem : MonoBehaviour
    {
        public Transform[] spawnPoints;
        [SerializeField] private Wave[] _waves;
        private float waveTimer = 0.0f;
        private Random rng;
        public event EventHandler<AllWavesEndEventArgs> OnAllWavesEnd;

        private void Start()
        {
            rng = new Random();
            foreach (var wave in _waves)
            {
                wave.Initialise();
            }

            OnAllWavesEnd += (sender, args) => { EventManager.Instance.TriggerLevelCompleteEvent(); };
        }

        public void Update()
        {
            waveTimer += Time.deltaTime;

            float tmpTimer = waveTimer;
            int currentWaveIdx = 0;
            while (tmpTimer >= 0.0f)
            {
                if(currentWaveIdx >= _waves.Length) break;

                Wave currentWave = _waves[currentWaveIdx];

                bool canWaveSpawn = currentWave.spawnTimer >= currentWave.spawnRate;
                if (canWaveSpawn)
                {
                    currentWave.spawnTimer = 0.0f;
                    if (currentWave.enemiesCounter > 0)
                    {
                        // spawn enemy
                        currentWave.enemiesCounter -= 1;

                        int randomSpawnPointIdx = rng.Next(0, spawnPoints.Length);
                        GameObject spawnedEnemy = Instantiate(currentWave.enemy, spawnPoints[randomSpawnPointIdx].position, Quaternion.identity);
                        
                        if (spawnedEnemy.TryGetComponent(out Enemy enemy))
                        {
                            enemy.SetColor(currentWave.color);
                        }
                    }
                }
                
                tmpTimer -= currentWave.nextWaveDelay;
                currentWave.spawnTimer += Time.deltaTime;
                currentWaveIdx += 1;
            }

            if (NoEnemiesLeft())
            {
                OnAllWavesEnd?.Invoke(this, new AllWavesEndEventArgs() { timeToComplete = waveTimer});
                Destroy(this.gameObject);
            }
        }

        public bool NoEnemiesLeft()
        {
            foreach (var wave in _waves)
            {
                if (wave.enemiesCounter != 0) return false;
            }

            Enemy[] enemies = FindObjectsOfType<Enemy>();

            return enemies.Length == 0;
        }
    }
}