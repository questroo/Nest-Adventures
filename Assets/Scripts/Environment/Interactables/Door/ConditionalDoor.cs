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
    PlayerUse,
    EnemiesKilled,
}

public class ConditionalDoor : MonoBehaviour
{
    public enum MovementType
    { 
        Up, 
        Down
    };

    public MovementType movementDirection = MovementType.Up;
    public float timeToFullyMove = 4.0f;

    float t = 0.0f;
    DoorState doorState = DoorState.Locked;
    Vector3 closedPosition;
    Vector3 openPosition;

    private void Start()
    {
        closedPosition = openPosition = transform.position;
        if (movementDirection == MovementType.Up)
            openPosition.y += transform.localScale.y;
        else
            openPosition.y -= (transform.localScale.y + 0.1f);
    }

    private void Update()
    {
        if (doorState == DoorState.Opening)
        {
            t += Time.deltaTime;
            if (t >= timeToFullyMove)
            {
                t = timeToFullyMove;
                doorState = DoorState.Open;

                Move();
            }
            else
            {
                Move();
            }
        }
    }

    void Move()
    {
        transform.localPosition = Vector3.Lerp(closedPosition, openPosition, (t / timeToFullyMove));
    }

    public void Unlock()
    {
        Debug.Log("Called Unlock");
        if(doorState == DoorState.Locked)
            doorState = DoorState.Opening;
    }
}