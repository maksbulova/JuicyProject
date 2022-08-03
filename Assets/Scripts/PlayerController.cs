using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Joystick joystick;
    [SerializeField] private Rigidbody playerRB;
    [Space]
    public float moveSpeed = 1;
    [SerializeField] private float moveMinInput = 0.2f;
    [Space]
    [SerializeField] private float minJumpInput = 0.25f;
    [SerializeField] private float jumpPower = 500;

    [Header("VFX")]
    [SerializeField] private MMF_Player moveFeedback;
    [SerializeField] private MMF_Player jumpFeedback;
    [SerializeField] private MMF_Player landFeedback;
    [SerializeField] private float chargeDuration = 0.2f;

    [SerializeField] private float minFallIntencivity = 0.2f;
    [SerializeField] private float maxFallIntencivity = 1f;
    [SerializeField] private float minFallVelocity = 1f;
    [SerializeField] private float maxFallVelocity = 1f;
    [SerializeField] private float minVelocity = 0.01f;


    [Header("Knockback")]
    [SerializeField] private Vector3 bonusKnockbackDirection = Vector3.up;
    [SerializeField] private float knockbackPower = 100;
    [SerializeField] private float maxVerticalKnockback = 100;


    private bool isCharging;
    private bool isJumping = true;

    private float previousFallSpeed = 0.1f;
    private float fallImpact;

    public bool isMoving => joystick.Direction.sqrMagnitude > 0;

    private void FixedUpdate()
    {
        HandleInput();
        HandleJump();
        if (isJumping)
            HandleFall();

    }

    private void HandleInput()
    {
        float input = joystick.Horizontal;
        // float input = Input.GetAxis("Horizontal");
        float horizontalInput = input * moveSpeed * playerRB.mass * playerRB.drag;
        playerRB.velocity = new Vector3(horizontalInput, playerRB.velocity.y);

        if (Mathf.Abs(horizontalInput) > 0 && !isCharging && !isJumping)
        {
            moveFeedback.PlayFeedbacks();
        }
        else
        {
            moveFeedback.StopFeedbacks();
        }

    }

    private void HandleJump()
    {
        float input = joystick.Vertical;
        //float input = Input.GetAxis("Vertical");

        if (input > minJumpInput && !isJumping && !isCharging)
        {
            landFeedback.StopFeedbacks();
            jumpFeedback.PlayFeedbacks();
            StartCoroutine(ChargeJump());
        }
    }
    private void HandleFall()
    {
        // landed
        if (Mathf.Abs(playerRB.velocity.y) < minVelocity && previousFallSpeed < minVelocity)
        {
            isJumping = false;
            landFeedback.PlayFeedbacks();
        }
        // flying up / down
        else if (Mathf.Abs(playerRB.velocity.y) >= minVelocity && previousFallSpeed >= minVelocity)
        {
            float v = Mathf.InverseLerp(minFallVelocity, maxFallVelocity, Mathf.Abs(playerRB.velocity.y));
            fallImpact = Mathf.Lerp(minFallIntencivity, maxFallIntencivity, v);
        }

        previousFallSpeed = Mathf.Abs(playerRB.velocity.y);
    }

    private IEnumerator ChargeJump()
    {
        isCharging = true;
        yield return new WaitForSeconds(chargeDuration);
        isCharging = false;

        Vector3 jumpForce = jumpPower * playerRB.mass * playerRB.drag * Vector3.up;
        playerRB.AddForce(jumpForce);

        previousFallSpeed = 0.1f;
        isJumping = true;
    }

    public void Knockback(Vector3 knockbackDirection)
    {
        Vector3 force = playerRB.mass * playerRB.drag * knockbackPower * knockbackDirection.normalized + bonusKnockbackDirection;
        force.y = Mathf.Clamp(force.y, -maxVerticalKnockback, maxVerticalKnockback);
        playerRB.AddForce(force);
    }
}
