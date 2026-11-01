using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public enum ResourceType { Grain, Coal, Iron, Oil }
    public ResourceType resourceType;
    public string requiredTech;
    private bool hasBeenCollected = false;

    void Start()
    {
        gameObject.SetActive(false);
        CheckTech();
        TechManager.Instance.RegisterClickableObject(this);
    }

    void OnDestroy()
    {
        if (TechManager.Instance != null)
        {
            TechManager.Instance.UnregisterClickableObject(this);
        }
    }

    void OnMouseDown()
    {
        if (!hasBeenCollected) 
        {
            gameObject.SetActive(false);
            ResourceManager.Instance.AddResource(resourceType, 1); 
            ResourceSoundManager.Instance.PlaySound(resourceType); 
            hasBeenCollected = true; 
        }
    }

    public void CheckTech()
    {
        if (TechManager.Instance.IsTechActive(requiredTech) && !hasBeenCollected)
        {
            gameObject.SetActive(true);
        }
    }
}
