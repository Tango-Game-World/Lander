using UnityEngine;

public class Oscillator : MonoBehaviour
{
  // Start is called before the first frame update
    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0, 1)] float movementFactor; //if we want the bar to show
    float movementFactor = 3;
    [SerializeField] float period = 2f;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}
        float cycle = Time.time / period; // continually growing
        const float tau = Mathf.PI * 2; // constant value 6.28333
        float rawSinWave = Mathf.Sin(cycle * tau); //going from -1 to 1

        movementFactor = (rawSinWave * 1f) / 2; // going from 0 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
