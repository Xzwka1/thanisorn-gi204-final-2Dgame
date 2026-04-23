using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float jumpForce = 15f;

    [Header("Health System")]
    public int maxHealth = 100;      // เลือดสูงสุด
    private int currentHealth;       // เลือดปัจจุบัน

    private float moveValue;
    private Rigidbody2D _rb;
    private bool _isGrounded = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // เติมเลือดให้เต็มเมื่อเริ่มเกม
        currentHealth = maxHealth;
    }

    void Update()
    {
        // 1. รับค่าการเดินซ้าย-ขวา
        if (Keyboard.current != null)
        {
            moveValue = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);
        }

        // 2. เคลื่อนที่แนวนอน
        _rb.linearVelocity = new Vector2(moveValue * speed, _rb.linearVelocity.y);

        // 3. ระบบกระโดด
        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
        {
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    // ==========================================
    // -------- ระบบเลือดและการโดนโจมตี --------
    // ==========================================

    // ฟังก์ชันนี้จะถูกเรียกโดยกระสุนของศัตรู
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("อั๊ค! ผู้เล่นโดนโจมตี! เลือดเหลือ: " + currentHealth);

        // เช็คว่าเลือดหมดหรือยัง
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("💀 Game Over! ผู้เล่นตายแล้ว");

        // เมื่อตายให้ทำลายวัตถุตัวละครทิ้ง (หรือจะเปลี่ยนเป็นการเรียกหน้า Game Over ก็ได้)
        Destroy(gameObject);
    }
}