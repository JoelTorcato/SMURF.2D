using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player_Life : MonoBehaviour
{

    private Animator anim; // Invés de usarmos o nome 'Animator' inteiro, usamos 'anim'
    private Rigidbody2D rb; // Invés de usarmos o nome 'Rigidbody2D' inteiro, usamos 'rb'

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // conecta a variável rb ao componente Rigidbody2D
        anim = GetComponent<Animator>(); // conecta a variável anim ao componente Animator
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;  // Esse código altera o tipo do corpo rígido 'Rigidbody' de um objeto 2D para estático.
        anim.SetTrigger("death");
    }

    private void RestartLever() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Esse código recarrega a cena atual no Unity, reiniciando tudo nela desde o início.
    }
}
