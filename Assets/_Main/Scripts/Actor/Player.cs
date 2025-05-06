using System;

using UnityEngine;

/// <summary>
/// プレイヤークラス
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private int hp = 1;

    /// <summary>
    /// 死亡した時のイベント
    /// </summary>
    public event Action OnDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePosition();
        FireBullet();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void MovePosition()
    {
        // キーボードの入力を取得
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = new Vector2(horizontal, vertical);

        if (move != Vector2.zero)
        {
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 弾を撃つ
    /// </summary>
    private void FireBullet()
    {
        // キーボードの入力を取得
        if (Input.GetButtonDown("Fire1"))
        {
            var instance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            var bullet = instance.GetComponent<PlayerBullet>();
            bullet.Go();
        }
    }

    /// <summary>
    /// 被弾した
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1.5f);
            Destroy(gameObject);
            OnDead?.Invoke();
        }
    }
}
