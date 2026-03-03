using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Nevigator))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class DesktopCat : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Nevigator nevigator;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D colli;
    private Button button;
    private Vector2 destination;
    private GameObject destinationMarker;

    [SerializeField] private float moveSpeed = 2f;
    

    private bool isMoving = false;
    private float idleTime = 0f;
    private float idleDuration = 10f; // Time to stay idle before moving again

    private void Start()
    {
        Setup();
        rectTransform.localPosition = Vector2.zero; // Start at the center of the screen
    }
    
    private void FixedUpdate()
    {
        idleTime += Time.fixedDeltaTime;
        if(!isMoving && idleTime >= idleDuration)
        {
            destinationMarker = new GameObject("DestinationMarker");
            Vector2 destination = nevigator.GetRandomDestination();
            this.destination = destination;
            StartCoroutine(Move(destination));
            idleTime = 0f; // Reset idle time after starting to move
        }
        else
        {
            
            if(idleTime >= idleDuration*2)
            {
                Destroy(destinationMarker);
                StopCoroutine(Move(destination));
                isMoving = false;
                idleTime = 0f;
            }
        }
        
    }

    private IEnumerator Move(Vector2 destination)
    {
        if(isMoving)
        {
            yield break; // Exit if already moving
        }
        isMoving = true;
        RectTransform windowTranform = WindowManager.instance.windowCanvas.GetComponent<RectTransform>();
        destinationMarker.AddComponent<RectTransform>();
        destinationMarker.transform.SetParent(windowTranform);
        destinationMarker.transform.localPosition = new Vector2(destination.x, destination.y);
        
        while (isMoving)
        {
            rectTransform.localPosition = Vector2.MoveTowards(rectTransform.localPosition, destinationMarker.transform.localPosition, moveSpeed * Time.fixedDeltaTime);
            Vector2 direction = destinationMarker.transform.localPosition - rectTransform.localPosition;
            FilpSprite(direction);
            if (Vector2.Distance(rectTransform.localPosition, destinationMarker.transform.localPosition) < 0.1f)
            {
                isMoving = false;
                Destroy(destinationMarker);
                yield break;
            }
            yield return null;
        }
        isMoving = false;
        
        
    }
    private void FilpSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

    private void Setup()
    {
        rectTransform = GetComponent<RectTransform>();
        nevigator = GetComponent<Nevigator>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colli = GetComponent<Collider2D>();
        button = GetComponent<Button>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(Application.isPlaying && destinationMarker != null)
            Gizmos.DrawLine(transform.position, destinationMarker.transform.position);
    }
}