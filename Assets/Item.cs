using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;


public class Item : MonoBehaviour, IConsumable
{
    [SerializeField]
    public float maxHealthBuff = 0;
    public float healthBuff = 0;
    public float movementSpeedBuff = 0.0f;
    public float damageBuff = 0.0f;
    public float attackSpeedBuff = 0.0f;

    public void ApplyEffects(PlayerStats stats)
    {
        stats.AddMaxHealth(maxHealthBuff);
        stats.AddHealth(healthBuff);
        stats.AddMovementSpeed(movementSpeedBuff);
        stats.AddDamage(damageBuff);
        stats.AddAttackSpeed(attackSpeedBuff);
        
        Destroy(this.gameObject);
    }
}
