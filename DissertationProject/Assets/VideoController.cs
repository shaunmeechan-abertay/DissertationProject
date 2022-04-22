using UnityEngine;
using UnityEngine.Video;
public class VideoController : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer videoPlayer;
    void Start()
    {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "DissMainMenu.mp4");
            videoPlayer.Play();
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
        }
    }
}
