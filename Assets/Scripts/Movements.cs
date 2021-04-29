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
           Launch();
        }
       
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            Rotate(rotationalSpeed, rightEngineThrusterParticles);
        }
        if(Input.GetKey(KeyCode.D))
        {
          Rotate(-rotationalSpeed, leftEngineThrusterParticles);
        }
    }

    void ApplyRotation(float rotationSpeed)
    {
        rb.freezeRotation = true; //freeze our rotation when bumped to something, so we can manually rotate
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        rb.freezeRotation = true;  //unfreeze our rotation after  manual control is taken.
    }

    void Rotate(float speed, ParticleSystem particle)
    {
        ApplyRotation(speed);
        if(!particle.isPlaying)
            particle.Play();
        else
            particle.Stop();
    }

    void Launch() 
    {
        if(!audioSource.isPlaying || !mainEngineParticles.isPlaying){
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
