using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad;
    public Canvas canvasToHide;
    public GameObject objectToShow;

    private TimerScript timerScript;

    void Start()
    {
        timerScript = FindObjectOfType<TimerScript>();
        PollutionManager.Instance.OnMilestoneReached += HandleMilestoneReached;
    }

    void HandleMilestoneReached(float milestone)
    {
        if (milestone == 20000f)
        {
            if (timerScript != null)
            {
                timerScript.StopTimer();
            }
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        Debug.Log("Switching to scene: " + sceneToLoad);
        if (canvasToHide != null)
        {
            canvasToHide.enabled = false;
        }

        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }

        SceneManager.LoadScene(sceneToLoad);
    }

    void OnDestroy()
    {
        if (PollutionManager.Instance != null)
        {
            PollutionManager.Instance.OnMilestoneReached -= HandleMilestoneReached;
        }
    }
}
