using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float startDeg = 0f;
    private float endDeg = 90f;
    private float t = 0f;
    private float swordDeg;
    private SwordState state;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject blade;
    private Collider bladeCollider;
    private float swingSpeed = 4f;
    [HideInInspector] public List<GameObject> enemiesHit;
    void Start()
    {
        state = SwordState.Up;
        bladeCollider = blade.GetComponent<Collider>();
        bladeCollider.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && state == SwordState.Up)
        {
            StartCoroutine(Attack());
        }
        if (state == SwordState.SwingDown)
        {
            t += swingSpeed * Time.deltaTime;
            Mathf.Clamp(t, 0f, 1f);

            swordDeg = Mathf.Lerp(startDeg, endDeg, t);
            model.transform.localRotation = Quaternion.Euler(swordDeg, model.transform.localRotation.y, model.transform.localRotation.z);
        }
        if (state == SwordState.SwingUp)
        {
            t += swingSpeed * Time.deltaTime;
            Mathf.Clamp(t, 0f, 1f);

            swordDeg = Mathf.Lerp(endDeg, startDeg, t);
            model.transform.localRotation = Quaternion.Euler(swordDeg, model.transform.localRotation.y, model.transform.localRotation.z);
        }
    }

    private IEnumerator Attack()
    {
        t = 0f;
        state = SwordState.SwingDown;
        bladeCollider.enabled = true;
        yield return new WaitForSeconds(1f/swingSpeed);
        state = SwordState.Forward;
        yield return new WaitForSeconds(0.1f);
        t = 0f;
        state = SwordState.SwingUp;
        bladeCollider.enabled = false;
        yield return new WaitForSeconds(1/swingSpeed);
        enemiesHit.Clear();
        state = SwordState.Up;
    }

    private enum SwordState
    {
        Up,
        SwingDown,
        Forward,
        SwingUp
    }
}
