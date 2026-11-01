using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public string worldName;
    public List<ResourceRequirement> resourceRequirements;
    public GameObject blockingObject;
    public AudioClip purchaseSfx;
    public AudioClip cannotPurchaseSfx;
    public AudioSource audioSource;

    private bool isPurchased = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnMouseDown()
    {
        if (!isPurchased)
        {
            if (CanPurchase())
            {
                PurchaseWorld();
                PlaySound(purchaseSfx);
            }
            else
            {
                PlaySound(cannotPurchaseSfx);
            }
        }
    }

    private bool CanPurchase()
    {
        foreach (var requirement in resourceRequirements)
        {
            if (ResourceManager.Instance.GetResourceAmount(requirement.resourceType) < requirement.amount)
            {
                return false;
            }
        }
        return true;
    }

    private void PurchaseWorld()
    {
        foreach (var requirement in resourceRequirements)
        {
            ResourceManager.Instance.DeductResource(requirement.resourceType, requirement.amount);
        }

        isPurchased = true;
        if (blockingObject != null)
        {
            blockingObject.SetActive(false);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

[System.Serializable]
public class ResourceRequirement
{
    public ClickableObject.ResourceType resourceType;
    public int amount;
}
