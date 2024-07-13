using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnimController : MonoBehaviour
{
    void OnEnable()
    {
        //StartCoroutine("Fade");
    }
    void OnDisable()
    {
        SpriteRenderer fanRenderer = GetComponent<SpriteRenderer>();
        fanRenderer.color = new Color(fanRenderer.color.r, fanRenderer.color.g, fanRenderer.color.b, 1);
    }

    // Referenced by animation clip event
    void OnAnimationComplete()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
