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
        // �{�^���������ꂽ���̏�����o�^
        stage1Button.onClick.AddListener(() => GotoGameScene(1));
        stage2Button.onClick.AddListener(() => GotoGameScene(2));
        stage3Button.onClick.AddListener(() => GotoGameScene(3));
    }

    /// <summary>
    /// �w�肳�ꂽ�X�e�[�W�Ɉړ�
    /// </summary>
    /// <param name="stageNumber"></param>
    private void GotoGameScene(int stageNumber)
    {
        SceneManager.LoadScene($"GameScene{stageNumber:000}");
    }
}
