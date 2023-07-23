using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private Transform objectTransform;
    private RectTransform rectTransform;

    private void Start()
    {
        objectTransform = GetComponentInParent<Transform>();
        rectTransform = GetComponent<RectTransform>();
    }
    //private void Update()
    //{
    //    float newRotationY = objectTransform.rotation.eulerAngles.y;

    //    //if (newRotationY == 180)
    //    //{
    //    //    rectTransform.rotation.eulerAngles = Quaternion.Euler(0f, newRotationY, 0f);
    //    //}

    //    //Vector3 mouseDir = transform.position.GetMouseDirectionVector();
    //    //float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
    //    rectTransform.rotation = Quaternion.Euler(0, newRotationY, 0);

    //    //if (newRotationY == 180)
    //    //{
    //    //}
    //}
}
