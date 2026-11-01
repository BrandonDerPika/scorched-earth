using System.Collections.Generic;
using UnityEngine;

public class TechActivator : MonoBehaviour
{
    public string techName;
    public List<Tree> treesToDisable;
    public List<Factory> factoriesToEnable;

    void Start()
    {
        TechManager.Instance.OnTechActivated += HandleTechActivated;
    }

    void OnDestroy()
    {
        if (TechManager.Instance != null)
        {
            TechManager.Instance.OnTechActivated -= HandleTechActivated;
        }
    }

    void HandleTechActivated(string activatedTechName)
    {
        if (activatedTechName == techName)
        {
            foreach (Tree tree in treesToDisable)
            {
                tree.SetActive(false);
            }

            foreach (Factory factory in factoriesToEnable)
            {
                factory.SetActive(true);
            }
        }
    }
}
