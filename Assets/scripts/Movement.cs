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
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(engineSound);
            }
            if(!mainEngineParticles.isPlaying) 
                mainEngineParticles.Play();
        } else {
            mainEngineParticles.Stop();
            audioSource.Stop();
        }
    }

    private void ProcessRotation() {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
            if(!rightEngineParticles.isPlaying) 
                leftEngineParticles.Stop();
                rightEngineParticles.Play();
        } else if(rotationInput > 0) {
            ApplyRotation(-rotationStrength);
            if(!leftEngineParticles.isPlaying) 
                rightEngineParticles.Stop();
                leftEngineParticles.Play();
        } else {
            rightEngineParticles.Stop();
            leftEngineParticles.Stop();
        }   
    }

    private void ApplyRotation(float rotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
