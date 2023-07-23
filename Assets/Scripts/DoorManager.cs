using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManager : MonoBehaviour
{
    public Tilemap tilemap;

    public List<TileBase> newTile;

    [SerializeField] bool shouldOpenDoor = true;
    [SerializeField] Vector2Int doorOffset = Vector2Int.zero;

    private bool canEndLevel = false;

    private void Start()
    {
        EventManager.Instance.OnLevelCompleteEvent += OpenTheDoor;
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
        if (canEndLevel && other.collider.CompareTag("Player"))
        {
            SceneManager.Instance.LoadNextScene();
        }
    }
}
