using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public Canvas titleScreenCanvas;
    public Canvas mainGameCanvas;
    public TimerScript timerScript;

    void Start()
    {
        if (titleScreenCanvas != null)
        {
            titleScreenCanvas.gameObject.SetActive(true);
        }
        if (timerScript != null)
        {
            timerScript.StopTimer();
        }
        if (mainGameCanvas != null)
        {
            mainGameCanvas.gameObject.SetActive(false);
        }
    }

    public void OnStartButtonPressed()
    {
        if (titleScreenCanvas != null)
        {
            titleScreenCanvas.gameObject.SetActive(false);
        }
        if (mainGameCanvas != null)
        {
            mainGameCanvas.gameObject.SetActive(true); 
        }
        if (timerScript != null)
        {
            timerScript.StartTimer();
        }
    }
}
