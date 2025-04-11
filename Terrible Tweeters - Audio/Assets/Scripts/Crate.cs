using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Crate : MonoBehaviour
{
    [SerializeField] AudioClip[] _clips;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 5f)
        {
            var clip = _clips[Random.Range(0, _clips.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}