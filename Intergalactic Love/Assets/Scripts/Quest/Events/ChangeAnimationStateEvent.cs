using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationStateEvent : QuestEvent
{
    private Animator animator;

    private bool endAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Execute()
    {
        endAnim = false;
        animator.SetTrigger("StateChange");

        while(!endAnim)
            yield return 0;
    }

    public void EndAnimation()
    {
        endAnim = true;
    }
}
