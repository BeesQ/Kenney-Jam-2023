using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float maxHealth = 10f;
    private float _health = 10.0f;
    public Slider healthSlider;

    [SerializeField]
    private ColorClass _color;

    void Start()
    {
        _health = maxHealth;
        healthSlider.maxValue = maxHealth;
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(5, _color);
        }
    }

    public void Damage(float amount, ColorClass col)
    {
        if(col != _color) return;
        
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
}
