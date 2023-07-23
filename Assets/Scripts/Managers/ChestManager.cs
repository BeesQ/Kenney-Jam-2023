using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    [SerializeField] private GameObject _chestPrefab;

    [SerializeField] private WaveSystem _waveSystem;
    [SerializeField] private Transform _chestSpawnPosition;

    private void Start()
    {
        _waveSystem.OnAllWavesEnd += WaveSystemOnOnAllWavesEnd;
    }

    private void WaveSystemOnOnAllWavesEnd(object sender, AllWavesEndEventArgs e)
    {
        Instantiate(_chestPrefab, _chestSpawnPosition.position, Quaternion.identity);
    }
}
