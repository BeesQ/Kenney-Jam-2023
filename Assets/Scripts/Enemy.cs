using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    //private float maxHealth = 10f;
    private float _health = 10.0f;

    [SerializeField]
    public Slider healthSlider;
    public EnemyStats stats;

    [SerializeField]
    private ColorClass _color;

    private float _attackCooldown = 0.0f; 

    void Start()
    {
        _health = stats.MaxHealth;
        healthSlider.maxValue = stats.MaxHealth;
        healthSlider.value = _health;

        var sliderChild = healthSlider.transform.GetChild(1);
        switch (_color)
        {
            case ColorClass.RED:
                sliderChild.GetComponent<Image>().color = new Color(255, 0, 0);
                break;

            case ColorClass.BLUE:
                sliderChild.GetComponent<Image>().color = new Color(0, 0, 255);
                break;

            default:
                sliderChild.GetComponent<Image>().color = new Color(0, 255, 0);
                break;
        }
    }

    private void Update()
    {
        _attackCooldown += Time.deltaTime;
    }

    private void  OnCollisionStay2D(Collision2D col)
    {
        if(col.collider.gameObject.CompareTag("Enemy")) return;

        bool damageOnCooldown = _attackCooldown < stats.AttackSpeed;
        if(damageOnCooldown) return;

        _attackCooldown = 0.0f;

        if (col.collider.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(stats.Damage, _color);
        }
    }

    public void Damage(float amount, ColorClass col)
    {
        if(col != _color && _color != ColorClass.NEUTRAL) return;
        
        _health -= amount;
        healthSlider.value = _health;
        SoundManager.Instance.PlayClickSound();
        
        if (_health <= 0)
        {
            // Play some sound then kill
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void SetColor(ColorClass color)
    {
        _color = color;
    }
}
