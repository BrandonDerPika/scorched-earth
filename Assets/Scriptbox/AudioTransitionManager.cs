using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioTransitionManager : MonoBehaviour
{
    public AudioSource backgroundAudio1;
    public AudioSource backgroundAudio2;
    public AudioSource backgroundAudio3;

    private Dictionary<float, Action> milestoneActions;
    private bool isFadingOutAudio1 = false;
    private bool isFadingInAudio2 = false;
    private bool isFadingInAudio3 = false;

    private void Start()
    {
        if (PollutionManager.Instance != null)
        {
            PollutionManager.Instance.OnMilestoneReached += HandleMilestoneReached;
        }

        milestoneActions = new Dictionary<float, Action>
        {
            { 150f, () => StartFadeOut(backgroundAudio1) },
            { 300f, () => StartFadeOut(backgroundAudio1) },
            { 600f, () => {
                StartFadeOut(backgroundAudio1);
                StartFadeIn(backgroundAudio2);
            }},
            { 2000f, () => StartFadeOut(backgroundAudio1) },
            { 8000f, () => StartFadeIn(backgroundAudio3) },
            { 13000f, () => {
                StartFadeIn(backgroundAudio2);
                StartFadeIn(backgroundAudio3);
            }},
            { 15000f, () => StartFadeIn(backgroundAudio3) },
            { 19000f, () => StartFadeIn(backgroundAudio3) },
            { 20000f, () => StartFadeIn(backgroundAudio3) },
        };
    }

    private void Update()
    {
        if (isFadingOutAudio1)
        {
            FadeOut(backgroundAudio1);
        }

        if (isFadingInAudio2)
        {
            FadeIn(backgroundAudio2);
        }

        if (isFadingInAudio3)
        {
            FadeIn(backgroundAudio3);
        }
    }

    private void HandleMilestoneReached(float milestone)
    {
        if (milestoneActions.ContainsKey(milestone))
        {
            milestoneActions[milestone]?.Invoke();
        }
    }

    private void StartFadeOut(AudioSource audioSource)
    {
        if (audioSource == backgroundAudio1)
        {
            isFadingOutAudio1 = true;
        }
    }

    private void StartFadeIn(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (audioSource == backgroundAudio2)
        {
            isFadingInAudio2 = true;
        }
        else if (audioSource == backgroundAudio3)
        {
            isFadingInAudio3 = true;
        }
    }

    private void FadeOut(AudioSource audioSource)
    {
        if (audioSource.volume > 0)
        {
            audioSource.volume -= Time.deltaTime / 10f;
        }
        else
        {
            audioSource.volume = 0;
            audioSource.Stop();

            if (audioSource == backgroundAudio1)
            {
                isFadingOutAudio1 = false;
            }
        }
    }

    private void FadeIn(AudioSource audioSource)
    {
        if (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / 10f;
        }
        else
        {
            audioSource.volume = 1;

            if (audioSource == backgroundAudio2)
            {
                isFadingInAudio2 = false;
            }
            else if (audioSource == backgroundAudio3)
            {
                isFadingInAudio3 = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (PollutionManager.Instance != null)
        {
            PollutionManager.Instance.OnMilestoneReached -= HandleMilestoneReached;
        }
    }
}
