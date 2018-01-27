using UnityEngine;
using UnityEngine.UI;

public class EmployeeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillBar;

    public virtual void ChangeColor(TaskEnum task)
    {
        fillBar.color = Task.GetColor(task);
    }

    public virtual void ChangeValue(float value)
    {
        fillBar.enabled = true;
        value = Mathf.Clamp01(value);
        slider.value = value;

        if (value == 0f)
        {
            RemoveFill();
        }
            
    }

    private void RemoveFill()
    {
        fillBar.enabled = false;
    }
}
