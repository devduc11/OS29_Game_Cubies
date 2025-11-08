using System.Collections;
using Teo.AutoReference;
using Unity.VisualScripting;
using UnityEngine;

public class PlayManager : BaseMonoBehaviour
{
    [SerializeField, FindInScene]
    private CameraSwitcherDOTween cameraSwitcherDOTween;
    public Joystick joystick;
    public float rollSpeed = 180f;
    public float checkDistance = 1.1f;
    public float cellSize = 2f; // Kích thước mỗi ô grid

    private bool isRolling = false;
    private bool isFalling = false;
    private Rigidbody rb;

    #region LoadComponents
    protected override void LoadComponents()
    {
        base.LoadComponents();
    }
    #endregion

    protected override void Start()
    {
        base.Start();
        SnapToGrid();
    }

    protected override void Update()
    {
        base.Update();
        if (isRolling) return;

        // Nếu đang rơi → đợi chạm đất
        if (isFalling)
        {
            if (HasGroundBelow()) StopFalling();
            return;
        }

        float h = joystick.Horizontal;
        float v = joystick.Vertical;

        if (v > 0.5f && !HasObstacle(Vector3.forward)) StartCoroutine(Roll(Vector3.forward));
        else if (v < -0.5f && !HasObstacle(Vector3.back)) StartCoroutine(Roll(Vector3.back));
        else if (h < -0.5f && !HasObstacle(Vector3.left)) StartCoroutine(Roll(Vector3.left));
        else if (h > 0.5f && !HasObstacle(Vector3.right)) StartCoroutine(Roll(Vector3.right));
    }

    // Kiểm tra vật cản theo hướng dir
    bool HasObstacle(Vector3 dir)
    {
        return Physics.Raycast(transform.position, dir, cellSize);
    }

    IEnumerator Roll(Vector3 dir)
    {
        isRolling = true;

        // Tính pivot xoay dựa theo cellSize (thay vì 1)
        Vector3 pivot = transform.position
                        + (dir * (cellSize / 2f))
                        + (Vector3.down * (cellSize / 2f));

        float rotated = 0f;
        float targetAngle = 90f;

        Vector3 axis = Vector3.Cross(Vector3.up, dir);

        while (rotated < targetAngle)
        {
            float step = rollSpeed * Time.deltaTime;
            transform.RotateAround(pivot, axis, step);
            rotated += step;
            yield return null;
        }

        SnapToGrid();
        isRolling = false;

        if (!HasGroundBelow()) StartFalling();
    }

    bool HasGroundBelow()
    {
        return Physics.Raycast(transform.position, Vector3.down, out _, checkDistance);
    }

    void StartFalling()
    {
        if (isFalling) return;
        isFalling = true;

        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 10f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void StopFalling()
    {
        isFalling = false;
        SnapToGrid();

        if (rb != null)
        {
            Destroy(rb);
            rb = null;
        }
    }

    void SnapToGrid()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Round(pos.x / cellSize) * cellSize;
        pos.y = Mathf.Round(pos.y / cellSize) * cellSize;
        pos.z = Mathf.Round(pos.z / cellSize) * cellSize;

        transform.position = pos;

        transform.rotation = Quaternion.Euler(
            Mathf.Round(transform.eulerAngles.x / 90f) * 90f,
            Mathf.Round(transform.eulerAngles.y / 90f) * 90f,
            Mathf.Round(transform.eulerAngles.z / 90f) * 90f
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kiểm tra tag của đối tượng va chạm
        if (collision.gameObject.tag == "Suggest")
        {
            cameraSwitcherDOTween.CheckSwitchCamera(1);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suggest"))
        {
            cameraSwitcherDOTween.CheckSwitchCamera(0);
        }
    }
}
