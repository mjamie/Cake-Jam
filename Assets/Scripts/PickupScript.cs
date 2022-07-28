using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    float speed = 1f;
    float height = 0.002f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        //bob up and down
        
        Vector3 pos = transform.position;
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            if (gameObject.tag == "Heart")
            {
                //increase player health
            }
            else
            {
                other.GetComponent<PlayerPickUpScript>().pickups--; //decrease pickup - make specific for each pickup
            }
            Destroy(gameObject);
        }
    }
}
