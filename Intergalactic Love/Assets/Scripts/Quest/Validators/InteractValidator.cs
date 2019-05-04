using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractValidator : QuestValidator
{
    public bool hasBeenInteracted = false;

    public override bool CanPartBeValidated()
    {
        return false;
    }

    public override Quest.ValidatorType GetValidatorType()
    {
        return Quest.ValidatorType.Interact;
    }

    public override void ValidatePart()
    { }
}
