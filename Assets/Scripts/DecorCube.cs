using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DecorCube : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    // [SerializeField] private float rotationDuration = 5;

    private void Start()
    {
        meshRenderer.material.color = Random.ColorHSV();

        transform.rotation = Random.rotation;
        //transform.DORotateQuaternion(Random.rotation, rotationDuration).SetEase(Ease.InOutCubic)
        //    .SetLoops(-1, LoopType.Yoyo)
        //    .SetDelay(Random.Range(0, rotationDuration));
    }


}
