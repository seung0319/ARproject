using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    public string url = "https://docs.google.com/spreadsheets/d/10GaozCAYllIZpwaNcOVs_IMyFdZO7P77DjIqR2mWiBY/edit#gid=0";
    struct POIdataList
    {
        public List<POIdata> pois;
        //public POIdata[] pois2 = new POIdata[2];
    }
    POIdataList data = new POIdataList();
    // Start is called before the first frame update
    void ReadJson()
    {
        //string path = Application.dataPath + "Resources/ROIInfo.json";
        TextAsset json = Resources.Load<TextAsset>("ROIInfo");
        data = JsonUtility.FromJson<POIdataList>(json.ToString());
        //data = JsonUtility.FromJson<POIdataList>(json.text);

        Debug.Log(data.pois.Count);
        //Debug.Log(data.pois.Length);
    }
    void Start()
    {
        ReadJson();
        StartCoroutine(RequestCoroutine());
    }

    IEnumerator RequestCoroutine()
    {
        UnityWebRequest data = UnityWebRequest.Get(url);

        yield return data.SendWebRequest(); //서버에 데이터 요청 보내기

        switch (data.result)
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
        if (data.isDone)
        {
            string json = data.downloadHandler.text; ;

            string[] rows = json.Split("\n");
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split("\t");

                foreach (var column in columns)
                {
                    Debug.Log("Line: " + i + ": " + column);//구글 스프레드 시트 사용 2~10번째
                }
            }
        }
    }
}
