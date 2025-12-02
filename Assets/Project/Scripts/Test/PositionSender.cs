using UnityEngine;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

public class PositionSender : MonoBehaviour
{
    public Transform characterTransform;
    private string serverUrl = "http://localhost:5299/position"; // Visual Studio側のサーバURL

    void Start()
    {
        InvokeRepeating(nameof(SendPosition), 1f, 1f); // 1秒ごとに送信
    }

    void SendPosition()
    {
        Vector3 pos = characterTransform.position;
        string json = JsonUtility.ToJson(new PositionData(pos.x, pos.y, pos.z));
        StartCoroutine(PostRequest(serverUrl, json));
    }

    IEnumerator PostRequest(string url, string json)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Error: " + request.error);
        else
            Debug.Log("Position sent: " + json);
    }

    [System.Serializable]
    public class PositionData
    {
        public float x, y, z;
        public PositionData(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }
    }
}
