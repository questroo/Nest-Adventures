using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Locked,
    Opening,
    Open
}
public enum ConditionType
{
    PlayerEnter,
    EnemiesKilled,
}

public class ConditionalDoor : MonoBehaviour
{
    public Vector3 upPosition;
    public Vector3 downPosition;
    public float timeToFullyMove = 4.0f;

    float t = 0.0f;
    DoorState doorState = DoorState.Locked;

    private void Update()
    {
        if (doorState == DoorState.Opening)
        {
            t += Time.deltaTime;
            if (t >= timeToFullyMove)
            {
                t = timeToFullyMove;
                doorState = DoorState.Open;

                MoveTrap();
            }
            else
            {
                MoveTrap();
            }
        }
    }

    void MoveTrap()
    {
        transform.localPosition = Vector3.Lerp(upPosition, downPosition, (t / timeToFullyMove));
    }

    public void UnlockDoor()
    {
        if(doorState == DoorState.Locked)
            doorState = DoorState.Opening;
    }
}