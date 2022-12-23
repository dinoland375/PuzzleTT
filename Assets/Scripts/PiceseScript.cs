using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PiceseScript : MonoBehaviour
{
    [SerializeField] private Transform parentContent;
    [SerializeField] private Animator animator;

    public bool inRightPosition;
    public bool selected;

    private Vector3 _rightPosition;

    void Start()
    {
        _rightPosition = transform.position;
        transform.SetParent(parentContent);
        animator = GetComponent<Animator>();
        inRightPosition = false;
    }

    void Update()
    {
        if (transform.parent == false)
            animator.SetBool("Idle", true);
        else
            animator.SetBool("Idle", false);
        

        if (Vector3.Distance(transform.position, _rightPosition) < 0.5f)
        {
            if (!selected)
            {
                if (inRightPosition == false)
                {
                    transform.position = _rightPosition;    
                    
                    animator.SetBool("Idle", false);
                    animator.SetBool("Play", true);
                    inRightPosition = true;
                    
                    GetComponent<SortingGroup>().sortingOrder = -1;
                    Camera.main.GetComponent<DragAndDrop>().countPieces++;
                }
            }
        }
        else
        {
            animator.SetBool("Play", false);
            inRightPosition = false;
        }
    }

    
}
