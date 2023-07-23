using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable 
{
    private int playerID;
    public PlayerMovement playerMovement;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private Vector2 inputMovementDirection;

    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    private string currentControlScheme;

    [SerializeField]
    private ColorClass _colorClass;
    public ColorClass ColorClass
    {
        get => _colorClass;
    }

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

    public void Damage(float amount, ColorClass col)
    {
        if (col != _color) return;

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

    public void SetupPlayer(int newPlayerID)
    {
        playerID = newPlayerID;
        currentControlScheme = playerInput.currentControlScheme;
    }

    
    void Update()
    {
        UpdatePlayerMovement();
    }

    void UpdatePlayerMovement()
    {
        playerMovement.UpdateMovementData(inputMovementDirection);
    }


    void RemoveAllBindingOverrides()
    {
        InputActionRebindingExtensions.RemoveAllBindingOverrides(playerInput.currentActionMap);
    }
    

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnablePauseMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(5, _colorClass);
        }
    }


    // Events
    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovementDirection = value.ReadValue<Vector2>();
    }

    public void OnControlsChanged()
    {

        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;

            RemoveAllBindingOverrides();
        }
    }
    
    // Get
    public int GetPlayerID()
    {
        return playerID;
    }

    public InputActionAsset GetActionAsset()
    {
        return playerInput.actions;
    }

    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }


}
