using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequestContrl : MonoBehaviour
{
    //private string url = "http://122.231.167.32:7515/large/screen/";
    //private string url = "http://192.168.1.218:7151/itom/large/screen/";
    private string url = "http://sc.ebike-charge.com/itom/large/screen/";
    public IEnumerator DoRequestGet(string methonName, System.Action<string> data)
    {
        var request = UnityWebRequest.Get(url + methonName);
        yield return request.Send();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
            yield break;
        }

        var html = request.downloadHandler.text;
        //Debug.Log(html);
        data(html);
    }

    public IEnumerator WebRquestGetContrl(string thisURL,string parameter,System.Action<string> data)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url + thisURL + "?" + parameter, "GET"))
        {
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            //webRequest.SetRequestHeader("Authorization",  "bearer " + GameManager.userToken);//请求头文件内容
            yield return webRequest.Send();
            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                data(webRequest.downloadHandler.text);
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
}
