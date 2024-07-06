using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class InputManager : MonoBehaviour
{
    Movement movement;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (movement.IsRotating) return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.RotateAround(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.RotateAround(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.RotateAround(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.RotateAround(Vector3.right);
        }
    }
}
