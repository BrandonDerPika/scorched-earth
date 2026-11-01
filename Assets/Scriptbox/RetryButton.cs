using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RetryButton : MonoBehaviour
{
    public string sceneToLoad;

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
