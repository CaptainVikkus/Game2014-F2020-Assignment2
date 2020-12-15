using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippingPlatformBehaviour : MonoBehaviour
{
    [SerializeField] float flipFreq;
    [SerializeField] float flipSpeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlipTimer());
    }

    IEnumerator FlipTimer()
    {
        while (this.isActiveAndEnabled)
        {
            //Wait for flip
            yield return new WaitForSeconds(flipFreq);
            //perform flip
            yield return PerformFlip(flipSpeed);
        }
    }

    IEnumerator PerformFlip(float duration)
    {
        Vector3 startRotation = transform.eulerAngles;
        float endRotation = startRotation.z + 180.0f;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation.z, endRotation, t / duration) % 360.0f;
            transform.eulerAngles = new Vector3(startRotation.x, startRotation.y, zRotation);
            yield return null;
        }
    }
}
