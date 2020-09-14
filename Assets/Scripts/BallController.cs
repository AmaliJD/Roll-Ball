using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    private Rigidbody body;
    private float maxSpeed = 50;
    private float jumpForce = 500;
    private float amt = 20;
    private bool grounded = false;

    public string up;
    public string down;
    public string left;
    public string right;
    public string jump;

    public Text score_text, points;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = 0, moveZ = 0;
        if(score < 0) { score = 0; }
        score_text.text = "Score: " + score;

        if(body.velocity.y < 0f) { body.velocity = new Vector3(body.velocity.x, body.velocity.y * 1.05f, body.velocity.z); }
        else if (body.velocity.y < 6f) { body.velocity = new Vector3(body.velocity.x, body.velocity.y / 4f, body.velocity.z); }

        if (Input.GetKey(left))
        {
            moveX = -amt;
        }

        if(Input.GetKey(right))
        {
            moveX = amt;
        }

        if (Input.GetKey(up))
        {
            moveZ = amt;
        }

        if (Input.GetKey(down))
        {
            moveZ = -amt;
        }
        
        if(Input.GetKey(jump) && grounded)
        {
            grounded = false;
            body.AddForce(new Vector3(0, jumpForce, 0));
        }

        if(Input.GetKeyUp(jump) && body.velocity.y > 0)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y/2, body.velocity.z);
        }

        body.AddForce(new Vector3(moveX, 0, moveZ));

        if(body.velocity.x >= maxSpeed) { body.velocity = new Vector3(maxSpeed, body.velocity.y, body.velocity.z); }
        else if (body.velocity.x <= -maxSpeed) { body.velocity = new Vector3(-maxSpeed, body.velocity.y, body.velocity.z); }
        if (body.velocity.z >= maxSpeed) { body.velocity = new Vector3(body.velocity.x, body.velocity.y, maxSpeed); }
        else if (body.velocity.z <= -maxSpeed) { body.velocity = new Vector3(body.velocity.x, body.velocity.y, -maxSpeed); }
    }

    public int getScore()
    {
        return score;
    }

    private IEnumerator Points(string message, Color color)
    {
        float time = 0;
        points.text = message;
        points.color = color;

        while (time < 1)
        {
            points.color = Color.Lerp(points.color, Color.clear, 1f * Time.deltaTime);

            time += Time.deltaTime;
            yield return null;
        }

        points.color = Color.clear;
    }

    private IEnumerator CubeRespawn(GameObject cube)
    {
        float time = 0;

        while(time < 10)
        {
            time += Time.deltaTime;
            yield return null;
        }

        cube.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
        if (collision.gameObject.tag == "wall")
        {
            score--;

            StopCoroutine("Points");
            points.color = Color.clear;
            StartCoroutine(Points("Collision -1", Color.red));
        }
        if (collision.gameObject.tag == "p1" || collision.gameObject.tag == "p2")
        {
            grounded = true;

            if (collision.gameObject.transform.position.y - transform.position.y >= .1f)
            {
                score--;
            }
            else if (collision.gameObject.transform.position.y - transform.position.y <= -.1f)
            {
                score++;

                StopCoroutine("Points");
                points.color = Color.clear;
                StartCoroutine(Points("Footstool +1", Color.yellow));
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "cube")
        {
            score++;
            collider.gameObject.SetActive(false);
            StartCoroutine(CubeRespawn(collider.gameObject));
        }
    }
}
