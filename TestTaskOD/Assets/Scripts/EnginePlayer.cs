using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnginePlayer : MonoBehaviour {
    public Vector3 moveDir = Vector3.zero;
    public float gravity;
    public float jumpSpeed;
    private float radiusPlayer;
    CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        radiusPlayer = GetComponent<CharacterController>().radius + 0.1f;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, radiusPlayer))
        {
            moveDir = Vector3.zero;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir = new Vector3(0, jumpSpeed, 1);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveDir = new Vector3(-1, jumpSpeed, 0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveDir = new Vector3(1, jumpSpeed, 0);
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

}
