using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetUIElement;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetUIElement != null)
        {
            targetUIElement.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetUIElement != null)
        {
            targetUIElement.SetActive(false);
        }
    }
}
