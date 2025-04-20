using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool played = false;
    public GameObject menus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (GridBehaviorUI.connectedNodes >= GridBehaviorUI.staticNumPairs && played == false)
        {
            Debug.Log("Connected Nodes: " + GridBehaviorUI.connectedNodes);
            Debug.Log("Number of Pairs: " + GridBehaviorUI.staticNumPairs);
            gameObject.SetActive(true);
            menus.SetActive(false);
            videoPlayer.Play();
            played = true;
        }
       
    }

    private void OnVideoEnd(VideoPlayer videoplayer)
    {
        gameObject.SetActive(false);
        menus.SetActive(true);
    }
}
