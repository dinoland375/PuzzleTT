using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Sprite[] levels;
    [SerializeField] private GameObject endMenu;
    
    [SerializeField] private GameObject scrollView;
    
    public int countPieces;
    public GameObject selectedPiece = null;
    
    private int _sortingOrderCount;
    private bool _isTouchDragging;
    private bool _isMouseDragging;

    private void Awake()
    {
        for (int i = 0; i < 16; i++)
        {
            GameObject.Find("Piece (" + i + ")").transform.Find("Puzzle").GetComponent<SpriteRenderer>().sprite =
                levels[PlayerPrefs.GetInt("Level")];
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1)  //TOUCH
        {
            _isMouseDragging = false;
            
            Touch touch0 = Input.GetTouch(0);
            
            if (touch0.phase == TouchPhase.Began)
            {
                RaycastHit2D hitTouch = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch0.position),  Vector2.zero);

                if (hitTouch.transform.CompareTag("Puzzle"))
                {
                    if (!hitTouch.transform.GetComponent<PiceseScript>().inRightPosition)
                    {
                        scrollView.GetComponent<ScrollRect>().horizontal = false;
                        selectedPiece = hitTouch.transform.gameObject;
                        selectedPiece.transform.SetParent(null);
                        selectedPiece.GetComponent<PiceseScript>().selected = true;
                        selectedPiece.GetComponent<SortingGroup>().sortingOrder = _sortingOrderCount;
                        _sortingOrderCount++;
                    }
                }
            }

            if (touch0.phase == TouchPhase.Ended)
            {
                if (selectedPiece != null) //Up
                {
                    scrollView.GetComponent<ScrollRect>().horizontal = true;

                    selectedPiece.GetComponent<PiceseScript>().selected = false;
                    selectedPiece = null;
                }
            }
           
            if (selectedPiece != null)
            {
                Vector3 touchPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                selectedPiece.transform.position = new Vector3(touchPoint.x, touchPoint.y, 0);
            }
        }
        else
        {
            _isMouseDragging = true;
        }

        if (_isMouseDragging)
        {
            if (Input.GetMouseButtonDown(0)) //MOUSE
            {
                RaycastHit2D hitMouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hitMouse.transform.CompareTag("Puzzle"))
                {
                    if (!hitMouse.transform.GetComponent<PiceseScript>().inRightPosition)
                    {
                        scrollView.GetComponent<ScrollRect>().horizontal = false;
                    
                        selectedPiece = hitMouse.transform.gameObject;
                        selectedPiece.transform.SetParent(null);
                    
                        selectedPiece.GetComponent<PiceseScript>().selected = true;
                        selectedPiece.GetComponent<SortingGroup>().sortingOrder = _sortingOrderCount;
                    
                        _sortingOrderCount++;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (selectedPiece != null) 
                {
                    selectedPiece.GetComponent<PiceseScript>().selected = false;
                    scrollView.GetComponent<ScrollRect>().horizontal = true;
                    selectedPiece = null;
                }
            }

            if (selectedPiece != null)
            {
                Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                selectedPiece.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0);
            }
        }
        
        if (countPieces == 16)
        {
            endMenu.SetActive(true);
        }
    }
}
