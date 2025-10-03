using System.Collections;
using UnityEngine;


public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 0f;
    [SerializeField] private bool useSmoothStep = false;
    [SerializeField] private bool pingPong = true;
    [SerializeField] private bool startAtA = true;

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 target;
    private bool initialized = false;

    private void Start()
    {
        if (pointA != null) posA = pointA.position; 
        else posA = transform.position;
        if (pointB != null) posB = pointB != null ? pointB.position : transform.position;

        if (posA == posB)
        {
            enabled = false;
            return;
        }

        target = startAtA ? posB : posA;
        initialized = true;
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (useSmoothStep)
            {
                float distance = Vector3.Distance(transform.position, target);
                if (distance < 0.001f)
                {
                    transform.position = target;
                }
                else
                {
                    float travelTime = distance / Mathf.Max(0.0001f, speed);
                    float t = 0f;
                    Vector3 startPos = transform.position;
                    while (t < 1f)
                    {
                        t += Time.deltaTime / travelTime;
                        float s = Mathf.SmoothStep(0f, 1f, t);
                        transform.position = Vector3.Lerp(startPos, target, s);
                        yield return null;
                    }
                    transform.position = target;
                }
            }
            else
            {
                while (Vector3.Distance(transform.position, target) > 0.001f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                    yield return null;
                }
                transform.position = target;
            }
            if (waitTime > 0f) yield return new WaitForSeconds(waitTime);

            if (pingPong)
            {
                if (target == posA) target = posB; 
                else target = posA;
            }
            else
            {
                if (target == posB)
                {
                    transform.position = posA;
                    target = posB;
                }
                else
                {
                    target = posB;
                }
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerSwitchCollider>() != null || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerSwitchCollider>() != null || collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            collision.gameObject.transform.SetParent(null);
        }
    } ///оепедекюрэ

    void OnDrawGizmos()
    {
        Vector3 a = pointA ? pointA.position : transform.position;
        Vector3 b = pointB ? pointB.position : transform.position;


        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(a, 0.1f);
        Gizmos.DrawSphere(b, 0.1f);
        Gizmos.DrawLine(a, b);
    }
}
