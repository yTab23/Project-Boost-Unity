using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticle;


    AudioSource audioSource;

    bool collisionDisabled = false;
    bool isTransitioning = false;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        RespondToDebugKeys();    
    }

    void  RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }

    }

    void OnCollisionEnter(Collision other) 
    {   
        if(isTransitioning || collisionDisabled){ return ;}

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

        // todo add particle effect upon crash
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
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
        // todo add particle effect upon crash
        isTransitioning = true;
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
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
