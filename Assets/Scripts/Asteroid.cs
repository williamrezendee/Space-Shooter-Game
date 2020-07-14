using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 5.0f;
    [SerializeField] private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    void Start() // Start is called before the first frame update
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    void Update() // Update is called once per frame
    {
        // Rotate the object on the zed axis
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(_explosionPrefab, 1.0f);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
        }
    }
}
