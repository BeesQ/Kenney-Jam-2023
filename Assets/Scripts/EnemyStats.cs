using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth => _maxHealth;
    
    [SerializeField]
    private float _health = 100;
    public float Health => _health;
    
    [SerializeField]
    private float _movementSpeed = 10.0f;
    public float MovementSpeed => _movementSpeed;
    
    [SerializeField]
    private float _damage = 10.0f;

    public float Damage => _damage;

    [SerializeField]
     private float _attackSpeed = 0.5f;
     public float AttackSpeed => _attackSpeed;

    [SerializeField]
    private float _keepingDistanceRange = 2.0f;
    public float KeepingDistanceRange => _keepingDistanceRange;

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

    /*public void AddAttackSpeed(float amount)
    {
        _attackSpeed += amount;
    }*/
    public void AddKeepingDistanceRange(float amount)
    {
        _keepingDistanceRange += amount;
    }

}
