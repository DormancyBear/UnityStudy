using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour {

    public SkinnedMeshRenderer blwRenderer;
    public SkinnedMeshRenderer eyeRenderer;
    public SkinnedMeshRenderer elRenderer;
    public SkinnedMeshRenderer mthRenderer;

    [Tooltip("播放一个表情动画所需时间")]
    public float animationDuration = 5;

    private float timeElapse = 0;

    #region DisstractAnim
    float[] blwWeight = new float[6]
    {
        0, 0, 0, 0, 0, 0,
    };

    float[] eyeWeight = new float[7]
    {
        11.3f, 0, 0, 0, 87.7f, 0, 25.64f
    };

    float[] elWeight = new float[7]
    {
        1, 0, 0, 0, 0, 0, 40,
    };

    float[] mthWeight = new float[6]
    {
        0, 0, 0, 0, 0, 0,
    };
    #endregion

    void Update()
    {
        timeElapse += Time.deltaTime;
        if (timeElapse > animationDuration)
        {
            timeElapse = 0;
        }
        float normalizeTime = timeElapse / animationDuration;

        for (int i = 0; i < blwWeight.Length; i++)
        {
            blwRenderer.SetBlendShapeWeight(i, Mathf.Lerp(0, blwWeight[i], normalizeTime));
        }
        
        for (int i = 0; i < eyeWeight.Length; i++)
        {
            eyeRenderer.SetBlendShapeWeight(i, Mathf.Lerp(0, eyeWeight[i], normalizeTime));
        }

        for (int i = 0; i < elWeight.Length; i++)
        {
            elRenderer.SetBlendShapeWeight(i, Mathf.Lerp(0, elWeight[i], normalizeTime));
        }

        for (int i = 0; i < mthWeight.Length; i++)
        {
            mthRenderer.SetBlendShapeWeight(i, Mathf.Lerp(0, mthWeight[i], normalizeTime));
        }
    }
}
