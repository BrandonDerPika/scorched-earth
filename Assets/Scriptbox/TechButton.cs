using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TechButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string techName;
    public GameObject currentUI;
    public GameObject newUI;
    public CostDisplay costDisplay;
    public GameObject targetUI;

    [System.Serializable]
    public struct ResourceCost
    {
        public ClickableObject.ResourceType resourceType;
        public int amount;
    }

    public List<ResourceCost> costs;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchUI();
        }
    }

    public void OnButtonClick()
    {
        if (CanAffordTech())
        {
            PurchaseTech();
            UpdateUI();
        }
        else
        {
            //play sound for not being able to purchase
        }
    }

    bool CanAffordTech()
    {
        foreach (var cost in costs)
        {
            if (ResourceManager.Instance.GetResourceAmount(cost.resourceType) < cost.amount)
            {
                return false;
            }
        }
        return true;
    }

    void PurchaseTech()
    {
        foreach (var cost in costs)
        {
            ResourceManager.Instance.DeductResource(cost.resourceType, cost.amount);
        }
        TechManager.Instance.ActivateTech(techName);
        //play sound effect for buying
    }

    void UpdateUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }
        if (newUI != null)
        {
            newUI.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (costDisplay != null)
        {
            costDisplay.gameObject.SetActive(true);
            costDisplay.UpdateCosts(costs);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (costDisplay != null)
        {
            costDisplay.gameObject.SetActive(false);
        }
    }

    void SwitchUI()
    {
        if (currentUI != null)
        {
            currentUI.SetActive(false);
        }
        if (targetUI != null)
        {
            targetUI.SetActive(true);
        }
    }
}
