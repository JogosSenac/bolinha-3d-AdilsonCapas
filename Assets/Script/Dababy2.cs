using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class Dababy2 : MonoBehaviour
{
    private float moveH;
    private float moveV;
    private Rigidbody rb;
    [SerializeField] private float velocidade;
    [SerializeField] private float forcaPulo;
    int pontos = 0;
    private TextMeshProUGUI textoPontos;
    private TextMeshProUGUI textoTotal;

    public SceneTransition sceneTransition;

    private bool jaRotacionou = false; // Controle se a rotação já foi aplicada

    // Referência para o Animator do objeto que será animado
    public Animator animacaoObjeto;  // Associe o Animator desse objeto no Inspector
    public GameObject objetoParaDestruir; // O objeto que será destruído após a animação

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        textoPontos = GameObject.FindGameObjectWithTag("Pontos").GetComponent<TextMeshProUGUI>();
        textoTotal = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
        textoTotal.text = GameObject.FindGameObjectsWithTag("CuboBrilhante").Length.ToString();
    }

    void Update()
    {
        // Captura o input do jogador
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");

        // Mantendo a movimentação simples
        transform.position += new Vector3(moveH * velocidade * Time.deltaTime,
                                           0,
                                           moveV * velocidade * Time.deltaTime);
                                 
        // Aplicando força de pulo se a tecla Espaço for pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * forcaPulo, ForceMode.Impulse);
        }

        // Checa se o jogador tem 8 pontos e faz a rotação uma única vez
        if (pontos == 22 && !jaRotacionou)
        {
            // Rotaciona a bolinha
            jaRotacionou = true;

            // Ativa a animação do objeto
            animacaoObjeto.SetBool("isAnimating", true);

            // Inicia a coroutine para destruir o objeto e mudar de cena após a animação
            StartCoroutine(DestruirObjetoECarregarCena());
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
        else if(other.gameObject.CompareTag("Finish"))
        {
        sceneTransition.TransitionToScene("Final");
        }
    }

   

    // Coroutine para aguardar o fim da animação, destruir o objeto e mudar de cena
    IEnumerator DestruirObjetoECarregarCena()
{
    // Tempo que você quer esperar antes de destruir o objeto (em segundos)
    float tempoParaEsperar = 3f; // Altere este valor conforme necessário

    // Espera pelo tempo definido
    yield return new WaitForSeconds(tempoParaEsperar);

    GameObject objetoParaDestruir = GameObject.FindWithTag("Parede");
    // Destroi o objeto
    Destroy(objetoParaDestruir);
}
}
