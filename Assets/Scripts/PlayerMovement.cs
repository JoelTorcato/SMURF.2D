using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D; // Invês de usarmos o nome 'Rigidbody2D' inteiro, usamos 'rb2D'
    private BoxCollider2D coll; // Invês de usarmos o nome 'BoxCollider2D' inteiro, usamos 'coll'
    private SpriteRenderer sprite; // Invês de usarmos o nome 'SpriteRenderer' inteiro, usamos 'sprite'
    private Animator anim; // Invês de usarmos o nome 'Animator' inteiro, usamos 'anim'

    [SerializeField] private LayerMask jumpableGround; // Torna o LayerMask jumpableGround visível no Inspector

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; // [SerializeField] torna uma variável privada visível no Inspector
    [SerializeField] private float jumpForce = 14f;
    // Start is called before the first frame update

    private enum MovementState { idle, running, jumping, falling } // Fizemos uma enumeração de todos os estados do personagem
    private void Start()
    {
        Debug.Log("Start");
        rb2D = GetComponent<Rigidbody2D>(); // conecta a variável rb2D ao componente Rigidbody2D
        coll = GetComponent<BoxCollider2D>(); // conecta a variável coll ao componente BoxCollider2D
        sprite = GetComponent<SpriteRenderer>(); // conecta a variável sprite ao componente SpriteRenderer
        anim = GetComponent<Animator>(); // conecta a variável anim ao componente Animator
    }

    // Update is called once per frame
    private void Update()
    {
        dirX /*dirX é sempre um float */ = Input.GetAxisRaw("Horizontal"); // Retorna automáticamente -1 (Esquerda), 0 (Parado) ou 1 (Direita)
        rb2D.velocity = new Vector2 /* o 'Vector2' é usado para jogos 2D */ (dirX * moveSpeed /* 7f */, rb2D.velocity.y); // rb.velocity.y (Junmp) = 0


        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) // ou GetKeyDown("space")
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

// Rever melhor esta parte do código
private bool IsGrounded() // Método privado que retorna um booleano indicando se o personagem está no chão.
{
    return Physics2D.BoxCast( // Realiza um BoxCast para verificar colisão com o chão.
        coll.bounds.center,  // Define o centro da "caixa" como o centro do colisor do objeto.
        coll.bounds.size,    // Usa o tamanho do colisor como o tamanho da "caixa" do BoxCast.
        0f,                  // Não aplica rotação à caixa (mantém alinhada ao objeto).
        Vector2.down,        // Lança a caixa para baixo (direção vertical negativa).
        0.1f,                // Define a distância curta do BoxCast (próxima ao personagem).
        jumpableGround       // Considera apenas colisões com camadas marcadas como "chão saltável".
    );
}
}
