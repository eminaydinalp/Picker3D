using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelEndObject : MonoBehaviour
{
    Animator _myAnimator;
    public bool IsLevelEnd { get => _levelEndLine.IsLevelEnd; set => _levelEndLine.IsLevelEnd = value;}
    [SerializeField]
    LevelEndLine _levelEndLine;
    public LevelEndLine LevelEndLine { get => _levelEndLine; set => _levelEndLine = value; }
    

    private void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void Open()
    {
        _myAnimator.SetTrigger("open");
    }
    public void Close()
    {
        _myAnimator.SetTrigger("close");
    }
    public void SetActive()
    {
        Close();
        _levelEndLine.gameObject.SetActive(true);
    }
    public void SetActive(bool fks)
    {
        Open();
        _levelEndLine.gameObject.SetActive(true);
    }
}
