using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    public float moveX { get; private set; }
    public float moveY { get; private set; }
    public float animMoveX { get; private set; }
    public float animMoveY { get; private set; }
    public Vector3 mousePos { get; private set; }
    public bool fire { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPause)
            return;
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        animMoveX = Input.GetAxis("Horizontal");
        animMoveY = Input.GetAxis("Vertical");
        mousePos = Input.mousePosition;
        fire = Input.GetMouseButton((int)MouseButton.LeftMouse);
    }
}
