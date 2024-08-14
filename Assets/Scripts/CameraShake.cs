using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private Camera mainCamera;
    void Awake()
    {
        //获取Camera组件
        mainCamera = GetComponent<Camera>();
        Instance = this;
    }

    void Update()
    {
    }

    public void Shake()
    {
        mainCamera.DOShakePosition(0.4f, strength: 0.6f, vibrato: 20);
    }
}
