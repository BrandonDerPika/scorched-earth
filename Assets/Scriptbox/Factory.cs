using UnityEngine;

public class Factory : MonoBehaviour
{
    public float increaseRate = 0.1f;
    private bool isActive = true;

    void Start()
    {
        UpdateState();
    }

    void Update()
    {
        if (isActive)
        {
            PollutionManager.Instance.AddPollutionIncreaseModifier(increaseRate * Time.deltaTime);
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
