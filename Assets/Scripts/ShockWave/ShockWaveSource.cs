using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveSource : MonoBehaviour
{
    [SerializeField] private ShockWaveSourcePreset swSourcePreset;
    [SerializeField] private bool drawDebug;

    private void OnEnable()
    {
        swSourcePreset.sourcePosition = transform.position;
    }

    public void GenerateShoke()
    {
        GenerateShoke(transform.position);
    }

    public void GenerateShoke(Vector3 sourcePosition)
    {
        swSourcePreset.sourcePosition = sourcePosition;

        ShockWaveController.Instance.InvokeShockWave(swSourcePreset);
    }

    private void OnDrawGizmos()
    {
        if (!drawDebug)
            return;

        Gizmos.color = Color.Lerp(new Color(1, 0, 0, 0.75f), new Color(0, 1, 0, 0.75f), 1f / 3f);
        Gizmos.DrawSphere(swSourcePreset.sourcePosition, swSourcePreset.MaxRadius / 3);

        Gizmos.color = Color.Lerp(new Color(1, 0, 0, 0.5f), new Color(0, 1, 0, 0.5f), 2f / 3f);
        Gizmos.DrawSphere(swSourcePreset.sourcePosition, swSourcePreset.MaxRadius * 2 / 3);

        Gizmos.color = Color.Lerp(new Color(1, 0, 0, 0.25f), new Color(0, 1, 0, 0.25f), 3f / 3f);
        Gizmos.DrawSphere(swSourcePreset.sourcePosition, swSourcePreset.MaxRadius);
    }
}
