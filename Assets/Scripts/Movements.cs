using UnityEngine;

public class Movements : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationalSpeed = 1f;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem rightEngineThrusterParticles;
    [SerializeField] ParticleSystem leftEngineThrusterParticles;
    [SerializeField] ParticleSystem mainEngineParticles;

    Rigidbody rb;
    AudioSource audioSource;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            
            if(!audioSource.isPlaying || !mainEngineParticles.isPlaying)            {
                audioSource.PlayOneShot(thrustAudio);
                mainEngineParticles.Play();
            }
            else
            {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
       
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationalSpeed);
            if(!rightEngineThrusterParticles.isPlaying)
                rightEngineThrusterParticles.Play();
        }
        if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationalSpeed);
            if(!leftEngineThrusterParticles.isPlaying)
                leftEngineThrusterParticles.Play();
        }
    }

    void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true; //freeze our rotation when bumped to something, so we can manually rotate
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = true;  //unfreeze our rotation after  manual control is taken.
    }
}
