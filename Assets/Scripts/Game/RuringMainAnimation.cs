using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RuringMainAnimation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void FailMotion()
    {
        animator.SetBool("Faint", true);
        StartCoroutine(ReturnAnim());
    }

    IEnumerator ReturnAnim()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("Faint", false);
    }
}
