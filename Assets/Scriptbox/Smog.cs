using UnityEngine;
using UnityEngine.UI;

public class Smog : MonoBehaviour
{
    public float speed = 0.5f;
    public float maxDistance = 100f;
    public float minDistance = -100f;
    public RawImage smogImage; 
    public float fadeInStartMilestone = 1000f;
    public float fadeDuration = 2f;

    private RectTransform smogRectTransform;
    private float imageWidth;
    private CanvasGroup canvasGroup;
    private bool isFadingIn = false;

    void Start()
    {
        if (smogImage != null)
        {
            smogRectTransform = smogImage.GetComponent<RectTransform>();
            imageWidth = smogRectTransform.rect.width;
            canvasGroup = smogImage.GetComponent<CanvasGroup>();

            if (canvasGroup == null)
            {
                canvasGroup = smogImage.gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f;

            PollutionManager.Instance.OnMilestoneReached += HandleMilestoneReached;
        }
        else
        {
            Debug.LogError("Smog image is not assigned.");
        }
    }

    void Update()
    {
        if (smogRectTransform != null)
        {
            smogRectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);

            //check if the image has moved past max distance to the right
            if (smogRectTransform.anchoredPosition.x > maxDistance)
            {
                //move it back
                smogRectTransform.anchoredPosition = new Vector2(minDistance - imageWidth, smogRectTransform.anchoredPosition.y);
            }

            //check if the image has moved past max distance to the left
            if (smogRectTransform.anchoredPosition.x < minDistance - imageWidth)
            {
                //move it back
                smogRectTransform.anchoredPosition = new Vector2(maxDistance, smogRectTransform.anchoredPosition.y);
            }

            if (isFadingIn)
            {
                FadeIn();
            }
        }
    }

    void HandleMilestoneReached(float milestone)
    {
        if (milestone >= fadeInStartMilestone)
        {
            isFadingIn = true;
        }
    }

    void FadeIn()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            if (canvasGroup.alpha >= 1f)
            {
                canvasGroup.alpha = 1f;
                isFadingIn = false;
            }
        }
    }

    void OnDestroy()
    {
        if (PollutionManager.Instance != null)
        {
            PollutionManager.Instance.OnMilestoneReached -= HandleMilestoneReached;
        }
    }
}
