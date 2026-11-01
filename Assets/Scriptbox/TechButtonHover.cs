using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class TechButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject costPanel;
    public Text grainText;
    public Text coalText;
    public Text ironText;
    public Text oilText;

    private TechButton techButton;

    void Start()
    {
        techButton = GetComponent<TechButton>();
        if (costPanel != null)
        {
            costPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (costPanel != null)
        {
            UpdateCostPanel();
            costPanel.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (costPanel != null)
        {
            costPanel.SetActive(false);
        }
    }

    private void UpdateCostPanel()
    {
        if (costPanel != null)
        {
            grainText.text = "";
            coalText.text = "";
            ironText.text = "";
            oilText.text = "";

            foreach (var cost in techButton.costs)
            {
                switch (cost.resourceType)
                {
                    case ClickableObject.ResourceType.Grain:
                        grainText.text = cost.amount.ToString();
                        break;
                    case ClickableObject.ResourceType.Coal:
                        coalText.text = cost.amount.ToString();
                        break;
                    case ClickableObject.ResourceType.Iron:
                        ironText.text = cost.amount.ToString();
                        break;
                    case ClickableObject.ResourceType.Oil:
                        oilText.text = cost.amount.ToString();
                        break;
                }
            }
        }
    }
}
