using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI customerText;

    private CustomerAnimator animator;

    private void Awake()
    {
        animator = GetComponent<CustomerAnimator>();

        if (animator == null)
        {
            Debug.Log("CustomerAnimator missing on CustomerManager GameObject");
        }
    }
    void Start()
    {
        //EnterAndRefresh();
    }

    public void EnterAndRefresh()
    {
        StopAllCoroutines();
        StartCoroutine(EnterRoutine());
    }

    public IEnumerator EnterRoutine()
    {
        Refresh();
        yield return animator.PlayEnter();
    }

    public void Refresh()
    {
        if (DayManager.Instance == null) return;

        CustomerData customer = DayManager.Instance.activeCustomer;

        if (customer == null ) return;

        portraitImage.sprite = customer.portrait;
        nameText.text = customer.customerName;
        customerText.text = customer.customerText;
    }

    public IEnumerator ExitAndRefresh()
    {
        Refresh();
        yield return animator.PlayExit();
    }

    private void OnEnable()
    {
        // Listen for load scene events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only trigger if back in shop scene
        if (scene.name == "ShopScene")
        {
            EnterAndRefresh();
        }
    }
}
