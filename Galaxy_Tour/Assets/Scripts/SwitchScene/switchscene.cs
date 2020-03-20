using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVRTouchSample;
using System;

public class switchscene : MonoBehaviour
{
    public Animator PanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        PanelAnim.SetTrigger("fade");
        
    }

    public void FadeIntoNewScene(int index)
    {
        PanelAnim.SetTrigger("Idle");
        StartCoroutine(SwitchScene(index));
    }

    IEnumerator SwitchScene(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetUp(OVRInput.RawButton.X))
        {
            FadeIntoNewScene(1);
        }
    }
}
