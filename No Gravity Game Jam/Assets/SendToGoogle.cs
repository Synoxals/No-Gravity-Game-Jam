using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SendToGoogle : MonoBehaviour
{
    public GameObject nickname;
    public TMP_InputField nameInputField;

    private string username;
    public string score;

    [SerializeField] private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc8-BMl1awbDUZIPeScJ6wrIuF3w-FpURD5GmlL2iET7Ai31w/formResponse";

    public void Send()
    {
        username = nameInputField.text;

        StartCoroutine(Post(username));
    }

    IEnumerator Post(string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.933819968", username);
        form.AddField("entry.1241200766", score);
        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Success");
            }

        }
    }

}
