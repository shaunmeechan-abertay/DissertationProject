using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using TMPro;

public class EndGameUI : MonoBehaviour
{
#if UNITY_ANDROID
    [DllImport("WebCopyText.jslib")]
#else
    [DllImport("__Internal")]
#endif
    private static extern void CopyText(string str);

    public GUIDHelper GUIDHelper;
    public TextMeshProUGUI GUIDText;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GUIDText.text = GUIDHelper.getUIDAsString();    
    }

    public void copyGUIDToClipboard()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            CopyText(GUIDText.text);
        }
        else
        {
            GUIUtility.systemCopyBuffer = GUIDText.text;
        }

        Application.OpenURL("https://forms.gle/DLW5NCTGqpohUhy76");
    }
}
