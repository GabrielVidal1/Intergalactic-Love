using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestValidator : MonoBehaviour
{
    public abstract bool CanPartBeValidated();

    public abstract void ValidatePart();

    public abstract Quest.ValidatorType GetValidatorType();
}
