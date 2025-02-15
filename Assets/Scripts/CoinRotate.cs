using UnityEngine;

public class CoinRotate : MonoBehaviour
{
   public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
    }
}
