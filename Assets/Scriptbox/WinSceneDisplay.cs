using UnityEngine;
using UnityEngine.UI;

public class WinSceneDisplay : MonoBehaviour
{
    public Text lastTimeText;
    public Text bestTimeText; 

    private const string LastTimeKey = "LastTime";
    private const string BestTimeKey = "BestTime";

    void Start()
    {
        float currentTimerValue = GameManager.TimerValue;

        SaveLastTime(currentTimerValue);
        float bestTime = LoadBestTime();

        if (currentTimerValue < bestTime || bestTime == 0)
        {
            SaveBestTime(currentTimerValue);
            bestTime = currentTimerValue;
        }

        float lastTime = LoadLastTime();
        DisplayTime(lastTime, lastTimeText);
        DisplayTime(bestTime, bestTimeText);
    }

    void SaveLastTime(float time)
    {
        PlayerPrefs.SetFloat(LastTimeKey, time);
        PlayerPrefs.Save();
    }

    void SaveBestTime(float time)
    {
        PlayerPrefs.SetFloat(BestTimeKey, time);
        PlayerPrefs.Save();
    }

    float LoadLastTime()
    {
        return PlayerPrefs.GetFloat(LastTimeKey, 0);
    }

    float LoadBestTime()
    {
        return PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);
    }

    void DisplayTime(float time, Text textComponent)
    {
        int minutes = ((int)time / 60);
        int seconds = ((int)time % 60);
        int milliseconds = (int)((time * 1000) % 1000);

        textComponent.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}

