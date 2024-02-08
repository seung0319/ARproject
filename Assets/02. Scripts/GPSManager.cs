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

    POIdata data = new POIdata("재물포시장", "좋아요.", 0,0,0,0);
    List<POIdata> POIlist = new List<POIdata>();
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
            LatitudeText.text = "Latitude: " + latitude;
            LongitudeText.text = "Longitude: " + longitude;
        }
    }
}
