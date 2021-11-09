using UnityEngine;
using UnityEngine.Rendering;

public class SRPController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            GraphicsSettings.renderPipelineAsset = null;
            Debug.Log("We are on WebGL so use the build in renderer");
        }
        else
        {
            Debug.Log("Using default render pipeline asset: " + GraphicsSettings.renderPipelineAsset.name);
            Destroy(gameObject);
        }
    }
}
