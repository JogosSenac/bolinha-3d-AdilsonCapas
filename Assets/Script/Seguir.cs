using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seguir : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float moveSpeed = 3f; // Velocidade de movimento do boneco
    private bool shouldFollow = false; // Variável para controlar quando seguir
    public Button botao;

    void Start()
    {
        botao.gameObject.SetActive(false);
         StartCoroutine(Botao(4f));
    }

    void Update()
    {
        if (shouldFollow && player != null)
        {
            // Calcula a direção na qual o boneco deve se mover para seguir o jogador
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); // Normaliza para obter um vetor de direção unitário

            // Move o boneco na direção calculada com a velocidade especificada
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    public IEnumerator Botao(float A)
    {
         yield return new WaitForSeconds(A);
         botao.gameObject.SetActive(true);
    }

    // Método para ser chamado quando o botão for clicado
    public void StartFollowing()
    {
        shouldFollow = true;
        botao.gameObject.SetActive(false);
    }
}
