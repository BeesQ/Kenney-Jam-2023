using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Chest : MonoBehaviour
{
    private bool opened = false;

    [SerializeField] private Sprite _openedChestSprite;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private GameObject[] _itemsPrefabs;
    [SerializeField] private Transform[] positions; 
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!col.gameObject.CompareTag("Player")) return;
        if(opened) return;

        // On Chest Open
        opened = true;
        _body.sprite = _openedChestSprite;

        DropItems();
    }

    private void DropItems()
    {
        foreach (var position in positions)
        {
            GameObject randomItem = GetRandomItem();
            Instantiate(randomItem, position.position, Quaternion.identity);
        }
    }

    GameObject GetRandomItem()
    {
        Random rng = new Random();
        return _itemsPrefabs[rng.Next(0, _itemsPrefabs.Length)];
    }
}
