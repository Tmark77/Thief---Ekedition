using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//még egy random várakozásos is menő lenne
//mínuszos várakozási idő végtelent jelent
[System.Serializable]
public class PatrolPost
{
    public Vector3 position;
    public float rotation;
    public float waitTime;

    /// <summary>
    /// Makes a new PatrolPost
    /// </summary>
    /// <param name="position">The position of the post. (where the creature will stand)</param>
    /// <param name="rotation">Use only positive values for valid rotations. Negative value means "no turn at all", so the creature will face the same diraction as it reaches the post.</param>
    /// <param name="waitTime">Set it to force the creature to stay a while at the post. You can set Infinite value.</param>
    public PatrolPost(Vector3 position, float rotation, float waitTime)
    {
        this.position = position;
        this.rotation = rotation;
        this.waitTime = waitTime;
    }

    public PatrolPost(Vector3 position, float rotation) : this(position, rotation, 0)
    {

    }

    public PatrolPost(Vector3 position) : this (position, -1, 0)
    {

    }
	
}
