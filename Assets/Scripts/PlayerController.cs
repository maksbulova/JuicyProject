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
    [SerializeField] private float jumpPower = 1;
    [SerializeField] private float jumpMinInput = 0.2f;

    [Header("VFX")]
    [SerializeField] private MMF_Player jumpFeedback;
    [SerializeField] private MMF_Player landFeedback;
    [SerializeField] private float chargeDuration = 0.2f;

    private bool canJump = true;

    private void Start()
    {
        canJump = true;
    }

    private void FixedUpdate()
    {
        HandleInput();
        HandleJump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;

            landFeedback.PlayFeedbacks();
        }
    }

    private void HandleInput()
    {
        if (Mathf.Abs(joystick.Horizontal) < moveMinInput)
            return;

        float horizontalInput = joystick.Horizontal * moveSpeed * playerRB.mass * playerRB.drag;
        playerRB.velocity = new Vector3(horizontalInput, playerRB.velocity.y);
    }

    private void HandleJump()
    {
        if (canJump && joystick.Vertical > jumpMinInput)
        {
            canJump = false;

            landFeedback.StopFeedbacks();
            jumpFeedback.PlayFeedbacks();
            StartCoroutine(ChargeJump());
        }
    }

    private IEnumerator ChargeJump()
    {
        yield return new WaitForSeconds(chargeDuration);

        Vector3 jumpForce = jumpPower * playerRB.mass * playerRB.drag * Vector3.up;
        playerRB.AddForce(jumpForce);
    }
}
