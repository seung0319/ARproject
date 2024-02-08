using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct POIdata
{
    private string name;
    private string description;
    private float latitude;
    private float longitude;
    private float altitude;

    public string Name { get { return name; } }
    public string Description { get { return description; } }
    public float Latitude { get { return latitude; } }
    public float Longitude { get { return longitude; } }
    public float Altitude { get { return altitude; } }


    public POIdata(string name, string description, float latitude, float longitude, float altitude)
    {
        this.name = name;
        this.description = description;
        this.latitude = latitude;
        this.longitude = longitude;
        this.altitude = altitude;
    }
}
