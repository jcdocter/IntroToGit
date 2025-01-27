using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyGame : MonoBehaviour
{
    const float PLAYER_SPEED = 10f;
    const float ENEMY_SPEED = 5f;
    const float BULLET_LIFE = 5f;
    const int ENEMY_HEALTH = 10;

    public GameObject playerInstance;
    public GameObject enemyPrefab;
    public GameObject bulletPrefab;

    public List<GameObject> enemies = new List<GameObject>();
    public List<int> enemyHealth = new List<int>();

    public List<GameObject> bullets = new List<GameObject>();
    public List<float> bulletTimers = new List<float>();

    private float refireTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn some enemies
        for (int i = 0; i < 10; ++i)
        {
            GameObject enemy = GameObject.Instantiate(enemyPrefab);
            enemy.transform.position = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
            enemies.Add(enemy);
            enemyHealth.Add(ENEMY_HEALTH);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update enemies
        for (int i = 0; i < enemies.Count; ++i)
        {
            // Move towards player
            enemies[i].transform.Translate((playerInstance.transform.position - enemies[i].transform.position).normalized * Time.deltaTime * ENEMY_SPEED);
        }

        // Move player
        playerInstance.transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * PLAYER_SPEED, 0, Input.GetAxis("Vertical") * Time.deltaTime * PLAYER_SPEED);

        // Fire gun
        refireTime -= Time.deltaTime;
        if (Input.GetMouseButton(0) && refireTime <= 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            Vector3 mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
            mousePosition.y = 0;
            bullet.transform.position = playerInstance.transform.position + (mousePosition - playerInstance.transform.position).normalized;
            bullet.transform.LookAt(mousePosition);
            bullets.Add(bullet);
            bulletTimers.Add(BULLET_LIFE);
        }

        // Update bullets
        
    }
}
