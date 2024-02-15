using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StaticMapManager : MonoBehaviour
{
    [SerializeField] string baseUrl = "https://naveropenapi.apigw.ntruss.com/map-static/v2/raster";
    [SerializeField] string clientID = "w9g29xtrwz";
    [SerializeField] string clientSecret = "WespgDc0DDelU0pN69HzZgyj5ByEBwhDGU0gxxTB";
    public RawImage mapRawImage;
    
    public string latitude = "37.467012";
    public string longitude = "126.657155";
    public int level = 15;
    public int mapWidth;
    public int mapHeight;

    StringBuilder markersParam;

    public Text debug;

    [System.Serializable]
    public class POIDataList
    {
        public List<POI> pois;
    }

    public void Start()
    {
        //string path = Application.dataPath + "/04. Resources/ROIInfo.json";
        //Debug.Log(path);
        //string json = File.ReadAllText(path);
        //Debug.Log(json);
        //POIDataList root = JsonUtility.FromJson<POIDataList>(json);
        //Debug.Log(root);

        //markersParam = new StringBuilder();
        //foreach (POI poi in root.pois)
        //{
        //    Debug.Log(poi.name);
        //    markersParam.Append($"&markers=type:d|size:small|pos:{poi.longitude} {poi.latitude}|label:{poi.name}|viewSizeRatio:0.3");
        //}
        //Debug.Log(markersParam.ToString());

        StartCoroutine(MapLoader());
    }

    IEnumerator MapLoader()
    {
        string apiRequestURL = $"{baseUrl}?w={mapWidth}&h={mapHeight}&center={longitude},{latitude}&level={level}";
        debug.text = "Hello";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(apiRequestURL);
        debug.text = "Heck";
        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientID);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);
        yield return request.SendWebRequest();
        debug.text = "Hi";
        // 데이터 로드 실패시
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                debug.text = "CE";
                Debug.Log("CE");
                yield break;
            case UnityWebRequest.Result.ProtocolError:
                debug.text = "PE";
                Debug.Log("PE");
                yield break;
            case UnityWebRequest.Result.DataProcessingError:
                debug.text = "DP";
                Debug.Log("DP");
                yield break;
            case UnityWebRequest.Result.Success:
                debug.text = "S";
                Debug.Log("S");
                break;
        }
        // 데이터 로드 성공시
        if (request.isDone)
        {
            //string json = request.downloadHandler.text;
            //print(json);
            debug.text = "YA";
            Debug.Log("Ya");


            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }
}
