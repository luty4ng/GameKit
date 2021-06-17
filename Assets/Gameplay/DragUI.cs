using System.Net.Mime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

    private Vector2 offsetPos;
    private Image image;
    private UnityAction update;
    private float oriSize;
    public GameObject prefabObj;
    public float minSize;
    public float shrinkSpeed;
    public float returnSpeed;
    public float returnError;
    public Vector2 oriPos;
    public bool canReturn;
    public float type;

    public enum Type
    {
        
    }

    private void OnEnable() {
        
    }
    private void Start() {
        image = this.GetComponent<Image>();
        oriSize = image.rectTransform.sizeDelta.x;
        oriPos = transform.position;
        canReturn = true;
    }

    private void Update() {
        if(update!=null)
            update.Invoke();   
        
        if(canReturn)
        {
            Vector2 dir = (Vector2)oriPos - (Vector2)transform.position;
            float x = Mathf.Abs(dir.x);
            float y = Mathf.Abs(dir.y);
            float distance = Mathf.Sqrt(x*x + y*y);
            if(distance > returnError)
                transform.Translate(dir.normalized * returnSpeed * Time.deltaTime);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(eventData.pointerId == -1)
        {
            Vector2 tmp = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - offsetPos;
            transform.position = new Vector3(tmp.x, tmp.y, transform.position.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(eventData.position));
        Debug.Log(transform.position);
        Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(eventData.position));
        Debug.Log((Vector2)transform.position);
        if(eventData.pointerId == -1)
        {
            offsetPos = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) - (Vector2)transform.position;
            canReturn = false;
            if(update!=null)
                update -= Recover;
            update += Shrink;
        }
        
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerId == -1)
        {
            //Ray ray = Camera.main.ScreenPointToRay(transform.position);
            Ray ray = new Ray(transform.position, new Vector3(0,0,20));
            RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.down);
            
            
            if(hitInfo)
            {
                Debug.DrawLine(ray.origin,hitInfo.point);
                GameObject gameObj = hitInfo.collider.gameObject;
                //Debug.Log("Name: " + gameObj.name);
                if(gameObj.tag != "Keys")
                {
                    Debug.Log(gameObj.tag);
                    Grid grid = GameObject.Find("Grid").GetComponent<Grid>();
                    Vector3Int cellPosition = grid.LocalToCell(hitInfo.point);
                    Vector3 localPosition = grid.CellToLocal(cellPosition) + new Vector3(0.5f,0.5f);
                    if(prefabObj != null)
                    {
                        Instantiate(prefabObj, localPosition, Quaternion.Euler(0,0,0));
                    }
                }
            }
        


        canReturn = true;
        if(update!=null)
            update -= Shrink;
        update += Recover;
        }
        
    }

    private void Shrink()
    {
        if(image.rectTransform.sizeDelta.x >= minSize)
            image.rectTransform.sizeDelta -= (new Vector2(1, 1)) * shrinkSpeed * Time.deltaTime;
    }

    private void Recover()
    {
        if(image.rectTransform.sizeDelta.x < oriSize)
            image.rectTransform.sizeDelta += (new Vector2(1, 1)) * shrinkSpeed * Time.deltaTime;
    }
}