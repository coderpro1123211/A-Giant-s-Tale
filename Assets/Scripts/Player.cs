using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float groundCheckDst;
    public float gravity;
    public float maxScale;
    public float minScale;
    public GameObject normalGraphics;
    public GameObject scaleGraphics;
    public Transform body;
    public float scaleSpeed;
    public LayerMask ground;
    public Vector2 rayOffset;
    public float platformRayDistance;
    public float minDistance;
    public Transform groundChk;
    public Vector3 interactRayOffset;
    public LayerMask interactable;
    public float scaleOffset;
    public Transform[] scaleCheckRayPos;

    public bool disableMovement;

    public bool canJump;

    public bool canInteract;

    public AnimationCurve scaleCurve;
    public AnimationCurve positionCurve;
    public AnimationCurve h;

    float scale;
    bool grounded;
    Vector2 velocity;
    Vector2 input;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer spriteRenderer;
    SpriteRenderer bodyRenderer;
    Collider2D c;
    float faceDir;
    float temp;
    float gravityScale = 1;
    float curG;
    bool jumping;

    private void Awake()
    {
        FindObjectOfType<StatusText>().p = this;
        FindObjectOfType<DialougeManager>().p = this;
    }

    void Start()
    {
        if (speed <= 1)
        {
            speed = 1;
        }
        rb = GetComponent<Rigidbody2D>();
        anim = normalGraphics.GetComponent<Animator>();
        spriteRenderer = normalGraphics.GetComponent<SpriteRenderer>();
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        rb.gravityScale = 0;
        scaleGraphics.SetActive(false);
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        if (disableMovement)
        {
            anim.SetBool("grounded", true);
            anim.SetBool("walking", false);
            return;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        grounded = Physics2D.OverlapPoint(groundChk.position, ground);

        if (grounded)
        {
            curG = 0;
        }
        else
        {
            curG += gravity * Time.deltaTime;
        }

        if (input.x != 0)
        {
            faceDir = Mathf.Sign(input.x);
        }

        if (faceDir < 0)
        {
            spriteRenderer.flipX = true;
            bodyRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            bodyRenderer.flipX = false;
        }

        if (grounded && Mathf.Abs(input.y) > 0.05f) Scale(input.y * Time.deltaTime * scaleSpeed);
        velocity = new Vector2(input.x * speed, 0);

        Ray2D ray = new Ray2D((Vector2)body.position + rayOffset * faceDir, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, platformRayDistance, ground);
        Collider2D[] col = new Collider2D[10];
        Physics2D.GetContacts(c, col);

        if (hit && grounded && scale > minScale + scaleOffset && !jumping)
        {
            //Debug.Log("HITIT");
            canJump = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumping = true;
                StartCoroutine(C(new Vector2(hit.point.x + faceDir * 0.1f, Mathf.Lerp(hit.point.y + (maxScale-scale) / maxScale, body.position.y + (maxScale - scale) / maxScale, .5f)), transform.position, minScale, scale));
            }
        }
        else
        {
            canJump = false;
        }

        ray = new Ray2D(body.position, Vector2.right * faceDir);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 1.5f, interactable);

        if (hit)
        {
            canInteract = true;
            //Debug.Log("lel");
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.GetComponent<Interactable>().Activate(true);
                print("lelel");
            }
        }
        else
        {
            canInteract = false;
        }

        

        anim.SetBool("grounded", grounded);
        anim.SetBool("walking", Mathf.Abs(rb.velocity.x) > 0.01f);
    }

    void Scale(float amount)
    {
        //if (!grounded) return;
        if (amount > 0)
        { 
            foreach (Transform t in scaleCheckRayPos)
            {
                if (Physics2D.Raycast(t.position, Vector2.up, amount).collider != null)
                {
                    print("Ray " + transform.name + " failed");
                    return;
                }
            }
        }


        scale = Mathf.Clamp(scale + amount, minScale, maxScale);
        if (scale <= minScale + 0.01f && !normalGraphics.activeSelf)
        {
            normalGraphics.SetActive(true);
            scaleGraphics.SetActive(false);
        }
        else if (scale > minScale && normalGraphics.activeSelf)
        {
            normalGraphics.SetActive(false);
            scaleGraphics.SetActive(true);
        }
        else if (scale > minScale)
        {
            body.localPosition = new Vector3(0, scale, 0);
        }
    }

    IEnumerator C(Vector2 targetPos, Vector2 currentPos, float targetScale, float currentScale)
    {
        float val = 0;
        temp = rb.gravityScale;
        rb.gravityScale = 0;

        Vector3 cur = new Vector3(currentPos.x, currentPos.y, -2);
        Vector3 tar = new Vector3(targetPos.x, targetPos.y, -2);
        yield return null;
        while (val < 1)
        {
            val = Mathf.Clamp(val + Time.deltaTime, 0, 1);
            scale = Mathf.Lerp(currentScale, targetScale, scaleCurve.Evaluate(val));
            body.localPosition = new Vector3(0, scale, 0);
            transform.position = Vector3.Lerp(cur,tar, positionCurve.Evaluate(val));

            yield return null;
        }
        scale = minScale;
        normalGraphics.SetActive(true);
        scaleGraphics.SetActive(false);
        jumping = false;
        rb.gravityScale = temp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine((Vector2)body.position + rayOffset * (faceDir == 0 ? 1 : faceDir), (Vector2)body.position + rayOffset * (faceDir == 0 ? 1 : faceDir) + Vector2.down * platformRayDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(body.position + interactRayOffset, body.position + Vector3.right * faceDir * 1.5f + interactRayOffset);
    }

    void FixedUpdate()
    {
        if (scale > minScale + 0.01f || disableMovement)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = velocity + Vector2.down * curG;
    }
}
