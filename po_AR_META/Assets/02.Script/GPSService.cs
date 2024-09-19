using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GPSService : MonoBehaviour
{
    //public Text textMsg;
    public TextMeshProUGUI textMsg;
   
    IEnumerator Start()
    {
        if(!Input.location.isEnabledByUser) yield break;

        Input.location.Start();

        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if(maxWait < 1)
        {
            textMsg.text = "Timed out";
            yield break;
        }

        if(Input.location.status == LocationServiceStatus.Failed)
        {
            textMsg.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            while(true)
            {
                textMsg.text = "[GPS]" + "\n" 
                    + "Lat : " + Input.location.lastData.latitude + "\n" 
                    + "Log : " + Input.location.lastData.longitude + "\n"
                    + "Hor : " + Input.location.lastData.horizontalAccuracy + "m";

                yield return new WaitForSeconds(1);
            }
        }
        // Input.location.Stop();
    }
    public Vector2 GetGPSInfo()
    {
        Vector2 vPos = new Vector2(Input.location.lastData.latitude,
                                   Input.location.lastData.longitude);
        return vPos;
    }
}
