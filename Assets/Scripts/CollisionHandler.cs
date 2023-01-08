using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay=1;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start() {
        audioSource=GetComponent<AudioSource>();
    }

    private void Update(){
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)) LoadNextLevel();
        else if(Input.GetKeyDown(KeyCode.C)) DisableCollisions();
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning||collisionDisabled) return;

        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("collided into Friendly");
                audioSource.PlayOneShot(success);
                break;
            case "Fuel":
                Debug.Log("collided into Fuel");
                break;
            case "Finish":
                Debug.Log("Finish");
                // GetComponent<Movement>().enabled=false;
                // Invoke("LoadNextLevel",delay);
                StartSuccessSequence();
                break;
            default:
                Debug.Log("Sorry, you blew up!");
                StartCrashSequence();
                break;
                
        }
        
    }
    private void DisableCollisions()
    {
       collisionDisabled = !collisionDisabled;
    }
    void StartCrashSequence(){
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",delay);
    }
    private void StartSuccessSequence(){
        // throw new NotImplementedException();
        isTransitioning=true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel",delay);
    }
    void ReloadLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadNextLevel(){
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex=currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

}
