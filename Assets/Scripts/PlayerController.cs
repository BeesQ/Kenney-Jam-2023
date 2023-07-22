using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
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
