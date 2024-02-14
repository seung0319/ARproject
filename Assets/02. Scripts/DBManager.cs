using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DBManager : MonoBehaviour
{
    public string url = "https://docs.google.com/spreadsheets/d/10GaozCAYllIZpwaNcOVs_IMyFdZO7P77DjIqR2mWiBY/edit#gid=0";
    struct POIDataList
    {
        public List<POIdata> pois;
    }
    POIDataList data = new POIDataList();
    // Start is called before the first frame update
    void Start()
    {
        ReadJson();
        StartCoroutine(RequestCoroutine());
    }

    IEnumerator RequestCoroutine()
    {
        UnityWebRequest data = UnityWebRequest.Get(url);
        yield return data.SendWebRequest();
        //Debug.Log(data.downloadHandler.text);
    
        // 데이터 로드 실패시
        switch (data.result)
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
        if (data.isDone)
        {
            string json = data.downloadHandler.text;
            DisplayText(json);
        }
    }

    private void DisplayText(string json)
    {
        string[] rows = json.Split('\n');
        for (int i = 0; i < rows.Length; i++)
        {
            string[] columns = rows[i].Split('\t');
            foreach(var column in columns)
            {
                Debug.Log("line "+ i + ": "+ column);
            }
        }
        
    }

    void ReadJson()
    {
        //                  Asset 폴더
        //string path = Application.dataPath + "04. Resources/ROIInfo.json";
        //string json = File.ReadAllText(path);
        //Debug.Log(json);

        //Resources 폴더 사용시 
        TextAsset json = Resources.Load<TextAsset>("ROIInfo");
        data = JsonUtility.FromJson<POIDataList>(json.text);

        Debug.Log(data.pois[1].name);
    }
}
