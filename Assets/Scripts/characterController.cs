using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody rb;

    public GameObject character;
    public GameObject reinicio;

    [SerializeField] private float dashForce;
    [SerializeField] private float dashCD;
    [SerializeField] private float TornadoCD;
    [SerializeField] private GameObject escudo;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        escudo.SetActive(false);
    }
    
    #region Dash Code : Aqui esta la lógica del Dash
    
    public void Dash()
    {
        _animator.SetBool("isDashing", true);
        rb.AddForce(transform.forward * dashForce);
        Invoke("StopDash", dashCD);
    }
    private void StopDash()
    {
        _animator.SetBool("isDashing", false);
        Invoke("ResetPosition",0.5f);
    }

    private void ResetPosition()
    {
        character.transform.position = reinicio.transform.position;
    }
    #endregion


    public void Tornado()
    {
        _animator.SetBool("CastTornado", true);
       
        Invoke("StopTornado", TornadoCD);
    }
    private void StopTornado()
    {
        _animator.SetBool("CastTornado", false);
        
    }

    public IEnumerator StartShield()
    {

        _animator.SetBool("Shield", true);
        escudo.SetActive(true);
        yield return new WaitForSeconds(2);
        escudo.SetActive(false);
        _animator.SetBool("Shield", false);
    }
    public void ShieldCoroutine()
    {
        StartCoroutine(StartShield());
    }

}
