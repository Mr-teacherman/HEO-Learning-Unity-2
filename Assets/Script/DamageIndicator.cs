using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Text text;
    public float displayTime;
    float elapsedTime;
    Canvas canvas;
    RectTransform canvasRact;
    // Start is called before the first frame update
    void Awake()
    {
        elapsedTime = 0;
        canvas = FindObjectOfType<Canvas>();
        canvasRact = canvas.GetComponent<RectTransform>();
        transform.SetParent(canvas.transform);
        GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > displayTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetAttack(Attack attack)
    {
        text.text = attack.damage.ToString();
        var worldPosition = attack.collision.contacts[0].point;
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRact.sizeDelta.x) - (canvasRact.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRact.sizeDelta.y) - (canvasRact.sizeDelta.y * 0.5f)));
        text.rectTransform.anchoredPosition = WorldObject_ScreenPosition;
    }
}
