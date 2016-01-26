﻿using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour, Poolable<Platform> {

    static int num = 0;

    protected static Platform SCreate ( ) {
        GameObject obj = new GameObject ( );
        obj.name = "platform_" + num.ToString ( "000" );
        obj.transform.SetParent ( TerrainManager.Instance.transform );

        Platform self = obj.AddComponent<Platform> ( );

        ++num;

        return self;
    }

    protected Pool<Platform> _pool;

    protected GameObject    _body;
    protected Vector3       _bodyOriginalPos;
    protected Quaternion    _bodyOriginalRot;

    void Start()
    {
        _body = transform.GetChild(0).gameObject;
        _bodyOriginalPos = _body.transform.localPosition;
        _bodyOriginalRot = _body.transform.localRotation;
    }


    public Platform Create ( ) {
        return Platform.SCreate ( );
    }


    public void Register ( UnityEngine.Object pool ) {
        _pool = pool as Pool<Platform>;
    }


    public bool IsReady ( ) {
        return !GetComponent<Renderer>().IsVisibleFrom(Camera.main);
    }


    public void Duplicate ( Platform a_template ) {
        foreach(Transform child in a_template.transform)
        {
            GameObject obj = Instantiate(child.gameObject) as GameObject;
            obj.transform.SetParent(transform);
        }

        transform.position = new Vector3(1000, 1000, 3);
    }


    public void Release ( ) {
        _pool.onRelease ( this );
        transform.position = new Vector3(- 1000f, - 1000f, - 1000f);
    }


    public bool makeFall(float gravity = 1.0f, float delay = .0f)
    {
        Rigidbody2D rb = _body.GetComponent<Rigidbody2D>();
        if (rb)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            StartCoroutine(fall(delay, rb));
            return true;
        }
        return false;
    }


    private IEnumerator fall(float delay, Rigidbody2D rb)
    {
        yield return new WaitForSeconds(delay);

        rb.gravityScale = 1.0f;
        rb.isKinematic = false;
        rb.AddForce(new Vector2(.0f, -1.0f));
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position;

        Rigidbody2D rb = _body.GetComponent<Rigidbody2D>();
        if (rb)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
            rb.isKinematic = true;
            _body.transform.localPosition = _bodyOriginalPos;
            _body.transform.localRotation = _bodyOriginalRot;
        }        
    }    

}
