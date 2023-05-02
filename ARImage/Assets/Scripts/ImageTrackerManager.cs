using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackerManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;
    private VideoPlayer videoPlayer;
    private bool isImageTrackable;

    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;

    }private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs evenData)
    {
        foreach (var trackedImage in evenData.added)
        {
            videoPlayer = trackedImage.GetComponentInChildren<VideoPlayer>();
            videoPlayer.Play();
        }

        foreach (var trackedImage in evenData.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if(!isImageTrackable)
                {
                    isImageTrackable = true;
                    videoPlayer.gameObject.SetActive(true);
                    videoPlayer.Play();
                }
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                if (isImageTrackable)
                {
                    isImageTrackable = false;
                    videoPlayer.gameObject.SetActive(false);
                    videoPlayer.Pause();
                }
            }
        }
    }
}
