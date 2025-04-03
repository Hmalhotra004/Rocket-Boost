using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Friendly":
                break;
            case "Finish":
                Debug.Log("Level Complete!");
                break;
            default:
            reloadLevel();
                break;
        }
    }

    private void loadNextLevel(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void reloadLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
