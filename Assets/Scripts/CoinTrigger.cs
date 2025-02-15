using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.IncrementScore(coinValue);
            Destroy(gameObject);
        }
    }
}
