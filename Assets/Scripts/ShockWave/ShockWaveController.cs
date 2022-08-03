using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoSingleton<ShockWaveController>
{
    public System.Action<ShockWaveSourcePreset> onShockEvent;

    public void InvokeShockWave(ShockWaveSourcePreset shockWavePreset)
    {
        onShockEvent?.Invoke(shockWavePreset);
    }
}
