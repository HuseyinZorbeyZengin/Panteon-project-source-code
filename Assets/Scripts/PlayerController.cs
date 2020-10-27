using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    #region Variables

    public float moveSpeed;
    public float jumpForce;

    public CharacterController charController;
    public float gravityScale = 5f;


    private Vector3 moveDirection;

    private Camera theCam;

    public GameObject playerModel;
    public float rotateSpeed;

    public Animator anim;

    public bool isKnocking;
    public float knockBackLenght = 0.5f;
    private float knockbackCounter;
    public Vector2 knockbackPower;

    public bool stopMove;

    #endregion


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        theCam = Camera.main;
    }


    void Update()
    {
        #region Player Movement , Animations And Physics
        if (!isKnocking && !stopMove)
        {




            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * moveSpeed;
            moveDirection.y = yStore;



            //Fixing infinite jump
            if (charController.isGrounded)
            {
                //Fixing high gravity fall from objects and platform and make it more realistic and smooth
                moveDirection.y = -1f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }

            }

            //Gravity
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);


            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);

                //facing the player direction of the camera
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));

                //smoothing the player movement
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            #region Animations
            //Idle to run animation transition
            anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
            //Idle, run to jump animation transition
            anim.SetBool("Grounded", charController.isGrounded);
            #endregion
        }
        //Knocking Player away from obsticles
        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {

                isKnocking = false;
            }
        }
        #endregion
        if (stopMove)
        {

            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);

        }
    }

    #region KnockBack Function
    public void KnockBack()
    {
        isKnocking = true;
        knockbackCounter = knockBackLenght;
        moveDirection.y = knockbackPower.y;
    }
}
#endregion