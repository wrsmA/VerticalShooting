using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// 敵クラス
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private EnemyBullet bulletPrefab;

    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private int hp = 3;

    [SerializeField]
    private float fireRate = 3f;

    /// <summary>
    /// 敵が有効かどうか
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// 次の弾を撃つ時間
    /// </summary>
    private float nextFireTime = 0f;

    /// <summary>
    /// 死亡した時のイベント
    /// </summary>
    public event Action OnDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Go();
    }

    private void Update()
    {
        if (isActive)
        {
            MovePosition();
            FireBullet();

            if (transform.position.y < -5f)
            {
                // 画面外に出たら消す
                Dead();
            }
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void MovePosition()
    {
        rb.MovePosition(rb.position + Vector2.down * speed * Time.deltaTime);
    }

    /// <summary>
    /// 弾を撃つ
    /// </summary>
    private void FireBullet()
    {
        if (transform.position.y >= 3.7f)
        {
            // 画面外にいる間は弾を撃たない
            return;
        }

        // 弾を撃っていい時間か？
        bool isTiming = Time.time >= nextFireTime;

        // 弾を撃つ確率（20%）を決める
        bool persent = UnityEngine.Random.Range(0f, 1f) < 0.2f;
        if (isTiming && persent)
        {
            // 弾を撃つ処理
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.Go();
        }

        // 次の発射時間を決める
        nextFireTime = Time.time + fireRate;
    }

    /// <summary>
    /// 敵を有効にする
    /// </summary>
    private void Go()
    {
        isActive = true;
    }

    /// <summary>
    /// 被弾した
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        hp -= damage;
        PlayDamageEffect();
        if (hp <= 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// ダメージ演出（赤に明滅させる）
    /// </summary>
    private void PlayDamageEffect()
    {
        StartCoroutine(FlashSprite());

        IEnumerator FlashSprite()
        {
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);
        Destroy(gameObject);

        OnDead?.Invoke();
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
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}