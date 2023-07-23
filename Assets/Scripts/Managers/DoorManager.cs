using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManager : MonoBehaviour
{
    public Tilemap tilemap;

    public List<TileBase> newTile;

    [SerializeField] bool shouldOpenDoor = true;
    [SerializeField] bool isLastLevel = false;
    [SerializeField] Vector2Int doorOffset = Vector2Int.zero;
    [SerializeField] Transform playerSpawnPoint = null;
    [SerializeField] PolygonCollider2D cameraBounds = null;
    
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject nextLevel;

    PlayerController[] players = null;

    private bool canEndLevel = false;

    private void Start()
    {
        EventManager.Instance.OnLevelCompleteEvent += OpenTheDoor;
        players = FindObjectsOfType<PlayerController>();
    }

    public void OpenTheDoor()
    {
        canEndLevel = true;
        if (shouldOpenDoor == false) { return; }

        var one = new Vector3Int(doorOffset.x - 1, doorOffset.y, 0);
        var two = new Vector3Int(doorOffset.x, doorOffset.y, 0);
        tilemap.SetTile(one, newTile[0]);

        if (newTile.Count == 1) { return; }
        tilemap.SetTile(two, newTile[1]);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isLastLevel && other.collider.CompareTag("Player"))
        {
            SceneManager.Instance.LoadNextScene();
        }
        if (canEndLevel && other.collider.CompareTag("Player"))
        {
            if (nextLevel != null)
            {
                nextLevel.SetActive(true);   
            }
            currentLevel.SetActive(false);
            
            //SceneManager.Instance.LoadNextScene();
            foreach (var player in players)
            {
                player.transform.position = playerSpawnPoint.position;
            }

            CameraManager.Instance.Confiner2D.m_BoundingShape2D = cameraBounds;
        }
    }
}
