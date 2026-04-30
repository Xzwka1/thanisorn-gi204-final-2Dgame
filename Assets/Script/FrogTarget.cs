using UnityEngine;

public class FrogTarget : MonoBehaviour
{
    [Header("ฟิสิกส์ตอนโดนยิง")]
    public float torqueForce = 15f;
    public float destroyDelay = 1.5f;
    private bool isHit = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 0;
    }

    public void OnHit()
    {
        if (isHit) return;
        isHit = true;
        Debug.Log("🎯 กบโดนยิง!");

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