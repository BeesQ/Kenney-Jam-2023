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

    private MeleePlayer meleePlayer;

    //private float maxHealth = 100f;
    //private float _health = 100.0f;

    [SerializeField]
    public Slider healthSlider;
    public PlayerStats stats;


    void Start()
    {
        if(_colorClass == ColorClass.RED)
        meleePlayer = GetComponent<MeleePlayer>();

        //_health = stats.MaxHealth;
        healthSlider.maxValue = stats.MaxHealth;
        healthSlider.value = stats.Health;

        var sliderChild = healthSlider.transform.GetChild(1);
        switch (_colorClass)
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
        bool isBlocked = false;
        if(_colorClass == ColorClass.BLUE) isBlocked = false;
        if (_colorClass == ColorClass.RED) isBlocked = meleePlayer.isBlocking;
        if (!isBlocked)
        {
            //_health -= amount;
            stats.AddHealth(-amount);
            //stats.Health -= amount;
            healthSlider.value = stats.Health;
            SoundManager.Instance.PlayClickSound();

            if (stats.Health <= 0)
            {
                // Play some sound then kill
                Kill();
            }
        }
    }

    public void Kill()
    {
        SceneManager.Instance.LoadGameOverScene();
        gameObject.SetActive(false);
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
        bool isBlocked = false;
        if (_colorClass == ColorClass.BLUE) isBlocked = false;
        if (_colorClass == ColorClass.RED) isBlocked = meleePlayer.isBlocking;
        if (!isBlocked)
        {
            if (col.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(stats.Damage, _colorClass);
            }
        }
        
        if (col.gameObject.TryGetComponent(out IConsumable consumable))
        {
            SoundManager.Instance.PlayPickUpSound();
            consumable.ApplyEffects(stats);
            healthSlider.maxValue = stats.MaxHealth;
            healthSlider.value = stats.Health;
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
