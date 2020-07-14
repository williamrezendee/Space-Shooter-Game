using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldsVisualizer;
    [SerializeField] private GameObject _fireDamageLeft;
    [SerializeField] private GameObject _fireDamageRight;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private int _lives = 3;

    private float _canFire = 0f;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    [SerializeField] private int _score = 0;
    
    void Start() // Start is called before the first frame update
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL!");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is NULL!");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The Audio Source is NULL!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

        _fireDamageLeft.SetActive(false);
        _fireDamageRight.SetActive(false);
    }

    void Update() // Update is called once per frame
    {
        CalculateMoviment();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMoviment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive == true)
        {
            float increaseValue = 3.0f;
            transform.Translate(direction * _speed * increaseValue * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        

        if (transform.position.y >= 5.95f)
        {
            transform.position = new Vector3(transform.position.x, 5.95f, 0);
        }
        else if (transform.position.y <= -3.95f)
        {
            transform.position = new Vector3(transform.position.x, -3.95f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.02f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }
    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;
        if (_lives == 2)
        {
            _fireDamageLeft.SetActive(true);
        }
        else if (_lives == 1)
        {
            _fireDamageRight.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _fireDamageLeft.SetActive(false);
            _fireDamageRight.SetActive(false);
            _speed = 0f;
            Destroy(this.gameObject, 0.3f); 
        }
    }
    public void TripleShotActive() // triple Shot Behaviour
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }
    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    public void SpeedBoostActive() // Speed Boost Behaviour
    {
        _isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostRoutine());
    }
    IEnumerator SpeedBoostRoutine()
    { 
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
    }
    public void ShieldActive() // Shield Powerup Behaviour
    {
        _isShieldActive = true;
        _shieldsVisualizer.SetActive(true);
    }
    public void AddingPointsToScore() // Create a method to add 10 points to the score!
    {
        _score = _score + 10;
        _uiManager.UpdateScore(_score); // Communicate to the UI script to update the score!
    }
}