using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ทำความเสียหายเมื่อโดนผู้เล่น
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);  // ทำลาย projectile หลังการปะทะ
        }
    }
}
