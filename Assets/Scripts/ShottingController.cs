using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class ShottingController : MonoBehaviour
{
    [Header("Shoting")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletDamage;

    [Header("Raycast")]
    [SerializeField] private Collider raycastTargetCollider;
    [SerializeField] private float raycastDistance = 100;

    [Header("VFX")]
    [SerializeField] private MMF_Player shotFeedback;


    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        InputHandle();
    }

    private void InputHandle()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (raycastTargetCollider.Raycast(ray, out RaycastHit hitInfo, raycastDistance))
            Shoot(hitInfo.point);
    }

    private void Shoot(Vector3 target)
    {
        Vector3 shootDirection = (target - transform.position).normalized;
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(shootDirection));

        bullet.Init(shootDirection, bulletVelocity, bulletDamage);

        shotFeedback.PlayFeedbacks();
    }
}
