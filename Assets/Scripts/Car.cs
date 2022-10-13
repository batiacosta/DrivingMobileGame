using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedGainPerSecond;
    [SerializeField] private float turnSpeed = 200f;

    private int _steerValue;
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime*speedGainPerSecond);
        speed += speedGainPerSecond * Time.deltaTime;
        
        transform.Rotate(0f, _steerValue * turnSpeed * Time.deltaTime, 0f);
    }

    public void Steer(int steerValue)
    {
        _steerValue = steerValue;
    }
}
