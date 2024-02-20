using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIData : MonoBehaviour
{
    public POI poi;

    public void SetData(POI newPOI)
    {
        poi = newPOI;
    }

    public POI GetData()
    {
        return poi;
    }
}
