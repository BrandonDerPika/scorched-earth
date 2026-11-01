using UnityEngine;

public class Tree : MonoBehaviour
{
    public float decreaseRate = 0.1f;
    private bool isActive = true;

    void Start()
    {
        UpdateState();
    }

    void Update()
    {
        if (isActive)
        {
            PollutionManager.Instance.AddPollutionDecreaseModifier(decreaseRate * Time.deltaTime);
        }
    }

    public void SetActive(bool active)
    {
        isActive = active;
        UpdateState();
    }

    void UpdateState()
    {
        gameObject.SetActive(isActive);
    }
}
