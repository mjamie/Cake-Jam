using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpScript : MonoBehaviour
{
    public int pickups = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Remaining: " + pickups);
    }
}
