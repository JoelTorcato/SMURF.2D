using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Life : MonoBehaviour
{

    private Animator anim; // Invês de usarmos o nome 'Animator' inteiro, usamos 'anim'

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>(); // conecta a variável anim ao componente Animator
    }

    private void OnCollissionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Trap")) {
            Die();
        }
    }

    priate void Die() {

    }
}