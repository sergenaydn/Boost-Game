using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    [SerializeField] AudioClip SUCC;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem succParticle;
    [SerializeField] ParticleSystem crashParticle;
    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();       

    }

    void Update()
    {
        RespondTodDebugKeys();
    }
    void RespondTodDebugKeys() 
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisabled) 
        {
            return;
        }
        switch(other.gameObject.tag )
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                StartSuccessSequence(); 
                Debug.Log("Congrats, you finished");
                break;
            default:
                startCrash();
                audioSource.PlayOneShot(crashSound);
                Debug.Log("You Fucked Up"); 
                break;

        }
    }

     void StartSuccessSequence()
    { 
        isTransitioning = true;
        succParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(SUCC);
        //todo add particle effect upn crash
        GetComponent<Movement>().enabled = false;
        Invoke("nextLevel", loadDelay);
    
    }

    void startCrash()
    {   
        isTransitioning = true;
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        //todo add particle effect upn crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }



    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1; 
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        
    }    
}
