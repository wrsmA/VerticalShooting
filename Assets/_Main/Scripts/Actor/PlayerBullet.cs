using UnityEngine;

/// <summary>
/// プレイヤーの弾クラス
/// </summary>
public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 10f;

    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Go()
    {
        isMoving = true;
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + Vector2.up * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// オブジェクトとぶつかった
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // 敵にダメージを与える
            collision.GetComponent<Enemy>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
