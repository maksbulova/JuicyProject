using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShockWaveListener : MonoBehaviour
{
    [SerializeField] private ShockWaveImpactPreset swImpactPreset;
    [SerializeField] private bool drawDebug;

    private float distance;
    private (Vector3, Quaternion) initialTransform;
    private Tween impactTween;

    private void Start()
    {
        initialTransform = (transform.position, transform.rotation);
        ShockWaveController.Instance.onShockEvent += RecieveShock;
    }

    private void OnDestroy()
    {
        if(ShockWaveController.Instance != null)
            ShockWaveController.Instance.onShockEvent -= RecieveShock;
    }

    private void RecieveShock(ShockWaveSourcePreset swSourcePreset)
    {
        distance = (transform.position - swSourcePreset.sourcePosition).magnitude;
        if (distance <= swSourcePreset.MaxRadius)
        {
            StartCoroutine(DelayedShock(swSourcePreset));
        }
    }

    private IEnumerator DelayedShock(ShockWaveSourcePreset swSourcePreset)
    {
        if (swSourcePreset.SpreadSpeed > 0)
        {
            float delay = distance / swSourcePreset.SpreadSpeed;

            yield return new WaitForSeconds(delay);
        }

        ShockImpact(swSourcePreset);
    }

    private void ShockImpact(ShockWaveSourcePreset swSourcePreset)
    {
        if (impactTween != null)
        {
            impactTween.Kill();
            transform.SetPositionAndRotation(initialTransform.Item1, initialTransform.Item2);
        }

        float d = distance / swSourcePreset.MaxRadius;
        float distanceMultiplier = swSourcePreset.ImpactCurve.Evaluate(d);

        Vector3 impactDirection;
        if (swImpactPreset.PunchDirection == Vector3.zero)
        {
            impactDirection = (transform.position - swSourcePreset.sourcePosition).normalized;
        }
        else
        {
            impactDirection = swImpactPreset.PunchDirection;
        }

        Vector3 resultImpact = impactDirection * distanceMultiplier * swImpactPreset.ImpaectMultipier * swSourcePreset.SourceMultiplier;

        if (drawDebug)
            Debug.DrawLine(transform.position, swSourcePreset.sourcePosition, Color.Lerp(Color.green, Color.red, distanceMultiplier), 1);

        impactTween = transform.DOPunchPosition(resultImpact, swImpactPreset.PunchDuration * distanceMultiplier, swImpactPreset.PunchVibrato)
            .SetEase(swImpactPreset.PunchCurve)
            .OnComplete(() => impactTween = null);
    }
}
