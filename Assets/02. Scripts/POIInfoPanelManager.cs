using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class POIInfoPanelManager : MonoBehaviour
{
    public static POIInfoPanelManager instance = null;
    public GameObject panel;
    public Text nameText;
    public Text typeText;
    public Text imageText;
    public Text latitudeText;
    public Text longitudeText;
    public Text addressText;
    public Text descriptionText;

    public void SetPanel(POI poi)
    {
        nameText.text = poi.name;
        typeText.text = poi.type;
        imageText.text = poi.image;
        latitudeText.text = poi.latitude.ToString();
        longitudeText.text = poi.longitude.ToString();
        addressText.text = poi.address;
        descriptionText.text = poi.description;
    }
}
