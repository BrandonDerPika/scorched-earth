using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostDisplay : MonoBehaviour
{
    public Text grainText;
    public Text coalText;
    public Text ironText;
    public Text oilText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateCosts(List<TechButton.ResourceCost> costs)
    {
        grainText.text = "";
        coalText.text = "";
        ironText.text = "";
        oilText.text = "";

        //update text boxes based on the costs
        foreach (var cost in costs)
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
