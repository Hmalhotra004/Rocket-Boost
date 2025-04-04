using UnityEngine; 
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip engineSound;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;

    [SerializeField] float thrustStrength = 100f;
    [SerializeField] float rotationStrength = 100f;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            StartThrusting();
        }
        else 
            StopThrusting();   
    }

    private void StartThrusting() {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying) 
            audioSource.PlayOneShot(engineSound);

        if (!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }

    private void StopThrusting() {
        mainEngineParticles.Stop();
        audioSource.Stop();
    }

    private void ProcessRotation() {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0) {
            RotateRight();
        }
        else if(rotationInput > 0) {
            RotateLeft();
        }
        else {
            StopRotating();
        }

    }

    void RotateLeft() {
        ApplyRotation(-rotationStrength);
        if (!leftEngineParticles.isPlaying){
            rightEngineParticles.Stop();
            leftEngineParticles.Play();
        }
    }

    private void StopRotating() {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }

    private void RotateRight() {
        ApplyRotation(rotationStrength);
        if (!rightEngineParticles.isPlaying) {
            leftEngineParticles.Stop();
            rightEngineParticles.Play();
        }
    }

    private void ApplyRotation(float rotate) {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
