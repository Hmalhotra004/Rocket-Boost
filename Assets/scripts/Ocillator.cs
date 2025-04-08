using UnityEngine;

public class Ocillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    Vector3 startPos;
    Vector3 endPos;
    float movementFactor;

    void Start() {
        startPos = transform.position;
        endPos = startPos + movementVector;
    }

    void Update() {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, endPos, movementFactor);
    }
}
