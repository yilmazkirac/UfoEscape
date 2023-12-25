using System.Collections;
using DG.Tweening;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    public float CharacterSpeed = 5f;
    public bool isJetpack;
    public bool isGrounded;
    public CharacterController CharCont;

    public Transform GroundChechPoint;
    public LayerMask GroundLayer;
    public float JumpForce = 12f, GravityMod = 2.5f;

    [SerializeField] private GameObject trail;

    private Vector3 newScale;
    private Vector3 scale;
    private bool isDuck;
    public Vector3 movement;
    public Vector3 moveDir;


    public int wayIndex;
   
    int[] wayIndexPoint= new int[3] {-3,0,3};

    [SerializeField] private Animator Anim;

    private void Start()
    {
        scale = gameObject.transform.localScale;
        newScale = scale / 2.5f;
        wayIndex = 1;    
    }


    private void Update()
    {
    
        if (isJetpack)
        {
            JetPackOn();
        }
        if (CharacterSpeed>0)
        {
            PlayerMovement();
        }
        WayCheck();
    }

   private void WayCheck()
    {
        if (wayIndex==2)
        {       
            if (transform.position.x>1.5f)
            {
                transform.position = new Vector3(3,transform.position.y, transform.position.z);
            }
        }

        if (wayIndex == 1)
        {
            if (transform.position.x < 1.5f&& transform.position.x > -1.5f)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
        if (wayIndex == 0)
        {
            if (transform.position.x < -1.5f)
            {
                transform.position = new Vector3(-3, transform.position.y, transform.position.z);
            }
        }
    }
    void SwapWay(int a)
    {    
        float distance = Vector3.Distance(transform.position, new Vector3(transform.position.x+ a, transform.position.y, transform.position.z));
        gameObject.transform.DOMove(new Vector3(wayIndexPoint[wayIndex], transform.position.y, transform.position.z + 3), distance * .2f / 5).SetEase(Ease.Linear);
    }
    private void PlayerMovement()
    {
        transform.position += (new Vector3(0, 0, 1) * Time.deltaTime * CharacterSpeed);
        if (SwipeManage.swipeRight)
        {
            Anim.CrossFade("right", .2f);
          
            wayIndex += 1;          
            if (wayIndex >2)
            {
                wayIndex = 2;       
            }           
            SwapWay(3);       
        }
        if (SwipeManage.swipeLeft)
        {
            Anim.CrossFade("left", .2f);
            wayIndex -= 1;
            if (wayIndex<0)
            {
                wayIndex = 0;           
            }
            SwapWay(-3);
            
        }

        if (!isJetpack)
        {
           moveDir = new Vector3(0, 0, 1);
            float yVel = movement.y;
            movement = ((transform.forward * moveDir.z) + (transform.right * moveDir.x)).normalized * CharacterSpeed;

            movement.y = yVel;

            if (CharCont.isGrounded)
            {
                movement.y = 0;
            }

            isGrounded = Physics.Raycast(GroundChechPoint.position, Vector3.down, .3f, GroundLayer);
            if (SwipeManage.swipeUp && isGrounded)
            {
                GravityMod = 4f;
                movement.y = JumpForce;
            }
            movement.y += Physics.gravity.y * Time.deltaTime * GravityMod;

           CharCont.Move(movement * Time.deltaTime);         


            if (SwipeManage.swipeDown && !isDuck)
            {
                 GravityMod=8;
                transform.DOScale(newScale, 0.3f);
                isDuck = true;
                Invoke("IsDuckFalse", .5f);
            }
        }
    }
    private void IsDuckFalse()
    {
        transform.DOScale(scale, 0.3f);
        isDuck = false;
        GravityMod =4f;
    }

    public void JetPackOn()
    {   
        trail.SetActive(true);
        transform.position += (new Vector3(0, 0, 2) * Time.deltaTime * CharacterSpeed);
        if (transform.position.y < 10)
        {
            transform.position += (new Vector3(0, 1, 0) * Time.deltaTime * CharacterSpeed);
        }
        StartCoroutine(JetPackTime());
    }

    IEnumerator JetPackTime()
    {
        yield return new WaitForSeconds(4f);
        isJetpack = false;    
        trail.SetActive(false);
    }
}
