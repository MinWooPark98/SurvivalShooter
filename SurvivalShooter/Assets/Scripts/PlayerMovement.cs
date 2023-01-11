using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField]
    private float speed = 20f;
    private Rigidbody rb;
    private PlayerInput input;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();

        var inputMove = new Vector3(input.animMoveX, 0, input.animMoveY);
        animator.SetFloat("Move", inputMove.magnitude);
    }

    private void Move()
    {
        var dirX = Camera.main.transform.right;
        dirX.y = 0f;
        dirX.Normalize();

        var dirY = Camera.main.transform.forward;
        dirY.y = 0f;
        dirY.Normalize();

        direction = dirX * input.moveX + dirY * input.moveY;

        direction.Normalize();
        rb.velocity = direction * speed;
    }

    public void Rotate()
    {
        var ray = Camera.main.ScreenPointToRay(input.mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, (int)LayerMask.GetMask("Mouse")))
        {
            var lookDir = hit.point - transform.position;
            lookDir.y = 0f;
            lookDir.Normalize();
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
