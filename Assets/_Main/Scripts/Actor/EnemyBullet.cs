using UnityEngine;

/// <summary>
/// 敵の弾を表すクラス
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5f;

    /// <summary>
    /// 弾が飛んでいるかどうか
    /// </summary>
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 弾を発射する
    /// </summary>
    public void Go()
    {
        isMoving = true;
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + Vector2.down * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// オブジェクトとぶつかった
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // プレイヤーにダメージを与える
            collision.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}