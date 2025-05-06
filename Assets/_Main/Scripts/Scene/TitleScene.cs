using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void Awake()
    {
        // ボタンが押された時の処理を登録
        startButton.onClick.AddListener(GotoStageSelectScene);
    }

    /// <summary>
    /// ステージ選択画面に行く
    /// </summary>
    private void GotoStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
