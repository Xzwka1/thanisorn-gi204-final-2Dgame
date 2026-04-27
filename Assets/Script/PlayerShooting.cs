using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootForce = 20f;

    void Update()
    {
        // ลบอันที่เบิ้ลออก เหลือเช็คแค่รอบเดียวพอครับ
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("เช็คตอนยิง");
            Shoot();
        }
    }

    void Shoot()
    {
        //คำนวณทิศทางกับหาตำแหน่ง
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 10f));
        Vector2 targetDirection = (new Vector2(mouseWorldPosition.x, mouseWorldPosition.y) - (Vector2)firePoint.position).normalized;

        //สร้างกระสุนและดึง Rigidbody2D
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        float m = rb.mass;       // ตัวแปร m mass มวลของลูกกระสุน
        float a = shootForce;    // ตัวแปร a acceleration ความเร่งที่เราตั้งไว้
        float F = m * a;         // สมการ F = ma คำนวณหาแรงลัพธ์ที่ต้องใช้

        rb.AddForce(targetDirection * shootForce, ForceMode2D.Impulse);
    }
}