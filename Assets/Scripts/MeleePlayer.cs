using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleePlayer : MonoBehaviour
{

    [Header("Component References")]
    public GameObject shield;
    public GameObject weaponPosition;

    private RedPlayerInput _input;
    private Collider2D weaponCollider;

    float timer;
    float angle;
    public float shieldDelay = 3;
    public float attackDelay = 0.01f;
    private Vector2 inputMovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        _input = new RedPlayerInput();
        _input.Enable();
        _input.PlayerControls.Attack.performed += ActivateAttack;
        _input.PlayerControls.Block.performed += ActivateBlock;
        _input.PlayerControls.Movement.performed += WeaponColiderMovement;
        weaponCollider = weaponPosition.GetComponent<Collider2D>();
        shield.SetActive(false);
        weaponPosition.GetComponent<Collider2D>().enabled = false;
    }
    void WeaponColiderMovement(InputAction.CallbackContext callbackContext)
    {
        inputMovementDirection = callbackContext.ReadValue<Vector2>();

        angle = Mathf.Atan2(inputMovementDirection.y, inputMovementDirection.x) * Mathf.Rad2Deg;
        if (angle < 90 && angle > -90)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            weaponPosition.transform.rotation = transform.rotation;
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }

        weaponPosition.transform.rotation = Quaternion.Euler(weaponPosition.transform.rotation.x, weaponPosition.transform.rotation.y, angle);
    }

    void ActivateBlock(InputAction.CallbackContext callbackContext)
    { 
        shield.SetActive(true);
        Invoke("DisableBlock",shieldDelay);
    }

    void ActivateAttack(InputAction.CallbackContext callbackContext)
    {
        if (!shield.activeSelf)
        {
            weaponCollider.enabled = true;
            Invoke("DisableAttack", attackDelay);
        }
    }
    void DisableBlock()
    {
        shield.SetActive(false);
    }
    void DisableAttack()
    {
        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}