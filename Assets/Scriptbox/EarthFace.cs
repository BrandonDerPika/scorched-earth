using System.Collections.Generic;
using UnityEngine;

public class EarthFace : MonoBehaviour
{
    [System.Serializable]
    public class FaceGroup
    {
        public List<GameObject> faces;
        public int currentFaceIndex = 0;
    }

    public List<FaceGroup> faceGroups;

    void Start()
    {
        PollutionManager.Instance.OnMilestoneReached += HandleMilestoneReached;
        PollutionManager.Instance.OnMilestoneDecreased += HandleMilestoneDecreased;
        UpdateAllFaceGroups();
    }

    void HandleMilestoneReached(float milestone)
    {
        foreach (var group in faceGroups)
        {
            //hide current face
            if (group.faces != null && group.currentFaceIndex < group.faces.Count)
            {
                group.faces[group.currentFaceIndex]?.SetActive(false);
            }

            //show next face
            group.currentFaceIndex++;
            if (group.faces != null && group.currentFaceIndex < group.faces.Count)
            {
                group.faces[group.currentFaceIndex]?.SetActive(true);
            }
        }
    }

    void HandleMilestoneDecreased(float milestone)
    {
        foreach (var group in faceGroups)
        {
            //dide current face
            if (group.faces != null && group.currentFaceIndex < group.faces.Count)
            {
                group.faces[group.currentFaceIndex]?.SetActive(false);
            }

            //show previous face
            group.currentFaceIndex--;
            if (group.faces != null && group.currentFaceIndex >= 0 && group.currentFaceIndex < group.faces.Count)
            {
                group.faces[group.currentFaceIndex]?.SetActive(true);
            }
        }
    }

    void UpdateAllFaceGroups()
    {
        foreach (var group in faceGroups)
        {
            if (group.faces != null)
            {
                for (int i = 0; i < group.faces.Count; i++)
                {
                    group.faces[i]?.SetActive(i == group.currentFaceIndex);
                }
            }
        }
    }

    public void ResetAllFaceGroups()
    {
        foreach (var group in faceGroups)
        {
            if (group.faces != null && group.faces.Count > 0)
            {
                foreach (var face in group.faces)
                {
                    if (face != null)
                    {
                        face.SetActive(false);
                    }
                }

                group.currentFaceIndex = 0;
                group.faces[group.currentFaceIndex]?.SetActive(true);
            }
        }
    }

    void OnDestroy()
    {
        if (PollutionManager.Instance != null)
        {
            PollutionManager.Instance.OnMilestoneReached -= HandleMilestoneReached;
            PollutionManager.Instance.OnMilestoneDecreased -= HandleMilestoneDecreased;
        }
    }
}
