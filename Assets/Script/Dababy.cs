using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dababy : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
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
        }

        if( pontos == 10)
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
        Destroy(other.gameObject);
    }
    else if (other.gameObject.CompareTag("Lava"))
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Morte");
    }
    else if (other.gameObject.CompareTag("Lava2"))
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Morte2");
    }
}

}
