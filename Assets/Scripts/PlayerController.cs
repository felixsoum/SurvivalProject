using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sheathedMoveForce = 3;
    public float unsheathedMoveForce = 2;
    public float meshLerpSpeed = 0.333f;
    public float groundDistance = 0.1f;
    public float extraGravity = 100;
    public GameObject mesh;

    Animator animator;
    PlayerAnimationEvents playerAnimationEvents;
    bool isEquipSheathed = true;
    bool isMovementReady = true;

    new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = mesh.GetComponent<Animator>();
        playerAnimationEvents = mesh.GetComponent<PlayerAnimationEvents>();
    }

    void Start()
    {
        playerAnimationEvents.OnMovementReady += OnMovementReady;
    }

    private void OnMovementReady()
    {
        isMovementReady = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Sheath"))
        {
            isEquipSheathed = !isEquipSheathed;
            animator.SetBool("isEquipSheathed", isEquipSheathed);
        }

        if (Input.GetMouseButtonDown(0) && !isEquipSheathed)
        {
            animator.SetTrigger("swing");
            isMovementReady = false;
            Hitbox[] hitboxes = GetComponentsInChildren<Hitbox>(true);
            foreach (var hitbox in hitboxes)
            {
                hitbox.ClearHurtGroup();
            }
        }

    }

    void FixedUpdate()
    {
        if (isMovementReady)
        {
            ApplyMovementInput();
        }

        ApplyFakeGravity();

    }

    void ApplyMovementInput()
    {
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;

        Vector3 move = cameraRight.normalized * Input.GetAxis("Horizontal") + cameraForward.normalized * Input.GetAxis("Vertical");
        float moveForce = isEquipSheathed ? sheathedMoveForce : unsheathedMoveForce;
        rigidbody.AddForce(move * moveForce, ForceMode.VelocityChange);


        if (move.magnitude > 0.1)
        {
            mesh.transform.forward = Vector3.Lerp(mesh.transform.forward, move.normalized, meshLerpSpeed);
        }
    }

    void ApplyFakeGravity()
    {
        if (!IsGrounded())
        {
            rigidbody.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
        }
    }

    bool IsGrounded()
    {
        Vector3 groundOrigin = transform.position + Vector3.up * groundDistance;
        Vector3 ray = Vector3.down *  groundDistance *2;
        var hits = Physics.RaycastAll(groundOrigin, Vector3.down, groundDistance * 2);
        Debug.DrawRay(groundOrigin, ray, Color.red);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Player")
            {
                continue;
            }
            return true;
        }
        return false;
    }
}
