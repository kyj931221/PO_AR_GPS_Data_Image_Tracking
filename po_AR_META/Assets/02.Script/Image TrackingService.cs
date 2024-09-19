using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTrackingService : MonoBehaviour
{
    public ARTrackedImageManager manager;

    private void OnEnable()
    {
        manager.trackedImagesChanged += OnChanged; 
    }

    private void OnDisable()
    {
        manager.trackedImagesChanged -= OnChanged;
    }

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(ARTrackedImage t in eventArgs.added)
        {
            Vector2 posGPS = GetComponent<GPSService>().GetGPSInfo();

            GetComponent<DBService>().CreateStoreCharacter(posGPS, t.transform);
        }

        foreach(ARTrackedImage t in eventArgs.updated)
        {

        }
    }
}
