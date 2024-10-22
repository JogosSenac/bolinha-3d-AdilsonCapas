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
    [SerializeField] private float desaceleracao = 2f; // Controle de desaceleração
    private bool olhandoParaTras = false; // Controle de estado para saber se o jogador está olhando para trás
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

    void Update()
    {
        // Captura o input de movimento horizontal e vertical
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        // Calcula a direção de movimento baseada na orientação atual do jogador
        Vector3 moveDirection = (transform.forward * moveV + transform.right * moveH).normalized;

        // Se o jogador estiver olhando para trás, inverta a direção do movimento
        if (olhandoParaTras)
        {
            moveDirection = -moveDirection;
        }

        // Aplica a movimentação apenas se houver input
        if (moveDirection.magnitude > 0)
        {
            rb.velocity = new Vector3(moveDirection.x * velocidade, rb.velocity.y, moveDirection.z * velocidade);
        }
        else
        {
            // Aplicar desaceleração ao liberar os botões de movimento
            rb.velocity = new Vector3(rb.velocity.x * (1 - desaceleracao * Time.deltaTime), rb.velocity.y, rb.velocity.z * (1 - desaceleracao * Time.deltaTime));
        }

        // Verifica se a tecla "P" foi pressionada
        if (Input.GetKeyDown(KeyCode.P) && !olhandoParaTras)
        {
            // Vira o jogador diretamente para trás (90 graus em relação ao inicial de -90)
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            olhandoParaTras = true;
        }

        // Verifica se a tecla "P" foi solta
        if (Input.GetKeyUp(KeyCode.P) && olhandoParaTras)
        {
            // Retorna o jogador à rotação original de -90 graus
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            olhandoParaTras = false;
        }

        // Pular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
        }

        // Trocar de cena ao atingir 10 pontos
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
