using UnityEngine;

/// <summary>
/// �G�̒e��\���N���X
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 5f;

    /// <summary>
    /// �e�����ł��邩�ǂ���
    /// </summary>
    private bool isMoving = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �e�𔭎˂���
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
    /// �I�u�W�F�N�g�ƂԂ�����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �v���C���[�Ƀ_���[�W��^����
            collision.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}