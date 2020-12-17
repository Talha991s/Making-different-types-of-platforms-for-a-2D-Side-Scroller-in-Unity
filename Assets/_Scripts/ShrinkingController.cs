/*
 Filename: ShrinkingController.cs
 Author: Salick Talhah
 Student Number: 101214166
 Date last modified: 17/12/2020
 Description: This script control the sound and functionality of the shrinking platform..
 Revision History: 17/12/2020
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingController : MonoBehaviour
{
    private bool isShinking;
    private bool isMoving;
    private Vector3 Pos;
    private Vector3 Scale;
    public AudioClip Shrinking;
    public AudioClip Expanding;

    // Start is called before the first frame update
    void Start()
    {
        //transform
        Pos = this.transform.position;
        Scale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving)
        {
            if(this.transform.position.y > Pos.y + 0.2f)
            {
                isMoving = true;
            }
            this.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f * Time.deltaTime, transform.position.z);

        }
        if(isMoving)
        {
            if (this.transform.position.y < Pos.y - 0.2f)
            {
                isMoving = false;
            }
            this.transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f * Time.deltaTime, transform.position.z);
        }

        if(isShinking)
        {
            if(this.transform.localScale.x > 0)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x - 0.35f * Time.deltaTime, this.transform.localScale.y - 0.35f * Time.deltaTime, this.transform.localScale.z);
            }
            if(this.transform.localScale.x <= 0)
            {
                isShinking = false;
            }

            this.GetComponent<AudioSource>().volume = 1;
        }

        if(!isShinking)
        {
            if(this.transform.localScale.x < Scale.x)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.2f * Time.deltaTime, this.transform.localScale.y + 0.2f * Time.deltaTime, this.transform.localScale.z);
            }
        }

        if (this.transform.localScale.x >= Scale.x)
        {
            this.GetComponent<AudioSource>().volume = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isShinking = true;
            this.GetComponent<AudioSource>().clip = Shrinking;
            this.GetComponent<AudioSource>().Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isShinking = false;
            this.GetComponent<AudioSource>().clip = Expanding;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
