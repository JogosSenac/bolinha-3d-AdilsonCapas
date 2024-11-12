using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dababy : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip pegaCubo;
    [SerializeField] private AudioClip morte;
    public AudioSource audioPlayer;
    int pontos = 0;
    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveH * velocidade * Time.deltaTime,
                                           0,
                                           moveV * velocidade *Time.deltaTime);
                                
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
            audioPlayer.PlayOneShot(pulo);
        }

        if( pontos == 14)
        {
            SceneManager.LoadScene("Fase2Menu");
        }

    }

   private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("CuboBrilhante"))
    {
        // Disable the collider to prevent multiple triggers
        other.enabled = false;

        pontos++;
        textoPontos.text = pontos.ToString();
        audioPlayer.PlayOneShot(pegaCubo);
        Destroy(other.gameObject);
        VerificaObjetivos();
    }
    else if (other.gameObject.CompareTag("Lava"))
    {
        audioPlayer.PlayOneShot(morte);
        StartCoroutine(Morte(0.5f));
        
        
       
    }
}

private void VerificaObjetivos()
    {
        int totalCubos = Int32.Parse(textoTotal.text);
        TextMeshProUGUI objetivo = GameObject.Find("Objetivo").GetComponent<TextMeshProUGUI>();
        Debug.LogFormat($"Pontos: {pontos}, Total Cubos: {totalCubos}");
        
        if(pontos < totalCubos)
        {
            objetivo.text = "Pegue todos os óleos!";
        }
        
        if(pontos >= totalCubos / 2)
        {
            objetivo.text = "Continue assim!";
        }
        
        if(pontos >= totalCubos - 5)
        {
            objetivo.text = "Quase no fim!";
        }
        
        if(pontos == totalCubos)
        {
            objetivo.text = "Todos os óleos coletados!";
        }
    }

private IEnumerator Morte(float delay)
    {
        yield return new WaitForSeconds(delay); 

        SceneManager.LoadScene("Morte");
        Destroy(gameObject);
    }    

}
