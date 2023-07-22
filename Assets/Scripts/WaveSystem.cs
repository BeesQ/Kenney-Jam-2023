using System;
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
    public class WaveSystem : MonoBehaviour
    {
        public Transform[] spawnPoints;
        [SerializeField] private Wave[] _waves;
        private float waveTimer = 0.0f;
        public Random rng;

        private void Start()
        {
            rng = new Random();
            foreach (var wave in _waves)
            {
                wave.Initialise();
            }
        }

        public void Update()
        {
            waveTimer += Time.deltaTime;

            float tmpTimer = waveTimer;
            int currentWaveIdx = 0;
            while (tmpTimer > 0.0f)
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
                        Instantiate(currentWave.enemy, spawnPoints[randomSpawnPointIdx].position, Quaternion.identity);
                    }
                }
                
                tmpTimer -= currentWave.nextWaveDelay;
                currentWave.spawnTimer += Time.deltaTime;
                currentWaveIdx += 1;
            }

        }
    }
}