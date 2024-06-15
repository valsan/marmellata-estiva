using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.position;
    }

    [SerializeField] private float _movingAmount = 5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = _initialPosition + Vector3.right * Mathf.PingPong(Time.time, _movingAmount);
        
    }
}
