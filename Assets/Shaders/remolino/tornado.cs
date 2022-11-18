using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    
    //public Material tornado1
    
    public ParticleSystem tornado1;
    public ParticleSystem tornado2;
    public float tornadocolddown;
    public Material material1;
    public Material material2;
    private float timer;
    private bool desaparce= false;



    // Start is called before the first frame update
    void Start()
    {


        material1.SetFloat("dissolve", timer);

        material2.SetFloat("dissolve", timer);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material1.SetFloat("dissolve", timer);
        material2.SetFloat("dissolve", timer);
       
        if (timer <= .79f) { timer = 0.8f; }
        if (timer == .8f)
        {
            desaparce = true;
        }
        else if (timer >= 1f)
        {
            desaparce = false;
        }
        if (desaparce == true) { timer += Time.deltaTime / 64; }
        else if (desaparce== false )
        {
            timer -= Time.deltaTime / 8;
        }
    }


    public void iniciartonado()
    {
        timer = 1f;
        tornado1.Play();
        tornado2.Play();
        
        
        Invoke("parartornado", tornadocolddown);
        
    }


    public void parartornado()
    {

        tornado1.Stop();
        tornado2.Stop();
    }

    
}
