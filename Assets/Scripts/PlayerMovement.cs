using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D; // Invês de usarmos o nome 'Rigidbody2D' inteiro, usamos 'rb2D'
    private SpriteRenderer sprite; // Invês de usarmos o nome 'SpriteRenderer' inteiro, usamos 'sprite'
    private Animator anim; // Invês de usarmos o nome 'Animator' inteiro, usamos 'anim'

    private float dirX = 0f; // ?
    [SerializeField] private float moveSpeed = 7f; // [SerializeField] torna uma variável privada visível no Inspector
    [SerializeField] private float jumpForce = 14f; 
    // Start is called before the first frame update

    private enum MovementState { idle, running, jumping, falling } // Fizemos uma enumeração de todos os estados do personagem
    private void Start()
    {
        Debug.Log("Start");
        rb2D = GetComponent<Rigidbody2D>(); // conecta a variável rb2D ao componente Rigidbody2D
        sprite = GetComponent<SpriteRenderer>(); // conecta a variável sprite ao componente SpriteRenderer
        anim = GetComponent<Animator>(); // conecta a variável anim ao componente Animator
    }

    // Update is called once per frame
    private void Update()
    {
        dirX /*dirX é sempre um float */ = Input.GetAxisRaw("Horizontal"); // Retorna automáticamente -1 (Esquerda), 0 (Parado) ou 1 (Direita)
        rb2D.velocity = new Vector2 /* o 'Vector2' é usado para jogos 2D */ (dirX * moveSpeed /* 7f */, rb2D.velocity.y); // rb.velocity.y (Junmp) = 0


        if (Input.GetButtonDown("Jump")) // ou GetKeyDown("space")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb2D.velocity.x, jumpForce); // O Vector3 é uzado para jogos 3D
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState() {
        MovementState state;

        if (dirX > 0f) // O personagem anda para a direita 
        {
            state = MovementState.running; // ou anim.SetBool("running", true)
            sprite.flipX = false; // O personagem continua virado para a direita
        }
        else if (dirX < 0f) // O personagem anda para a esquerda
        {
            state = MovementState.running; // ou anim.SetBool("running", true)
            sprite.flipX = true; // O personagem vira para esquerda
        } 
        else // O personagem está parado
        {
            state = MovementState.idle; // ou anim.SetBool("running", false)
        }

        if (rb2D.velocity.y > .1f) {
            state = MovementState.jumping;
        }
        else if (rb2D.velocity.y < -.1f) {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }
}