using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{
    public Text p1_score, p2_score;
    private float time = 0, wait = 0;
    private bool up = true;

    private void Start()
    {
        wait = Random.Range(0f, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward, 2f);

        time += Time.deltaTime;
        if(time < wait) { return; }

        if(transform.position.y > 1.95f && up)
        {
            up = false;
        }
        else if (transform.position.y <= .19f && !up)
        {
            up = true;
            time = 0;
        }

        if (up)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 2f, transform.position.z), 3f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, .18f, transform.position.z), 4f * Time.deltaTime);
        }
        
    }
}
