using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayEnter()
    {
        animator.SetTrigger("Enter");
    }

    public IEnumerator PlayExit()
    {
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(0.5f); // animation length
    }
}
