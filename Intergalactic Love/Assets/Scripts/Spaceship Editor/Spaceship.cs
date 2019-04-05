using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : SpaceshipPart
{
    public void SaveSpaceship()
    {

    }

    public void LoadSpaceship()
    {

    }

}

[System.Serializable]
public class SerializedSpaceship
{
    public SerializedSpaceshipPart[] parts;

    [System.Serializable]
    public class SerializedSpaceshipPart
    {
        public Vector3 position;
        public Quaternion rotation;

        public SerializedSpaceshipPart[] parts;
    }
}