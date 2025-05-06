using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectScene : MonoBehaviour
{
    [SerializeField]
    private Button stage1Button;

    [SerializeField]
    private Button stage2Button;

    [SerializeField]
    private Button stage3Button;

    private void Awake()
    {
        // ボタンが押された時の処理を登録
        stage1Button.onClick.AddListener(() => GotoGameScene(1));
        stage2Button.onClick.AddListener(() => GotoGameScene(2));
        stage3Button.onClick.AddListener(() => GotoGameScene(3));
    }

    /// <summary>
    /// 指定されたステージに移動
    /// </summary>
    /// <param name="stageNumber"></param>
    private void GotoGameScene(int stageNumber)
    {
        SceneManager.LoadScene($"GameScene{stageNumber:000}");
    }
}
