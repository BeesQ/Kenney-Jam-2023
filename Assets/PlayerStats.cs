using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100;
    
    [SerializeField]
    private float _health = 100;
    
    [SerializeField]
    private float _movementSpeed = 10.0f;
    
    [SerializeField]
    private float _damage = 10.0f;
    
    [SerializeField]
    private float _attackSpeed = 2.0f;

    public void AddHealth(float amount)
    {
        _health += amount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void AddMaxHealth(float amount)
    {
        _maxHealth += amount;
    }

    public void AddMovementSpeed(float amount)
    {
        _movementSpeed += amount;
    }

    public void AddDamage(float amount)
    {
        _damage += amount;
    }

    public void AddAttackSpeed(float amount)
    {
        _attackSpeed += amount;
    }
    
}
