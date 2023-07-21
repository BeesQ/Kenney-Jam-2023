using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayHoverSound();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayClickSound();
    }
}
