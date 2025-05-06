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
        // ボタンが押された時の処理を登録
        retryButton.onClick.AddListener(RetryStage);
        backToTitleButton.onClick.AddListener(GotoTitleScene);
        
        gameOverGUI.SetActive(false);

        SetupEnemies();
        SpawnPlayer();
    }

    /// <summary>
    /// 現在のステージをやり直す
    /// </summary>
    private void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// タイトルシーンに戻る
    /// </summary>
    private void GotoTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    /// <summary>
    /// プレイヤーをスポーンする
    /// </summary>
    private void SpawnPlayer()
    {
        // プレイヤーをスポーン
        var playerInstance = Instantiate(player, new Vector3(0, -2.5f, 0), Quaternion.identity);
        playerInstance.OnDead += OnPlayerDead;
    }

    /// <summary>
    /// 敵をセットアップする
    /// </summary>
    private void SetupEnemies()
    {
        // 敵の情報を取得
        enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemyCount = enemies.Length;
        foreach (var enemy in enemies)
        {
            enemy.OnDead += OnEnemyDead;
        }
    }

    /// <summary>
    /// 敵が死亡した時の処理
    /// </summary>
    private void OnEnemyDead()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            // 全ての敵が死亡した場合
            gameOverGUI.SetActive(true);
        }
    }

    /// <summary>
    /// プレイヤーが死亡した時の処理
    /// </summary>
    private void OnPlayerDead()
    {
        gameOverGUI.SetActive(true);
    }
}