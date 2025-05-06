using System;

using UnityEngine;

/// <summary>
/// �v���C���[�N���X
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
    /// ���S�������̃C�x���g
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
    /// �ړ�
    /// </summary>
    private void MovePosition()
    {
        // �L�[�{�[�h�̓��͂��擾
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = new Vector2(horizontal, vertical);

        if (move != Vector2.zero)
        {
            rb.MovePosition(rb.position + move * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// �e������
    /// </summary>
    private void FireBullet()
    {
        // �L�[�{�[�h�̓��͂��擾
        if (Input.GetButtonDown("Fire1"))
        {
            var instance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            var bullet = instance.GetComponent<PlayerBullet>();
            bullet.Go();
        }
    }

    /// <summary>
    /// ��e����
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
