using UnityEngine;

[System.Serializable]
public class POI
{
    public string name;
    public string type;
    public string image;
    public float latitude;
    public float longitude;
    public string address;
    public string description;
}

[System.Serializable]
public class POIList
{
    public POI[] pois;
}


