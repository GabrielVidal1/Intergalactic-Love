using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationStateEvent : QuestEvent
{
    private Animator animator;

    public string triggerName = "StateChange";

    private bool endAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Execute()
    {
        endAnim = false;
        animator.SetTrigger(triggerName);

        while(!endAnim)
            yield return 0;
    }

    public void EndAnimation()
    {
        endAnim = true;
    }
}
