using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedGain = 0.2f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;

    // Update is called once per frame
    void Update()
    {
        speed += speedGain * Time.deltaTime;

        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.CompareTag("Obstacle")) {
            Debug.Log(message: "wtf ", obj);
            SceneManager.LoadScene("Scene_Main_Menu"); // or 0 as its the first scene
        }
    }

    public void Steer(int value) 
    {
        steerValue = value;
    }
}
