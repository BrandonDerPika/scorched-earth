using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager Instance;

    public List<Tech> techList;
    private List<ClickableObject> clickableObjects = new List<ClickableObject>();

    public delegate void TechActivatedHandler(string techName);
    public event TechActivatedHandler OnTechActivated;

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

    public void RegisterClickableObject(ClickableObject obj)
    {
        if (!clickableObjects.Contains(obj))
        {
            clickableObjects.Add(obj);
        }
    }

    public void UnregisterClickableObject(ClickableObject obj)
    {
        if (clickableObjects.Contains(obj))
        {
            clickableObjects.Remove(obj);
        }
    }

    public void ActivateTech(string techName)
    {
        foreach (Tech tech in techList)
        {
            if (tech.name == techName)
            {
                tech.isActive = true;
                NotifyClickableObjects();
                ResourceManager.Instance.UpdateResourceVisibility(techName);
                OnTechActivated?.Invoke(techName);
                Debug.Log($"Tech activated: {techName}");
                return;
            }
        }
        Debug.LogWarning($"Tech not found: {techName}");
    }

    public bool IsTechActive(string techName)
    {
        foreach (Tech tech in techList)
        {
            if (tech.name == techName && tech.isActive)
            {
                return true;
            }
        }
        return false;
    }

    public void ResetTechs()
    {
        foreach (Tech tech in techList)
        {
            tech.isActive = tech.name == "Basic";
        }
        NotifyClickableObjects();
        Debug.Log("Techs have been reset.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetTechs();
        }
    }

    private void NotifyClickableObjects()
    {
        foreach (var obj in clickableObjects)
        {
            obj.CheckTech();
        }
    }
}

[System.Serializable]
public class Tech
{
    public string name;
    public bool isActive;
}
