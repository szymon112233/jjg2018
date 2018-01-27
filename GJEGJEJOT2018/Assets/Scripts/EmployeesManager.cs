using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeesManager : MonoBehaviour {

    [SerializeField] private List<Employee> Employees;

    private void Awake()
    {
        BuildEmployees();
    }

    public void BuildEmployees()
    {
        foreach (var employee in Employees)
        {
            employee.Build();
        }
    }
}
