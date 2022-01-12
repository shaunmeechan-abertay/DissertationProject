using UnityEngine;

public class QuitGame : MonoBehaviour
{
    private void Start()
    {
        //Don't show the button if we are on the Web or Android
        if(Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.Android)
        {
            gameObject.SetActive(false);
        }
    }

    //Function to exit the game
    public void exitGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
