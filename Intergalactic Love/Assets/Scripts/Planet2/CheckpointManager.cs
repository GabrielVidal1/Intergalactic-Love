using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager
{
    private static Checkpoint lastCheckpoint;

    public static Checkpoint GetCurrentCheckpoint()
    { return lastCheckpoint; }

    public static void SetCheckpoint(Checkpoint checkpoint)
    {
        if (lastCheckpoint != null)
            lastCheckpoint.SetFlag(false);

        checkpoint.SetFlag(true);

        lastCheckpoint = checkpoint;
    }
}
