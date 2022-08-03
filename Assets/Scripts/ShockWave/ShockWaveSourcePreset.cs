using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SWSourcePreset", menuName = "ShockWave/SourcePreset", order = 0)]
public class ShockWaveSourcePreset : ScriptableObject
{
    [SerializeField] private float sourceMultiplier = 1;
    [SerializeField] private float spreadSpeed = 100;
    [SerializeField] private float maxRadius = 100;
    [SerializeField] private AnimationCurve impactCurve;

    [HideInInspector] public Vector3 sourcePosition;

    public float SpreadSpeed => spreadSpeed;
    public float SourceMultiplier => sourceMultiplier;
    public float MaxRadius => maxRadius;
    public AnimationCurve ImpactCurve => impactCurve;
}
