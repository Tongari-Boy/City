using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleUI : MonoBehaviour
{
    public GameObject ShowRecordPopup;
    public GameObject HowToPlayPopup;

    void Start()
    {
        //カーソルを表示して動かせるようにする
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("City");
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

    public void OnClickHowToPlay()
    {
        HowToPlayPopup.SetActive(true);
    }
}