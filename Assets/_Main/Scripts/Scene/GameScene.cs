using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Button retryButton;

    [SerializeField]
    private Button backToTitleButton;

    [SerializeField]
    private GameObject gameOverGUI;

    [SerializeField]
    private Player player;

    private Enemy[] enemies;

    private int enemyCount = 0;

    private void Awake()
    {
        // �{�^���������ꂽ���̏�����o�^
        retryButton.onClick.AddListener(RetryStage);
        backToTitleButton.onClick.AddListener(GotoTitleScene);
        
        gameOverGUI.SetActive(false);

        SetupEnemies();
        SpawnPlayer();
    }

    /// <summary>
    /// ���݂̃X�e�[�W����蒼��
    /// </summary>
    private void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// �^�C�g���V�[���ɖ߂�
    /// </summary>
    private void GotoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// �v���C���[���X�|�[������
    /// </summary>
    private void SpawnPlayer()
    {
        // �v���C���[���X�|�[��
        var playerInstance = Instantiate(player, new Vector3(0, -2.5f, 0), Quaternion.identity);
        playerInstance.OnDead += OnPlayerDead;
    }

    /// <summary>
    /// �G���Z�b�g�A�b�v����
    /// </summary>
    private void SetupEnemies()
    {
        // �G�̏����擾
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemyCount = enemies.Length;
        foreach (var enemy in enemies)
        {
            enemy.OnDead += OnEnemyDead;
        }
    }

    /// <summary>
    /// �G�����S�������̏���
    /// </summary>
    private void OnEnemyDead()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            // �S�Ă̓G�����S�����ꍇ
            gameOverGUI.SetActive(true);
        }
    }

    /// <summary>
    /// �v���C���[�����S�������̏���
    /// </summary>
    private void OnPlayerDead()
    {
        gameOverGUI.SetActive(true);
    }
}