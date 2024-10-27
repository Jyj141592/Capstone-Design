using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    
    void Start()
    {
        
    }
    public void StartTarget(){
        gameObject.SetActive(true);
    }
    public void StopTarget(){
        gameObject.SetActive(false);
    }
    public void Shoot(){

    }
}
