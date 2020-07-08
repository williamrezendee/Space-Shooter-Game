using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _powerUpContainer;

    private Vector3 _positionToSpawn;
    private bool _stopSpawning = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _positionToSpawn = new Vector3(Random.Range(-9.0f, 9.0f), 7.0f, 0);
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, _positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            GameObject newTripleShotPowerup = Instantiate(_powerUps[randomPowerUp], _positionToSpawn, Quaternion.identity);
            newTripleShotPowerup.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
        
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }
}
