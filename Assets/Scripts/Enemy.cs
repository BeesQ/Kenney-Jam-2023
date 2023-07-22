using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _health = 10.0f;
    
    [SerializeField]
    private ColorClass _color;
    
    
    public void Damage(float amount, ColorClass col)
    {
        if(col != _color) return;
        
        _health -= amount;
    }
}
