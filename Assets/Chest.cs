using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool opened = false;

    [SerializeField] private Sprite _openedChestSprite;
    [SerializeField] private SpriteRenderer _body;
    
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
        
    }
}
