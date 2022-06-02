using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LevelController levelController;

    private Vector2 startPosition;
    private Vector2 targetPosition;

    private float moveTime;
    private float timeElapsed;

    private bool movingPlayer = false;

    void Start()
    {
        moveTime = levelController.startPuzzleTime;
    }

    void Update()
    {
        if (movingPlayer)
        {
            if (timeElapsed < moveTime)
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition, timeElapsed / moveTime);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0f;
                movingPlayer = false;
            }
        }
    }

    public void MovePlayer(GameObject destination)
    {
        startPosition = transform.position;
        targetPosition = new Vector2(destination.transform.position.x, transform.position.y);
        movingPlayer = true;
    }
}
