using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    [Header("การเคลื่อนที่ (ซ้าย-ขวา แกน X)")]
    public float moveSpeed = 2f;
    public float moveRange = 3f;
    private Vector3 startPos;

    [Header("ฟิสิกส์ตอนโดนยิง")]
    public float torqueForce = 15f;
    public float destroyDelay = 1.5f;
    private bool isHit = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        if (rb != null) rb.gravityScale = 0;
    }

    void Update()
    {
        if (!isHit)
        {
            float newX = startPos.x + Mathf.Sin(Time.time * moveSpeed) * moveRange;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    public void OnHit()
    {
        if (isHit) return;
        isHit = true;
        Debug.Log("🎯 หนูโดนยิง!");

        if (rb != null)
        {
            float I = rb.inertia;
            float alpha = torqueForce;
            float calculatedTorque = I * alpha;
            rb.AddTorque(calculatedTorque, ForceMode2D.Impulse);
        }

        if (ScoreManager.instance != null) ScoreManager.instance.AddScore(1);
        Destroy(gameObject, destroyDelay);
    }
}