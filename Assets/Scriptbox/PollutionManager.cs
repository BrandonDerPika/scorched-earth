using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionManager : MonoBehaviour
{
    public static PollutionManager Instance;

    public float pollutionLevel;
    public float pollutionDecreaseRate = 1f; 
    public Slider pollutionSlider; 
    public GameObject resetGameObject; 

    private float pollutionIncreaseRate = 0f;
    private float pollutionDecreaseModifiers = 0f;
    private List<float> milestones = new List<float> { 150f, 300f, 600f, 2000f, 8000f, 13000f, 15000f, 19000f, 20000f };
    private int nextMilestoneIndex = 0;
    private const float maxPollution = 20000f; 

    public event Action<float> OnMilestoneReached;
    public event Action<float> OnMilestoneDecreased; 

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

    void Start()
    {
        pollutionSlider.maxValue = maxPollution;
        pollutionSlider.minValue = 0f; 
    }

    void Update()
    {
        UpdatePollution();
        CheckMilestones();
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed.");
            ResetPollution();
            ResetEarthFace();
        }
    }

    void UpdatePollution()
    {
        pollutionLevel += (pollutionIncreaseRate - (pollutionDecreaseRate + pollutionDecreaseModifiers)) * Time.deltaTime;
        pollutionLevel = Mathf.Clamp(pollutionLevel, 0f, maxPollution);
    }

    void CheckMilestones()
    {
        if (nextMilestoneIndex < milestones.Count && pollutionLevel >= milestones[nextMilestoneIndex])
        {
            TriggerMilestone(milestones[nextMilestoneIndex]);
            nextMilestoneIndex++;
        }
    }

    void TriggerMilestone(float milestone)
    {
        OnMilestoneReached?.Invoke(milestone);
    }

    public void AddPollutionIncreaseModifier(float amount)
    {
        pollutionIncreaseRate += amount;
    }

    public void RemovePollutionIncreaseModifier(float amount)
    {
        pollutionIncreaseRate -= amount;
    }

    public void AddPollutionDecreaseModifier(float amount)
    {
        pollutionDecreaseModifiers += amount;
    }

    public void RemovePollutionDecreaseModifier(float amount)
    {
        pollutionDecreaseModifiers -= amount;
    }

    public void ResetPollution()
    {
        pollutionLevel = 0f;
        pollutionIncreaseRate = 0f;
        pollutionDecreaseModifiers = 0f; 
        pollutionSlider.value = pollutionLevel;
        nextMilestoneIndex = 0;

        if (resetGameObject != null)
        {
            resetGameObject.SetActive(true);
        }

        Debug.Log("Pollution and milestones have been reset.");
    }

    void ResetEarthFace()
    {
        EarthFace earthFace = FindObjectOfType<EarthFace>();
        if (earthFace != null)
        {
            Debug.Log("ResetAllFaceGroups called on EarthFace.");
            earthFace.ResetAllFaceGroups();
        }
        else
        {
            Debug.LogError("EarthFace component not found.");
        }
    }

    void UpdateUI()
    {
        pollutionSlider.value = pollutionLevel;
    }
}
