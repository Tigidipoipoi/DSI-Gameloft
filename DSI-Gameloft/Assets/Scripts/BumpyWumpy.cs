using UnityEngine;
using System.Collections;

public class BumpyWumpy : MonoBehaviour
{

    public Vector2 bumpyAmp;
    public float minBumpyFreq;
    public float maxBumpyFreq;

    Vector2 phase_offset;
    Vector2 freq;
    Vector3 original_position;


    // Use this for initialization
    void Start()
    {
        original_position = transform.localPosition;
        phase_offset = new Vector2(Random.Range(0, Mathf.PI * 2), Random.Range(0, Mathf.PI * 2));
        freq = new Vector2(Random.Range(minBumpyFreq, maxBumpyFreq), Random.Range(minBumpyFreq, maxBumpyFreq));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            Mathf.Sin(Time.time * freq.x * Mathf.PI + phase_offset.x) * bumpyAmp.x,
            Mathf.Sin(Time.time * freq.y * Mathf.PI + phase_offset.y) * bumpyAmp.y,
            0
        ) + original_position;
    }
}
