using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject linkedPortal;
    public float reenableTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) 
        {
            linkedPortal.GetComponent<Collider2D>().enabled = false;
            other.gameObject.transform.position = linkedPortal.transform.position;
            Invoke("EnableColider", reenableTime);
        }
    }
    public void EnableColider()
    {
        linkedPortal.GetComponent<Collider2D>().enabled = true;
    }
}
