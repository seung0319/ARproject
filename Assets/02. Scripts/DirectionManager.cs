using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DirectionManager : MonoBehaviour
{
    [SerializeField] string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving";
    [SerializeField] string clientID = "w9g29xtrwz";
    [SerializeField] string clientSecret = "WespgDc0DDelU0pN69HzZgyj5ByEBwhDGU0gxxTB";
    [SerializeField] string myPoint = "";
    [SerializeField] string destination = "";
    [SerializeField] string option = "목적지이름";

    IEnumerator Start()
    {
        //"https://naveropenapi.apigw.ntruss.com/map-direction/v1/driving?start={출발지}&goal={목적지}&option={탐색옵션}" \
        string apiRequestURL = $"{baseUrl}?start={myPoint}&goal={destination}&option={option}";

        UnityWebRequest request = UnityWebRequest.Get(apiRequestURL);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        // 데이터 로드 실패시
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                yield break;
            case UnityWebRequest.Result.Success:
                break;
        }
        // 데이터 로드 성공시
        if (request.isDone)
        {
            string json = request.downloadHandler.text;
            print(json);
        }
    }
}
