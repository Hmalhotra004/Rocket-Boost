using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem susccessParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isContollable = true;
    bool isCollidable = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys() {
        if(Keyboard.current.lKey.wasPressedThisFrame) {
            loadNextLevel();
        } else if( Keyboard.current.cKey.wasPressedThisFrame) {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(!isContollable || !isContollable) return;

        switch(other.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                startSuccessSequence();
                break;
            default:
                startCrashSequence();
                break;
        }
    }

    private void startSuccessSequence() {
        isContollable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(successSound);
        susccessParticles.Play();
        Invoke("loadNextLevel",levelLoadDelay);
    }

    private void startCrashSequence() {
        isContollable = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        Invoke("reloadLevel", levelLoadDelay);
    }

    private void loadNextLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void reloadLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
