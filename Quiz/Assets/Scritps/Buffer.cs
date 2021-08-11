using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Buffer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SpritsPrefabs;
    [SerializeField]
    private GameObject BorderPrefab;
    [SerializeField]
    private Transform GameGrid;
    [SerializeField]
    private Transform Finder;
    [SerializeField]
    private GameObject UIRestart;
    [SerializeField]
    private Image FabeImage;

    private List<int> MentionedIds = new List<int>();
    private int level;
    private int correctNumber;
    private int maxLevel = 3;
    private bool Success = false;
    private bool IsRestarting = false;


    private void Start()
    {
        ClearPoints();
        InitLevel();
    }


    public void InitLevel()
    {
        ClearPoints();
        int[] arrNumberes = GetRandomArr((level + 1) * 3, SpritsPrefabs.Length);

        int number = 0;
        for (int h = 0; h < level + 1; h++)
        {
            for (int i = -1; i < 2; i++)
            {
                GameObject Border = Instantiate(BorderPrefab, GameGrid);
                Border.transform.position = new Vector3(i * 20, h * 20 - (level * 20), 0);
                Border.transform.rotation = Quaternion.Euler(new Vector3());
                ScriptBorder ScriptBorder = Border.GetComponent<ScriptBorder>();
                int id = arrNumberes[number];
                ScriptBorder.Id = id;
                Transform BorderChild = Border.transform.Find("ViewImg");
                GameObject Sprite = Instantiate(SpritsPrefabs[id], BorderChild);
                number++;
                if (level == 0)
                {
                    Border.transform.DOPunchScale(new Vector3(1.2f, 1.2f, 0), 1, vibrato: 5, elasticity: 0.2f);
                }
            }
        }

        do
        {
            correctNumber = arrNumberes[UnityEngine.Random.Range(0, arrNumberes.Length)];
        }
        while (MentionedIds.Contains(correctNumber));
        GameObject FindObject = Instantiate(SpritsPrefabs[correctNumber], Finder);
    }
    private void ClearPoints()
    {
        for (int i = 0; i < GameGrid.childCount; i++)
        {
            Destroy(GameGrid.GetChild(i).gameObject);
        }
        for (int i = 0; i < Finder.childCount; i++)
        {
            Destroy(Finder.GetChild(i).gameObject);
        }
    }
    public void Request(int id, Transform TransformBorder)
    {
        Transform ChildTransform = TransformBorder.Find("ViewImg");

        if (!Success && !IsRestarting)
        {

            if (correctNumber == id)
            {
                ChildTransform.DOPunchPosition(new Vector3(0, 2, 0), 2);
                ParticleSystem particleSystem = TransformBorder.GetComponent<ParticleSystem>();
                particleSystem.Play();
                if (level + 1 < maxLevel)
                {
                    Invoke("CreateLevel", 3);
                }
                else
                {
                    Invoke("RestartLevel", 3);
                }
                Success = true;
            }
            else
            {
                TransformBorder.GetComponent<ScriptBorder>().ClickFalse();

            }
        }
    }
    private void InitRestart()
    {
        IsRestarting = true;
        level = 0;
        InitLevel();
        UIRestart.SetActive(true);
        FabeImage.gameObject.SetActive(true);
        FabeImage.canvasRenderer.SetAlpha(0);
        FabeImage.CrossFadeAlpha(1, 2, false);
    }
    private void RestartLevel()
    {
        Success = false;
        InitRestart();
    }
    private void FabeOn()
    {
        FabeImage.gameObject.SetActive(false);
    }
    public void Restart()
    {
        UIRestart.SetActive(false);
        FabeImage.CrossFadeAlpha(0, 2, false);
        Invoke("FabeOn", 2);
        IsRestarting = false;


    }
    private void CreateLevel()
    {
        level++;
        Success = false;
        ClearPoints();
        InitLevel();
    }


    private int[] GetRandomArr(int length, int max)
    {
        var rand = new System.Random();

        var knownNumbers = new HashSet<int>();

        int[] arr = new int[length];

        for (int i = 0; i < arr.Length; i++)
        {
            int newElement;
            do
            {
                newElement = rand.Next(max);
            } while (!knownNumbers.Add(newElement));

            arr[i] = newElement;
        }

        return arr;
    }
}
