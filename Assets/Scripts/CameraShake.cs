using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private Camera mainCamera;
    //震动标志位
    public bool isShakeCamera = false;
    //震动幅度
    public float shakeLevel = 0.1f;
    //震动时间
    public float setShakeTime = 1f;
    //震动的FPS
    public float shakeFps = 45f;

    private float fps;
    private float shakeTime = 0.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;

    void Awake()
    {
        //获取Camera组件
        mainCamera = GetComponent<Camera>();
        Instance = this;
    }

    void Start()
    {
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
    }

    void Update()
    {
        if (isShakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    mainCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isShakeCamera = false;
                    shakeTime = setShakeTime;
                    fps = shakeFps;
                    frameTime = 0.03f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;
                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        mainCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value*(20-Player.health*0.01f)),
                            shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }

    public void Shake()
    {
        isShakeCamera = true;
    }
}
