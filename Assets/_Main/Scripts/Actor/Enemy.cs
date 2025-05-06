using System;
using System.Collections;

using UnityEngine;

/// <summary>
/// �G�N���X
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
    /// �G���L�����ǂ���
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// ���̒e��������
    /// </summary>
    private float nextFireTime = 0f;

    /// <summary>
    /// ���S�������̃C�x���g
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
                // ��ʊO�ɏo�������
                Dead();
            }
        }
    }

    /// <summary>
    /// �ړ�
    /// </summary>
    private void MovePosition()
    {
        rb.MovePosition(rb.position + Vector2.down * speed * Time.deltaTime);
    }

    /// <summary>
    /// �e������
    /// </summary>
    private void FireBullet()
    {
        if (transform.position.y >= 3.7f)
        {
            // ��ʊO�ɂ���Ԃ͒e�������Ȃ�
            return;
        }

        // �e�������Ă������Ԃ��H
        bool isTiming = Time.time >= nextFireTime;

        // �e�����m���i20%�j�����߂�
        bool persent = UnityEngine.Random.Range(0f, 1f) < 0.2f;
        if (isTiming && persent)
        {
            // �e��������
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.Go();
        }

        // ���̔��ˎ��Ԃ����߂�
        nextFireTime = Time.time + fireRate;
    }

    /// <summary>
    /// �G��L���ɂ���
    /// </summary>
    private void Go()
    {
        isActive = true;
    }

    /// <summary>
    /// ��e����
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
    /// �_���[�W���o�i�Ԃɖ��ł�����j
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
    /// ���S
    /// </summary>
    private void Dead()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1.5f);
        Destroy(gameObject);

        OnDead?.Invoke();
    }

    /// <summary>
    /// �I�u�W�F�N�g�ƂԂ�����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �v���C���[�Ƀ_���[�W��^����
            var player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}