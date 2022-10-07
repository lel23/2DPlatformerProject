using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = new Color(200, 0, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.color = new Color(155, 0, 0);
    }
}