using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText; // Torna o Text cherriesText visível no Inspector


    private void OnTriggerEnter2D(Collider2D collision) { // Detecta quando outro objeto entra no Trigger do colisor 2D, executando ações com base nisso.
        if (collision.gameObject.CompareTag("Cherries")) { // Verifica se o objeto que colidiu tem a tag "Cherries"
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = "Cherries: " + cherries; // Atualiza o texto na interface para exibir a quantidade de cerejas coletadas
        }
    }
}
