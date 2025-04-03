using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly collision detected.");
                break;
            case "Finish":
                Debug.Log("W");
                // Damage the enemy
                break;
            default:
                Debug.Log("GG");
                break;
        }
    }
}
