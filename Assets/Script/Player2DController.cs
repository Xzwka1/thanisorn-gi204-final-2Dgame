using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float speed = 5.0f;

    // หมายเหตุ: เมื่อเราเปลี่ยนไปใช้ ForceMode2D.Impulse 
    // ค่า jumpForce 450 จะแรงทะลุจอมาก แนะนำให้ปรับลดลงเหลือประมาณ 10 - 20 ในหน้า Inspector ครับ
    public float jumpForce = 15f;

    private float moveValue;
    private Rigidbody2D _rb;

    // ตัวแปรเช็คว่าตัวละครยืนอยู่บนพื้นหรือไม่
    private bool _isGrounded = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        // 3. ระบบกระโดด: ต้องกด Spacebar และ ต้องอยู่บนพื้น (_isGrounded == true)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && _isGrounded)
        {
            // ดันขึ้นไปในแกน Y ตรงๆ (X=0) และใช้แบบ Impulse เพื่อให้พุ่งทันที
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            // เมื่อกระโดดแล้ว สถานะจะกลายเป็นลอยอยู่กลางอากาศ
            _isGrounded = false;
        }
    }

    // ฟังก์ชันนี้จะทำงานอัตโนมัติเมื่อตัวละครหล่นลงมาแตะวัตถุอื่น (เช่น พื้น)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // เมื่อเท้าแตะพื้น ให้รีเซ็ตสถานะกลับมาเป็น true เพื่อให้กระโดดครั้งต่อไปได้
        _isGrounded = true;
    }
}