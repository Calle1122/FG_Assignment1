using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMe : MonoBehaviour
{
    public void StartScale(float splashRadius)
    {
        StartCoroutine(BlastScale(transform, splashRadius));
    }

    private IEnumerator BlastScale(Transform objectToScale, float splashRadius)
    {
        float percent = 0f;

        Vector3 startScale = objectToScale.localScale;
        Vector3 endScale = objectToScale.localScale * (splashRadius * 2);
        
        while (percent < 1f)
        {
            percent += Time.deltaTime * 5f;

            objectToScale.transform.localScale = Vector3.Lerp(startScale,
                endScale, percent);
            
            yield return new WaitForEndOfFrame();
            
        }

        objectToScale.transform.localScale = startScale * splashRadius * 2;
    }
}
