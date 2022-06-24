using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    bool gameReset = false;
    [SerializeField] float levelLoadDelay = 2f;

    void OnCollisionEnter(Collision other) 
    {   
        
        switch (other.gameObject.tag )
        {
            case "Friendly":
            Debug.Log("Friendly");
            break;

            case "Obstacle":
            StartCrashSequence();
            break;

            case "Ground":
            Invoke("StartCrashSequence", levelLoadDelay);
            break;

            case "Finish":
            StartSuccessSequence();
            break;

            default:
            break;
        }
        
    }

    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartSuccessSequence()
    {
        // todo add SFX upon crash
        // todo add particle effect upon crash

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);        
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
