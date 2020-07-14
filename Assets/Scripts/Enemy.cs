using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;

    private Player _player;
    private Animator _explosionAnim;
    private Collider2D _collider2D;
    private AudioSource _explosionAudio;

    void Start() // Update is called once per frame
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explosionAnim = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
        _explosionAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -5.5f)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            _explosionAnim.SetTrigger("OnEnemyDeath");
            _explosionAudio.Play();
            _collider2D.enabled = false;
            _speed = 2.0f;
            Destroy(this.gameObject, 2.6f);
        }
        
        if (other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddingPointsToScore();
            }
            _explosionAnim.SetTrigger("OnEnemyDeath");
            _explosionAudio.Play();
            _collider2D.enabled = false;
            _speed = 2.0f;
            Destroy(this.gameObject, 2.6f);
        }
    }
}
