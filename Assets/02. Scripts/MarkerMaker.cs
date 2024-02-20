using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMaker : MonoBehaviour
{
    public GameObject buttonPrefab; // Button Prefab을 Inspector에서 할당해주세요.
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
        GameObject parent = GameObject.Find("Map"); // Map 게임오브젝트 찾기
        if (parent != null) // Map 게임오브젝트가 존재하는 경우에만 버튼 생성
        {
            button = Instantiate(buttonPrefab, position, Quaternion.identity);
            button.transform.SetParent(parent.transform, false); // Map을 부모로 설정
        }
        else
        {
            Debug.LogError("Map 게임오브젝트를 찾을 수 없습니다."); // Map 게임오브젝트가 없는 경우 오류 메시지 출력
        }
    }

    private Vector3 ConvertGeoToUnityCoordinate(float latitude, float longitude)
    {
        // 기준 위도, 경도
        float originLatitude = 37.7137920f;
        float originLongitude = 126.7442360f;
        // 경기인력개발원
        // 37.7137920f;
        // 126.7442360f;
        // 랜덤위치
        // 37.711892f;
        // 126.745209f;

        // 기준 x, y
        float originX = 0;
        float originY = 0;

        // 위도, 경도에 대한 x, y의 변화 비율
        float xRatio = 559092.4f;
        float yRatio = 714178.2f;
        Debug.Log(xRatio + " " + yRatio);
        //559092.4 714178.2
        // 위도, 경도를 x, y로 변환
        float x = originX + (longitude - originLongitude) * xRatio;
        float y = originY + (latitude - originLatitude) * yRatio;
        

        return new Vector2(x, y);
    }
}
