using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int grainAmount;
    public int coalAmount;
    public int ironAmount;
    public int oilAmount;

    public Text grainText;
    public Text coalText;
    public Text ironText;
    public Text oilText;

    private Dictionary<ClickableObject.ResourceType, int> resources;
    private Dictionary<ClickableObject.ResourceType, string> resourceTechs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeResources();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeResources()
    {
        resources = new Dictionary<ClickableObject.ResourceType, int>
        {
            { ClickableObject.ResourceType.Grain, grainAmount },
            { ClickableObject.ResourceType.Coal, coalAmount },
            { ClickableObject.ResourceType.Iron, ironAmount },
            { ClickableObject.ResourceType.Oil, oilAmount }
        };

        resourceTechs = new Dictionary<ClickableObject.ResourceType, string>
        {
            { ClickableObject.ResourceType.Grain, "GrainTech" },
            { ClickableObject.ResourceType.Coal, "CoalTech" },
            { ClickableObject.ResourceType.Iron, "IronTech" },
            { ClickableObject.ResourceType.Oil, "OilTech" }
        };

        UpdateAllUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetResources();
        }
    }

    public void AddResource(ClickableObject.ResourceType resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += amount;
            UpdateUI(resourceType);
        }
    }

    public void UpdateUI(ClickableObject.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ClickableObject.ResourceType.Grain:
                grainText.text = resources[resourceType].ToString();
                break;
            case ClickableObject.ResourceType.Coal:
                coalText.text = resources[resourceType].ToString();
                break;
            case ClickableObject.ResourceType.Iron:
                ironText.text = resources[resourceType].ToString();
                break;
            case ClickableObject.ResourceType.Oil:
                oilText.text = resources[resourceType].ToString();
                break;
        }
    }

    public void UpdateResourceVisibility(string techName)
    {
        foreach (var kvp in resourceTechs)
        {
            if (kvp.Value == techName)
            {
                SetResourceObjectsActive(kvp.Key, true);
            }
        }
    }

    public int GetResourceAmount(ClickableObject.ResourceType resourceType)
    {
        if (resources.ContainsKey(resourceType))
        {
            return resources[resourceType];
        }
        return 0;
    }

    public void DeductResource(ClickableObject.ResourceType resourceType, int amount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] = Mathf.Max(0, resources[resourceType] - amount);
            UpdateUI(resourceType);
        }
    }

    private void SetResourceObjectsActive(ClickableObject.ResourceType resourceType, bool isActive)
    {
        ClickableObject[] clickableObjects = FindObjectsOfType<ClickableObject>();
        foreach (var obj in clickableObjects)
        {
            if (obj.resourceType == resourceType)
            {
                obj.gameObject.SetActive(isActive);
            }
        }
    }

    private void UpdateAllUI()
    {
        resources[ClickableObject.ResourceType.Grain] = grainAmount;
        resources[ClickableObject.ResourceType.Coal] = coalAmount;
        resources[ClickableObject.ResourceType.Iron] = ironAmount;
        resources[ClickableObject.ResourceType.Oil] = oilAmount;

        UpdateUI(ClickableObject.ResourceType.Grain);
        UpdateUI(ClickableObject.ResourceType.Coal);
        UpdateUI(ClickableObject.ResourceType.Iron);
        UpdateUI(ClickableObject.ResourceType.Oil);
    }

    void ResetResources()
    {
        grainAmount = 0;
        coalAmount = 0;
        ironAmount = 0;
        oilAmount = 0;

        resources[ClickableObject.ResourceType.Grain] = grainAmount;
        resources[ClickableObject.ResourceType.Coal] = coalAmount;
        resources[ClickableObject.ResourceType.Iron] = ironAmount;
        resources[ClickableObject.ResourceType.Oil] = oilAmount;

        UpdateAllUI();

        Debug.Log("All resources have been reset and UI updated.");
    }
}
