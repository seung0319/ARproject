using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 네이버 클라우드 플렛폼의 MapsAPI중 Directions5 API에 Directions5 Json 요청
/// </summary>
public class DirectionManger : MonoBehaviour
{
    [SerializeField] string Baseurl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string ClientID = "xev65etf45";
    [SerializeField] string ClientSecret = "DbGrTl24UthXNSoedR18bWmyVOIAovvbQMUzsIGZ";
    [SerializeField] string myPoint = "";
    [SerializeField] string destination = "";
    [SerializeField] enum OptionCode
    {
        trafast,
        tracomfort,
        traoptimal,
        traavoidtoll,
        traavoidcaronly
    }

    IEnumerator Start()
    {
        string apiRequesrURL = Baseurl + $"?start = {myPoint}&goal={destination}&option={OptionCode.trafast}";
        UnityWebRequest request = UnityWebRequest.Get(apiRequesrURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", ClientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", ClientSecret);

        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            case UnityWebRequest.Result.ConnectionError:
                yield break;
                break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
                break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
                break;
        }

        if(request.isDone)
        {
            string json = request.downloadHandler.text;
            Debug.Log(json);
        }
    }
}
