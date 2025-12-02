using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleUI : MonoBehaviour
{
    public GameObject ShowRecordPopup;

    void Start()
    {
        //カーソルを表示して動かせるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Enemy");
        //いつかMainにする↑
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnClickShowRecords()
    {
        ShowRecordPopup.SetActive(true);
    }

}
