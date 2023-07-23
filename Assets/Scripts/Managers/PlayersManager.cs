using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    [SerializeField]
    private PlayerController _playerBlue;
    
    [SerializeField]
    private PlayerController _playerRed;

    [SerializeField]
    private PlayerController[] _players;

    public PlayerController PlayerBlue => _playerBlue;

    public PlayerController PlayerRed => _playerRed;

    public PlayerController[] Players => _players;

    private void Awake()
    {
        _players = FindObjectsOfType<PlayerController>();

        foreach (var player in _players)
        {
            switch (player.ColorClass)
            {
                case ColorClass.RED:
                    _playerRed = player;
                    break;
                case ColorClass.BLUE:
                    _playerBlue = player;
                    break;
            }
        }
    }

    public (float, PlayerController) GetClosestPlayerToPos(Vector3 position)
    {
        int closestPlayerIdx = 0;
        float closestDistance = float.MaxValue;
        int i = 0;
        foreach (var player in _players)
        {
            float dist = Vector3.Distance(position, player.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestPlayerIdx = i;
            }

            i++;
        }

        return (closestDistance, _players[closestPlayerIdx]);
    }
}
