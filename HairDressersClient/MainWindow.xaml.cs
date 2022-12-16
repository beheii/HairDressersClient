using HairDressersClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HairDressersClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private ObservableCollection<Customer> _Items;
        private Customer _currentItem;
        private HttpClient client = new HttpClient();
        public List<Customer> Customers { get; set; }
        public List<Appointment> Appointments { get; set; }
        public Customer CurrentItem
        {
            get { return _currentItem; }
            set { _currentItem = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            Customers = new List<Customer>();
            Appointments = new List<Appointment>();
            ShowCustomersList();
            ShowAppointsmentsList();
        }
        private async void ShowCustomersList()
        {
            HttpClient httpClient = new HttpClient();
            var res = await httpClient.GetStringAsync("https://localhost:44337/api/Customers");
            Customers = JsonConvert.DeserializeObject<List<Customer>>(res);
        } 

        private async void ShowAppointsmentsList() 
        {
            HttpClient httpClient = new HttpClient();
            var res = await httpClient.GetStringAsync("https://localhost:44337/api/Appointments");
            Appointments = JsonConvert.DeserializeObject<List<Appointment>>(res);
        }

        private void ShowCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomersGrid.ItemsSource = Customers;
        }

        private void ShowAppointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentGrid.ItemsSource = Appointments;
        }
        private async void CustomersGridFilling(object sender, DataGridRowEditEndingEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var res = await httpClient. 
                PostAsJsonAsync("https://localhost:44337/api/Customers", Customers[CustomersGrid.SelectedIndex]);
            ShowCustomersList();
        }
        private async void AppointmentsGridFilling(object sender, DataGridRowEditEndingEventArgs e)
        {
            HttpClient httpClient = new HttpClient(); 
            var res = await httpClient.
                PostAsJsonAsync("https://localhost:44337/api/Appointments", Appointments[AppointmentGrid.SelectedIndex]);
            ShowAppointsmentsList();
        }
        private async void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
                var res = await httpClient.
                    DeleteAsync($"https://localhost:44337/api/Customers/{Customers[CustomersGrid.SelectedIndex].Id}");
                ShowCustomersList();
                throw new Exception("Data Successfully Deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private async void DeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HttpClient httpClient = new HttpClient();
            var res = await httpClient.
                DeleteAsync($"https://localhost:44337/api/Appointments/{Appointments[AppointmentGrid.SelectedIndex].Id}");
            ShowCustomersList();
            throw new Exception("Data Successfully Deleted");
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

 
        private void UpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Content = JsonContent.Create(CurrentItem),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:44337/api/Appointments", UriKind.Relative)
                };
                client.SendAsync(request).Wait();
                ShowAppointsmentsList();
                throw new Exception("Data Successfully Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void UpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage
            {
                Content = JsonContent.Create(CurrentItem),
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:44337/api/Customers", UriKind.Relative)
            };
            client.SendAsync(request).Wait();
            ShowCustomersList();
            throw new Exception("Data Successfully Updated");
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
}
    }
}
