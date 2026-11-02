using UnityEngine;

public class tesscriptpopupsementaraaja : MonoBehaviour
{
    [SerializeField] private GameObject keypadUI;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) keypadUI.SetActive(true);
    }
}
