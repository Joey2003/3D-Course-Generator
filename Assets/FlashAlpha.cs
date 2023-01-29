using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAlpha : MonoBehaviour
{

    public CanvasGroup cg;
    public float alphaChange, alpha = 0;
    public float currentAlpha;
    // Start is called before the first frame update
    void Start()
    {
        //prevAlpha = cg.alpha;
        cg.alpha = 0;
        alpha = 0;
        currentAlpha = cg.alpha;
    }

    // Update is called once per frame
    void Update()
    {
        //prevAlpha = cg.alpha;
        alpha += alphaChange;
        cg.alpha = Mathf.Clamp(alpha, 0, 1);

        if (alpha >= 1) { alphaChange *= -1; }
        else if (alpha <= 0) { alphaChange *= -1; }
    }
}
