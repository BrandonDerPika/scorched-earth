using UnityEngine;
using UnityEngine.UI;

public class HideOnKeyPress : MonoBehaviour
{
    public GameObject objectToHide;
    public Canvas canvasToShow; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (objectToHide != null)
            {
                objectToHide.SetActive(false);
            }

            if (canvasToShow != null)
            {
                canvasToShow.enabled = true;
            }
        }
    }
}
