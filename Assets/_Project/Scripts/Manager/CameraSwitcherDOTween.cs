using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Teo.AutoReference;
using UnityEngine;
using UnityEngine.InputSystem; // dùng Input System mới

public class CameraSwitcherDOTween : BaseMonoBehaviour
{
    [SerializeField, Get]
    private CameraFollow cameraFollow;
    [Header("Camera Points")]
    public Transform[] cameraPoints; // 3 góc camera

    [Header("Transition Settings")]
    public float transitionDuration = 1f;
    public Ease transitionEase = Ease.InOutSine;

    private Camera mainCamera;
    private AudioListener audioListener;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        audioListener = mainCamera.GetComponent<AudioListener>();
        if (audioListener == null)
        {
            audioListener = mainCamera.gameObject.AddComponent<AudioListener>();
        }

        // Đặt camera vào vị trí đầu tiên
        if (cameraPoints.Length > 0)
        {
            mainCamera.transform.position = cameraPoints[0].position;
            mainCamera.transform.rotation = cameraPoints[0].rotation;
        }
    }

    // protected override void Update()
    // {
    //     base.Update();
    //     if (Keyboard.current.digit1Key.wasPressedThisFrame) SwitchCamera(0);
    //     if (Keyboard.current.digit2Key.wasPressedThisFrame) SwitchCamera(1);
    // }

    void SwitchCamera(int index)
    {
        if (index < 0 || index >= cameraPoints.Length) return;

        Transform targetPoint = cameraPoints[index];

        // DOTween di chuyển và xoay mượt
        mainCamera.transform.DOMove(targetPoint.position, transitionDuration).SetEase(transitionEase);
        mainCamera.transform.DORotateQuaternion(targetPoint.rotation, transitionDuration).SetEase(transitionEase)
        .OnComplete(() =>
        {
            if (index == 0)
            {
                cameraFollow.SetIsPause(false);
            }
        });

        // Đảm bảo chỉ 1 AudioListener (nếu có nhiều camera khác)
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        foreach (var l in listeners)
        {
            l.enabled = (l == audioListener);
        }
    }

    public void CheckSwitchCamera(int index)
    {
        // bool bl = index == 0 ? false : true;
        cameraFollow.SetIsPause(true);
        SwitchCamera(index);
    }
}
