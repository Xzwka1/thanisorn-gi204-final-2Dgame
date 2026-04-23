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
            Debug.Log("🔥 กดคลิกซ้ายแล้ว ระบบรับทราบ!");
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 10f));
        Vector2 targetDirection = (new Vector2(mouseWorldPosition.x, mouseWorldPosition.y) - (Vector2)firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(targetDirection * shootForce, ForceMode2D.Impulse);
    }
}