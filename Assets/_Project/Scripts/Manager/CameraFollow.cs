using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : BaseMonoBehaviour
{
    public Transform player;      // đối tượng Player
    public Transform main_Camera_Pos_1;
    public Vector3 offset;        // khoảng cách camera so với player
    public float smoothSpeed = 5f; // tốc độ mượt

    private Quaternion initialRotation;
    private bool isPause;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion
    protected override void Start()
    {
        base.Start();
        // Lưu rotation ban đầu của camera
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (isPause) return;
        // Vị trí mục tiêu là player + offset
        Vector3 targetPos = player.position + offset;

        // Di chuyển mượt camera từ vị trí hiện tại đến targetPos
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        main_Camera_Pos_1.position = transform.position;

        // Giữ rotation cố định
        transform.rotation = initialRotation;
    }

    public void SetIsPause(bool isPause)
    {
        this.isPause = isPause;
    }
}
