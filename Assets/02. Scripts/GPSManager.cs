using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

// Summaray : 유저 GPS 기능 사용하기 위한 스크립트

public class GPSManager : MonoBehaviour
{
    public static GPSManager Instance { set; get; }

    public float latitude;
    public float longitude;

    public TextMeshProUGUI LatitudeText; // TextMeshProUGUI를 참조하기 위한 변수
    public TextMeshProUGUI LongitudeText; // TextMeshProUGUI를 참조하기 위한 변수
    public TextMeshProUGUI StatusText; // TextMeshProUGUI를 참조하기 위한 변수
    public TextMeshProUGUI DistanceText; // TextMeshProUGUI를 참조하기 위한 변수

    public List<POIdata> POIlist = new List<POIdata>();
    
    POIdata restaurantData = new POIdata("식당", "좋아요.", 37.714231f, 126.743506f, 0);
    POIdata dormitoryData = new POIdata("기숙사", "좋아요.", 37.714431f, 126.744381f,0);
    POIdata frontdoorData = new POIdata("정문", "좋아요.", 37.714263f, 126.742161f,0);
    
    private void Start()
    {
        Instance = this;
        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }
        }

        Input.location.Start(1, 1); // 업데이트 주기를 1초로, 최소 거리를 1로 지정
        int maxWait = 20;

        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        yield break;
    }

    private void Update()
    {
        StatusText.text = "Status : " + Input.location.status;

        if (Input.location.status == LocationServiceStatus.Running)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            LatitudeText.text = "Latitude: " + latitude.ToString("0.000000");
            LongitudeText.text = "Longitude: " + longitude.ToString("0.000000");

            float radius = 6371f; // 지구의 반지름 (단위: km)

            float lat1Rad = Mathf.Deg2Rad * latitude;
            float lon1Rad = Mathf.Deg2Rad * longitude;
            float lat2Rad = Mathf.Deg2Rad * restaurantData.Latitude;
            float lon2Rad = Mathf.Deg2Rad * restaurantData.Longitude;

            float deltaLat = lat2Rad - lat1Rad;
            float deltaLon = lon2Rad - lon1Rad;

            float a = Mathf.Sin(deltaLat / 2f) * Mathf.Sin(deltaLat / 2f) +
                Mathf.Cos(lat1Rad) * Mathf.Cos(lat2Rad) *
                Mathf.Sin(deltaLon / 2f) * Mathf.Sin(deltaLon / 2f);

            float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));

            float distance = radius * c;

            DistanceText.text = "Distance to Restaurant: " + distance.ToString("0.000000");
        }

    }

    public void addData(string name, string description, float latitude, float longitude, float altitude)
    {
        POIlist.Add(new POIdata(name, description, latitude, longitude, altitude));
    }
}
