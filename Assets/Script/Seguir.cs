using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguir : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float moveSpeed = 3f; // Velocidade de movimento do boneco

    void Update()
    {
        if (player != null)
        {
            // Calcula a direção na qual o boneco deve se mover para seguir o jogador
            Vector3 direction = player.position - transform.position;
            direction.Normalize(); // Normaliza para obter um vetor de direção unitário

            // Move o boneco na direção calculada com a velocidade especificada
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
