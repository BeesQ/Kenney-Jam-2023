using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Extensions;
using Unity.VisualScripting;
using UnityEngine.InputSystem;

public class ShootingPlayer : MonoBehaviour
{
    private ColorClass _color = ColorClass.BLUE;

    [SerializeField] private PlayerStats _stats;
    
    [SerializeField]
    private Transform bow;

    [SerializeField] private GameObject arrowPrefab;
    
    private BluePlayerInput _input;
    

    // Start is called before the first frame update
    void Start()
    {
        _input = new BluePlayerInput();
        _input.Enable();
        _input.PlayerControls.Attack.performed += ShootArrow;
    }

    // Update is called once per frame
    void Update()
    {
        HandleBowPosition();
        HandlePlayerRotation();
    }

    void HandleBowPosition()
    {
        Vector3 mouseDir = bow.position.GetMouseDirectionVector();
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x);
        bow.rotation = Quaternion.Euler(0.0f, 0.0f, angle * Mathf.Rad2Deg);
    }

    void HandlePlayerRotation()
    {
        Vector3 mouseDir = transform.position.GetMouseDirectionVector();
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;

        if (angle < 90 && angle > -90)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }

    void ShootArrow(InputAction.CallbackContext context)
    {
        Vector3 mouseDir = transform.position.GetMouseDirectionVector();
        GameObject arrow = Instantiate(arrowPrefab, bow.transform.position, bow.transform.rotation);
        Arrow arrowComponent = arrow.GetComponent<Arrow>();
        arrowComponent.Initialise(mouseDir, _color, _stats.Damage);
    }
}
