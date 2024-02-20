using UnityEngine;

public class POIButton : MonoBehaviour
{
    public POIData poiData;
    public POIInfoPanelManager displayPOI;

    private void Start()
    {
        displayPOI = FindObjectOfType<POIInfoPanelManager>();
    }
    public void OnClick()
    {
        displayPOI.SetPanel(poiData.GetData());
    }
}