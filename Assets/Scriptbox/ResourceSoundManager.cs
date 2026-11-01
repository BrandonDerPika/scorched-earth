using UnityEngine;

public class ResourceSoundManager : MonoBehaviour
{
    public static ResourceSoundManager Instance;

    public AudioClip grainSound;
    public AudioClip coalSound;
    public AudioClip ironSound;
    public AudioClip oilSound;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(ClickableObject.ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ClickableObject.ResourceType.Grain:
                audioSource.PlayOneShot(grainSound);
                break;
            case ClickableObject.ResourceType.Coal:
                audioSource.PlayOneShot(coalSound);
                break;
            case ClickableObject.ResourceType.Iron:
                audioSource.PlayOneShot(ironSound);
                break;
            case ClickableObject.ResourceType.Oil:
                audioSource.PlayOneShot(oilSound);
                break;
        }
    }
}
