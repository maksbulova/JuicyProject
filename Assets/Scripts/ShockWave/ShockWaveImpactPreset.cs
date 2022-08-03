using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SWImpactPreset", menuName = "ShockWave/ImpactPreset", order = 1)]
public class ShockWaveImpactPreset : ScriptableObject
{
    [Header("Punch")]
    public Vector3 PunchDirection;
    public float ImpaectMultipier = 1;
    public float PunchDuration = 2;
    public int PunchVibrato = 5;
    public AnimationCurve PunchCurve;
}
