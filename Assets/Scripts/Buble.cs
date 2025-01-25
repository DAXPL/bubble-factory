using UnityEngine;

public class Buble : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sideSpeed = 0.1f;
    float startTime;

    private void Start()
    {
        speed = speed * Random.Range(0.8f, 1.2f);
        sideSpeed = sideSpeed * Random.Range(0.6f, 1.4f);
        startTime = Time.time;
    }
    void Update()
    {
        this.transform.position = transform.position + (Vector3.up* speed*Time.deltaTime) + (Vector3.right*Mathf.Sin(startTime + Time.time)* sideSpeed);
    }
}
