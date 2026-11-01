using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void ResetScene()
    {
        
    }
}
