using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isRunning;

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            //calc time passed since start
            float t = Time.time - startTime;

            int minutes = ((int)t / 60);
            int seconds = ((int)t % 60);
            int milliseconds = (int)((t * 1000) % 1000);

            // Update the timer text
            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
        GameManager.TimerValue = Time.time - startTime;
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        timerText.text = "00:00:000";
    }
}
