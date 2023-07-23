using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector2 _moveDir;
    private float _speed = 10.0f;
    private float _lifetime = 5.0f;
    private float _livedFor = 0.0f;
    private ColorClass _color;
    private float _damage;
    
    private bool initialised = false;
    void Update()
    {
        if(!initialised) return;
        
        transform.position += (Vector3)_moveDir * (_speed * Time.deltaTime);
        _livedFor += Time.deltaTime;

        if (_livedFor > _lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialise(Vector2 moveDir, ColorClass color, float speed, float lifeTime, float damage)
    {
        _moveDir = moveDir;
        _speed = speed;
        _lifetime = lifeTime;
        _damage = damage;
        _color = color;

        initialised = true;
    }
    
    public void Initialise(Vector2 moveDir, ColorClass color, float damage)
    {
        _moveDir = moveDir;
        _color = color;
        _damage = damage;
        
        initialised = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent(out PlayerController playerController)) return;

        if (col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(_damage, _color);
        }
        
        Destroy(this.gameObject);
    }
}
