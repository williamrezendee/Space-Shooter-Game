using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _clip;

        
    // Start is called before the first frame update
    void Start()
    {
        // Start the PowerUp in this position
        float randomX = Random.Range(-9.0f, 9.0f);
        transform.position = new Vector3(randomX, 7.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move down at a speed of 3 (adjustable in the inspector)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
         
        // When we leave the screen, destroy this object
        if (transform.position.y <= -5.8f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Verify witch GameObject was collided with this PowerUp
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("Default Value.");
                        break;
                }

            }
            Destroy(this.gameObject);
        }
    }
}
