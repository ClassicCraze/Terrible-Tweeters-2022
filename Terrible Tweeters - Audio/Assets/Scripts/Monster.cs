using System.Collections;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] AudioClip _deathClip;

    bool _hasDied;

    IEnumerator Start() // Convert to cached coroutine to show that and fix issue
    {
        while (_hasDied == false)
        {
            float delay = Random.Range(5, 30);
            GetComponent<AudioSource>()?.Play();
            yield return new WaitForSeconds(delay);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied)
            return false;

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (collision.contacts[0].normal.y < -0.5)
            return true;

        return false;
    }

    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<AudioSource>().PlayOneShot(_deathClip);
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        GetComponent<AudioSource>()?.Play();
    }
}