using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayPopup : MonoBehaviour
{
    public GameObject Popup;

    //•Â‚¶‚éƒ{ƒ^ƒ“
    public void OnClickCloseButton()
    {
        Popup.SetActive(false);
    }
}