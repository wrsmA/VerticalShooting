using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void Awake()
    {
        // �{�^���������ꂽ���̏�����o�^
        startButton.onClick.AddListener(GotoStageSelectScene);
    }

    /// <summary>
    /// �X�e�[�W�I����ʂɍs��
    /// </summary>
    private void GotoStageSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
