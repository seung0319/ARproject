using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMaker : MonoBehaviour
{
    public GameObject buttonPrefab; // Button Prefab�� Inspector���� �Ҵ����ּ���.
    GameObject button;

    private void Start()
    {
        foreach (var poi in DataManager.instance.poiList.pois)
        {
            CreateButtonAtLocation(poi.latitude, poi.longitude);
            POIData poiData = button.GetComponent<POIData>();
            poiData.SetData(poi);
            Debug.Log(poi.name);
        }
        //CreateButtonAtLocation(37.712223f, 126.744613f);
        //CreateButtonAtLocation(37.711977f, 126.746041f);
        //CreateButtonAtLocation(37.7146690f, 126.7416670f);
        //CreateButtonAtLocation(37.7137920f, 126.7442360f);
        //CreateButtonAtLocation(37.712312f, 126.744701f);
    }
    public void CreateButtonAtLocation(float latitude, float longitude)
    {
        Vector3 position = ConvertGeoToUnityCoordinate(latitude, longitude);
        GameObject parent = GameObject.Find("Map"); // Map ���ӿ�����Ʈ ã��
        if (parent != null) // Map ���ӿ�����Ʈ�� �����ϴ� ��쿡�� ��ư ����
        {
            button = Instantiate(buttonPrefab, position, Quaternion.identity);
            button.transform.SetParent(parent.transform, false); // Map�� �θ�� ����
        }
        else
        {
            Debug.LogError("Map ���ӿ�����Ʈ�� ã�� �� �����ϴ�."); // Map ���ӿ�����Ʈ�� ���� ��� ���� �޽��� ���
        }
    }

    private Vector3 ConvertGeoToUnityCoordinate(float latitude, float longitude)
    {
        // ���� ����, �浵
        float originLatitude = 37.7137920f;
        float originLongitude = 126.7442360f;
        // ����η°��߿�
        // 37.7137920f;
        // 126.7442360f;
        // ������ġ
        // 37.711892f;
        // 126.745209f;

        // ���� x, y
        float originX = 0;
        float originY = 0;

        // ����, �浵�� ���� x, y�� ��ȭ ����
        float xRatio = 559092.4f;
        float yRatio = 714178.2f;
        Debug.Log(xRatio + " " + yRatio);
        //559092.4 714178.2
        // ����, �浵�� x, y�� ��ȯ
        float x = originX + (longitude - originLongitude) * xRatio;
        float y = originY + (latitude - originLatitude) * yRatio;
        

        return new Vector2(x, y);
    }
}
