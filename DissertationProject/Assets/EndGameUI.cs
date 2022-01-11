using UnityEngine;
using System.Runtime.InteropServices;
using TMPro;

public class EndGameUI : MonoBehaviour
{
    [DllImport("__Internal")]
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
    }
}
