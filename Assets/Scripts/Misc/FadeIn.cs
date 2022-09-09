using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public CanvasGroup fadeGroup;
    public bool creditsPassed;
    public LayerMask isCreditsCollider;
    public Transform creditCheck;
    public float creditCheckRadius = 0.1f;

    void Start()
    {
        if (creditCheckRadius != 0.1f)
        {
            creditCheckRadius = 0.1f;
        }
    }

    private void Update()
    {
        creditsPassed = Physics2D.OverlapCircle(creditCheck.position, creditCheckRadius, isCreditsCollider);
        
        if (creditsPassed)
        {
            if (fadeGroup.alpha < 1)
            {
                fadeGroup.alpha += Time.deltaTime / 3;

                if (fadeGroup.alpha >= 1)
                {
                    return;
                }
            }
        }
        else
        {
            if (fadeGroup.alpha > 0)
            {
                fadeGroup.alpha = 0;
            }
        }
    }
}
