using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Dababy3 : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    [SerializeField] private float desaceleracao = 2f;
    [SerializeField] private AudioClip pulo;
    [SerializeField] private AudioClip morte;
    public AudioSource audioPlayer;
    private bool olhandoParaTras = false;
    int pontos = 0;
    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;
    
    // Nova variável de controle de movimento
    public bool podeMover = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
    }

    void Update()
    {
        // Verifica se o jogador pode se mover antes de permitir o movimento
        if (!podeMover) return;

        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = (transform.forward * moveV + transform.right * moveH).normalized;

        if (olhandoParaTras)
        {
            moveDirection = -moveDirection;
        }

        if (moveDirection.magnitude > 0.1)
        {
            rb.velocity = new Vector3(moveDirection.x * velocidade, rb.velocity.y, moveDirection.z * velocidade);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 - desaceleracao * Time.deltaTime), rb.velocity.y, rb.velocity.z * (1 - desaceleracao * Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.P) && !olhandoParaTras)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            olhandoParaTras = true;
        }

        if (Input.GetKeyUp(KeyCode.P) && olhandoParaTras)
        {
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            olhandoParaTras = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
            audioPlayer.PlayOneShot(pulo);
        }

        if (pontos == 10)
        {
            SceneManager.LoadScene("Fase2Menu");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CuboBrilhante"))
        {
            other.enabled = false;
            pontos++;
            textoPontos.text = pontos.ToString();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Agua"))
        {
            audioPlayer.PlayOneShot(morte);
            StartCoroutine(Morte(0.5f));
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            audioPlayer.PlayOneShot(morte);
            Destroy(gameObject);
            SceneManager.LoadScene("Morte3");
        }
        else if (other.gameObject.CompareTag("Chão"))
        {
            SceneManager.LoadScene("Vitoria");
        }
    }
    private IEnumerator Morte(float delay)
    {
        yield return new WaitForSeconds(delay); 

        SceneManager.LoadScene("Morte3");
        Destroy(gameObject);
    }    
}
