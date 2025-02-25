using BLL.Employees;
using DTO.Model;
using System;
using System.Windows;

namespace EmployeeGUIWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        EmployeeBLL bll = new EmployeeBLL();
        Employee emp;
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            emp = bll.getEmployee(Int32.Parse(SearchId.Text));
            Navn.Content = emp.Name;
            YearsEmployed.Content = emp.YearsEmployed;
            //NameToChangeOrAdd.Text = emp.Name;
            //Years.Text = emp.YearsEmployed.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Employee emp = new Employee(NameToChangeOrAdd.Text, int.Parse(Years.Text));
            bll.AddEmployee(emp);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            emp.YearsEmployed = int.Parse(Years.Text);
            emp.Name = NameToChangeOrAdd.Text;
            bll.EditEmployee(emp);
        }

        private void AllCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            AllCompaniesList.ItemsSource = bll.AllCompanies();
        }

        private void AllCompaniesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EmployeesForCompany.Items.Clear();
            CompanyDetail comp = bll.GetCompanyDetails(((CompanyOverview)AllCompaniesList.SelectedItem).CompanyId);
            foreach (Employee emp in comp.Employees)
            {
                EmployeesForCompany.Items.Add(emp.Name + " har været ansat i " + comp.CompanyName + " i " + emp.YearsEmployed + " år");
            }

        }

        private void AddEmployeeToCompany_Click(object sender, RoutedEventArgs e)
        {
            bll.AddEmployeeToCompany(emp.EmployeeId, ((CompanyOverview)AllCompaniesList.SelectedItem).CompanyId);
        }
    }
}
