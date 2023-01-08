using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        // Debug.Log("starting position is " + startingPosition);
    }

    // Update is called once per frame
    void Update()
    {
        OscillateObstacle();
    }

    private void OscillateObstacle()
    {
        if(period<=Mathf.Epsilon) return;
        else {
            const float tau = Mathf.PI * 2; // constant value of 6.283 
            float cycles = Time.time / period; // continually growing over time
            float rawSinWave = Mathf.Sin(cycles * tau); // A number between -1 and 1
            movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so its cleaner
            Vector3 offset = movementFactor * movementVector;
            transform.position = startingPosition + offset;
        }
    }
}
