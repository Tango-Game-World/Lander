using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    SceneManager scene;
    [SerializeField] float delayReload = 1f;
    [SerializeField] float delayNextLevel = 1.5f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip successAudio;
    [SerializeField] int fuelLevel = 0;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    MeshRenderer rocketRenderer;

    bool isTransitioning = false;
    void OnCollisionEnter(Collision collision)
    {  
        audioSource = GetComponent<AudioSource>();
        rocketRenderer = GetComponent<MeshRenderer>();
        if(isTransitioning)
        {
            return;
        }
        else
        {
            switch (collision.gameObject.tag)
            {
                case "fuel": 
                    fuelLevel++;
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                case "friendly":
                    Debug.Log("am still have'nt took off");
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }
    
    void StartCrashSequence()
    {
        
        //reduce fuel level, if fuel level is 0, go to level 1
        
        isTransitioning = true;
        audioSource.Stop();
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(crashAudio);
            crashParticle.Play();

        GetComponent<Movements>().enabled = false;
        Invoke("ReloadLevel", delayReload);
        fuelLevel--;
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(successAudio);
            successParticle.Play();
        GetComponent<Movements>().enabled = false;
        Invoke("LoadNextLevel", delayNextLevel);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(levelIndex == SceneManager.sceneCountInBuildSettings)
            levelIndex = 0;
        SceneManager.LoadScene(levelIndex);
    }
}
