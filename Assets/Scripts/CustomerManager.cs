using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private TextMeshProUGUI nameText;

    private CustomerAnimator animator;


    private void Awake()
    {
        animator = GetComponent<CustomerAnimator>();

        if (animator != null)
        {
            Debug.Log("CustomerAnimator missing on CustomerManager GameObject");
        }
    }
    void Start()
    {
        Refresh(); 
        if (animator != null)
        {
            animator.PlayEnter();
        }
    }

    public void Refresh()
    {
        if (DayManager.Instance == null) return;

        CustomerData customer = DayManager.Instance.activeCustomer;

        if (customer == null ) return;

        portraitImage.sprite = customer.portrait;
        nameText.text = customer.customerName;
    }

    public IEnumerator ExitAndRefresh()
    {
        if (animator != null)
        {
            yield return animator.PlayExit();
        }

        Refresh();

        if (animator != null)
        {
            animator.PlayEnter();
        }  
    }
}
