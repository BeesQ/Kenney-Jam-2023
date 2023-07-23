using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameMode
{
    SinglePlayer,
    LocalMultiplayer,
    Menu
}

public class GameManager : Singleton<GameManager>
{
    // Game Mode
    public GameMode currentGameMode;

    // Single Player
    public GameObject inScenePlayer;

    // Local Multiplayer
    public GameObject playerPrefab;
    public int numberOfPlayers;

    public Transform spawnRingCenter;
    public float spawnRingRadius;

    // Spawned Players
    private List<PlayerController> activePlayerControllers;
    private PlayerController focusedPlayerController;

    // Level
    public bool IsLevelComplete { get; private set; }


    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
        IsLevelComplete = true;
        // SetupBasedOnGameState();
    }

    private void Update()
    {
        if (IsLevelComplete)
        {
            EventManager.Instance.TriggerLevelCompleteEvent();
            IsLevelComplete = false;
        }
    }

    void SetupBasedOnGameState()
    {
        switch (currentGameMode)
        {
            case GameMode.SinglePlayer:
                SetupSinglePlayer();
                break;

            case GameMode.LocalMultiplayer:
                SetupLocalMultiplayer();
                break;

            case GameMode.Menu:
                break;

            default: break;
        }
    }

    void SetupSinglePlayer()
    {
        activePlayerControllers = new List<PlayerController>();

        if (inScenePlayer == true)
        {
            AddPlayerToActivePlayerList(inScenePlayer.GetComponent<PlayerController>());
        }

        SetupActivePlayers();
        SetupSinglePlayerCamera();
    }

    void SetupLocalMultiplayer()
    {
        if (inScenePlayer == true)
        {
            Destroy(inScenePlayer);
        }

        SpawnPlayers();

        SetupActivePlayers();
    }

    void SpawnPlayers()
    {

        activePlayerControllers = new List<PlayerController>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Vector3 spawnPosition = CalculatePositionInRing(i, numberOfPlayers);

            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity) as GameObject;
            AddPlayerToActivePlayerList(spawnedPlayer.GetComponent<PlayerController>());
        }
    }

    void AddPlayerToActivePlayerList(PlayerController newPlayer)
    {
        activePlayerControllers.Add(newPlayer);
    }

    void SetupActivePlayers()
    {
        for (int i = 0; i < activePlayerControllers.Count; i++)
        {
            activePlayerControllers[i].SetupPlayer(i);
        }
    }

    void SetupSinglePlayerCamera()
    {
        //CameraManager.Instance.SetupSinglePlayerCamera(singlePlayerCameraMode);
    }



    //Get
    public List<PlayerController> GetActivePlayerControllers()
    {
        return activePlayerControllers;
    }

    public PlayerController GetFocusedPlayerController()
    {
        return focusedPlayerController;
    }

    public int NumberOfConnectedDevices()
    {
        return InputSystem.devices.Count;
    }



   

    //Spawn Utilities

    Vector3 CalculatePositionInRing(int positionID, int numberOfPlayers)
    {
        if (numberOfPlayers == 1)
            return spawnRingCenter.position;

        float angle = (positionID) * Mathf.PI * 2 / numberOfPlayers;
        float x = Mathf.Cos(angle) * spawnRingRadius;
        float z = Mathf.Sin(angle) * spawnRingRadius;
        return spawnRingCenter.position + new Vector3(x, 0, z);
    }
}
