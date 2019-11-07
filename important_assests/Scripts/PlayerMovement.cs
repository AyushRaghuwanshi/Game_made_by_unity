using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{   
    public Canvas canvas;
    private Rigidbody rigidbody;
    public int velocity_z;
    public int velocity_x;
    bool is_change = true;
    int size = 1;
    int shape = 1;
    public int force;
    public Camera camera;
    public int offset;
    bool is_jump = true;
    private AudioSource audiosource;
    private bool is_camera = true;
    public Button forward;
    public Button backward;
    public Button left;
    public Button right;
    public Button jump;
    public Button change;

    //Start is called before the first frame update
    void Start()
    {   
        //buttons binding for mobile inputs .for future implemetation.
        if (forward != null)
        {
            forward.onClick.AddListener(MoveForward);
            backward.onClick.AddListener(MoveBackward);
            left.onClick.AddListener(MoveLeft);
            right.onClick.AddListener(MoveRight);
            jump.onClick.AddListener(Jump);
            change.onClick.AddListener(DoChange); 
        }
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
        if(camera == null)
        {
            is_camera = false;
        }
        
    }



    private void Jump()
    {
        if (is_jump == true)
        {
            rigidbody.AddForce(0, force, 0);
            audiosource.PlayOneShot(audiosource.clip);
        }
    }

    private void MoveRight()
    {
        rigidbody.velocity = new Vector3(velocity_x, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    private void MoveLeft()
    {
        rigidbody.velocity = new Vector3(-velocity_x, rigidbody.velocity.y, rigidbody.velocity.z);
    }

    private void MoveBackward()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -1 * velocity_z);
    }

    private void MoveForward()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, velocity_z);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
   
        
        if(is_change == true)
        {
            ChangePlayer();
        }

        if(is_camera == true)
        {
            CameraOffset();
        }
        
    }

    private void CameraOffset()
    {
        camera.transform.position = new Vector3(transform.position.x, camera.transform.position.y, transform.position.z - offset);
    }

    private void ChangePlayer()
    {
        if (Input.GetKeyDown(KeyCode.C))
            DoChange();
    }

    private void DoChange()
    {
        size = ((size + 1) % 3) + 1;
        if (size == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            force = 400;
            velocity_z = 9;
            velocity_x = 5;
            rigidbody.mass = 1;

        }
        else if (size == 2)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            force = 500;
            velocity_z = 12;
            velocity_x = 7;
            rigidbody.mass = 1;
        }
        else
        {
            transform.localScale = new Vector3(2, 2, 2);
            force = 1600;
            velocity_z = 7;
            velocity_x = 5;
            rigidbody.mass = 5;
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W) )
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, velocity_z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, -1*velocity_z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.velocity = new Vector3(-velocity_x, rigidbody.velocity.y, rigidbody.velocity.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.velocity = new Vector3(velocity_x, rigidbody.velocity.y, rigidbody.velocity.z);
        }
        if (Input.GetKeyDown(KeyCode.Space) && is_jump == true){
            rigidbody.AddForce(0, force, 0);
            audiosource.PlayOneShot(audiosource.clip);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "change_scene")
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string nextScene = "";
            int length = sceneName.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    char index = sceneName[length - 1];
                    int index_int = (int)index;
                    index_int++;
                    index = (char)index_int;
                    nextScene = nextScene + index;
                    break;
                }

                nextScene = nextScene + sceneName[i];

            }
            SceneManager.LoadScene(nextScene);
        }
      
        
       
        
       
        if(collision.gameObject.tag == "ground")
        {
            is_change = false;
            Destroy(GameObject.FindGameObjectWithTag("change"));
        }
    }

    void OnCollisionExit(Collision collision)
    {

        
          
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "change")
        {
            is_jump = false;
        }

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ground" || collision.gameObject.tag == "change")
        {
            is_jump = true;
        }
    }


}
