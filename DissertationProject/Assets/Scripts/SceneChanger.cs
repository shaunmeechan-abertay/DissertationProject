//SUMMARY: This script fades a scene out then start to load another in.
//Credit: Loading Async and loading progress: Brackys, https://youtu.be/YMj2qPq9CP8 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    //Reference to the image used to fade out of a scene
    public Image fadeOutImage;
    //Reference to the image used to fade into a scene
    public Image fadeInImage;
    //Should we fade into the current scene?
    private bool shouldFadeIn = true;
    //Should we fade out of the current scene?
    private bool shouldFadeOut = false;
    //What scene do we want to load?
    public int sceneToLoad = 0;
    //Reference to the loading bar slider component
    public Slider slider;
    //Reference to progress text
    public TextMeshProUGUI sliderProgressText;

    // Update is called once per frame
    void Update()
    {
        //Should we fade into the current scene?
        if (shouldFadeIn == true)
        {
            //If yes then decrease the alpha value of the fade in image
            fadeInImage.color = new Color(fadeInImage.color.r, fadeInImage.color.g, fadeInImage.color.b, fadeInImage.color.a - 0.1f);
            if (fadeInImage.color.a <= 0.0f)
            {
                //When it is invisible deactivate it
                shouldFadeIn = false;
                fadeInImage.gameObject.SetActive(false);
            }
        }

        //Should we fade out of the current scene?
        if (shouldFadeOut == true)
        {
            //If so increase the alpha of the fade out image
            fadeInImage.color = new Color(fadeInImage.color.r, fadeInImage.color.g, fadeInImage.color.b, fadeInImage.color.a + 0.1f);
            if (fadeInImage.color.a >= 1.0f)
            {
                //When we are fully faded out start loading the new scene
                shouldFadeOut = false;
                StartCoroutine(LoadAsynchronously());
            }
        }
    }

    //Starts the process of fading out of the current scene
    public void fadeOut()
    {
        fadeOutImage.gameObject.SetActive(true);
        shouldFadeOut = true;
        shouldFadeIn = false;
    }

    //Sets the value of the scene the script will load
    public void setSceneToLoad(int value)
    {
        sceneToLoad = value;
    }

    //Starts the loading the requsted scene asynchronously
    //Also handles moving the loading bar to show the loading progress
    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        float progress = 0.0f;
        while (operation.isDone == false)
        {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            slider.value = progress;
            sliderProgressText.text = progress * 100f + "%";
            yield return null;
        }
    }
}
