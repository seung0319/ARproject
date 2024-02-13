using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    struct POIDataList
    {
        public List<POIdata> pois;
    }
    POIDataList data = new POIDataList();
    // Start is called before the first frame update
    void Start()
    {
        //                  Asset Æú´õ
        //string path = Application.dataPath + "04. Resources/ROIInfo.json";

        TextAsset json = Resources.Load<TextAsset>("ROIInfo");
        data = JsonUtility.FromJson<POIDataList>(json.text);

        Debug.Log(data.pois[1].name);
    }
}
