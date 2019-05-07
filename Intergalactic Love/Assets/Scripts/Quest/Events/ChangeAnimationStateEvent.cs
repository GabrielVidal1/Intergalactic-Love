using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationStateEvent : QuestEvent
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Execute()
    {
        animator.SetTrigger("StateChange");
        yield break;
    }
}
