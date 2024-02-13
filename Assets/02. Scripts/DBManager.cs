using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    struct POIdataList
    {
        public List<POIdata> pois;
        //public POIdata[] pois2 = new POIdata[2];
    }
    POIdataList data = new POIdataList();
    // Start is called before the first frame update
    void Start()
    {
        //string path = Application.dataPath + "Resources/ROIInfo.json";
        TextAsset json = Resources.Load<TextAsset>("ROIInfo");
        data = JsonUtility.FromJson<POIdataList>(json.ToString());
        //data = JsonUtility.FromJson<POIdataList>(json.text);

        Debug.Log(data.pois.Count);
        //Debug.Log(data.pois.Length);
    }
}
