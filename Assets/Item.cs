using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemBuffs", order = 1)]
public class ItemBuffs : ScriptableObject
{
    public float maxHealthBuff = 0;
    public float healthBuff = 0;
    public float movementSpeedBuff = 0.0f;
    public float damageBuff = 0.0f;
    public float attackSpeedBuff = 0.0f;
}
public class Item : MonoBehaviour, IConsumable
{
    public ItemBuffs itemBuffs;

    public void ApplyEffects(PlayerStats stats)
    {
        stats.AddMaxHealth(itemBuffs.maxHealthBuff);
        stats.AddHealth(itemBuffs.healthBuff);
        stats.AddMovementSpeed(itemBuffs.movementSpeedBuff);
        stats.AddDamage(itemBuffs.damageBuff);
        stats.AddAttackSpeed(itemBuffs.attackSpeedBuff);
        
        Destroy(this.gameObject);
    }
}
