using UnityEngine; 
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate() {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust() {
        if (thrust.IsPressed()) {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }

    private void ProcessRotation() {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        else if(rotationInput > 0) {
            ApplyRotation(-rotationStrength);
        } 
    }

    private void ApplyRotation(float rotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
