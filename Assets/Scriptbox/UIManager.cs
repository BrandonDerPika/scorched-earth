using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ShowCanvas(Canvas canvas)
    {
        if (canvas != null)
        {
            canvas.enabled = true;
        }
        else
        {
            Debug.LogWarning("Canvas to show is not assigned.");
        }
    }
}
