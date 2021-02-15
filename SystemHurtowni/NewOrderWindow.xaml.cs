using ProjektZaliczeniowyProgramowanie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SystemHurtowni
{
    /// <summary>
    /// Logika interakcji dla klasy NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public NewOrderWindow()
        {
            InitializeComponent();
            LoadDataFromDB();
        }

        private void BtnNewOrderApply_Click(object sender, RoutedEventArgs e)
        {
            bool isValidForm = true;

            if (datePickerOrderDate.SelectedDate == null)
            {
                isValidForm = false;
                ShowMessage("Nie podano daty zamówienia.", "Błąd", MessageBoxImage.Error);
            }
            else if (datePickerOrderDate.SelectedDate < DateTime.Today)
            {
                isValidForm = false;
                ShowMessage("Data zamówienia nie może być dniem przeszłym.", "Błąd", MessageBoxImage.Error);
            }

            if (datePickerReciveDate.SelectedDate == null)
            {
                isValidForm = false;
                ShowMessage("Nie podano daty dostarczenia.", "Błąd", MessageBoxImage.Error);
            }
            else if (datePickerOrderDate.SelectedDate >= datePickerReciveDate.SelectedDate)
            {
                isValidForm = false;
                ShowMessage("Data dostarczenia musi być przynajmniej 1 dniem różnicy od daty zamówienia.", "Błąd", MessageBoxImage.Error);
            }

            if (comboBoxProducts.SelectedItem == null || comboBoxCarrier.SelectedItem == null || comboBoxRecipient.SelectedItem == null ||
                textBoxProductQuantity.Text == "" || textBoxAdress.Text == "" || textBoxCity.Text == "")
            {
                isValidForm = false;
                ShowMessage("Nie wypełniono wszystkich pól formularza.", "Błąd", MessageBoxImage.Error);
            }
            
            string productName = comboBoxProducts.SelectedItem != null ? comboBoxProducts.SelectedItem.ToString() : "";
            bool validQuantity = int.TryParse(textBoxProductQuantity.Text, out int productQuantity);
            string carrierName = comboBoxCarrier.SelectedItem != null ? comboBoxCarrier.SelectedItem.ToString() : "";
            string recipientName = comboBoxRecipient.SelectedItem != null ? comboBoxRecipient.SelectedItem.ToString() : "";

            if (isValidForm && validQuantity)
            {
                using (var context = new BazaDanychHurtowniaEntities())
                {
                    var productID = context.Product.Single(x => x.Name == productName);
                    var carrierID = context.Carrier.Single(x => x.Name == carrierName);
                    var recipientID = context.Recipient.Single(x => x.Name == recipientName);

                    context.Order.Add(new Order
                    {
                        ProductID = productID.ID,
                        Quantity = productQuantity,
                        OrderDate = datePickerOrderDate.SelectedDate.Value,
                        ReciveDate = datePickerReciveDate.SelectedDate.Value,
                        CarrierID = carrierID.ID,
                        RecipientID = recipientID.ID,
                        Adress = textBoxAdress.Text,
                        City = textBoxCity.Text,
                    });

                    context.SaveChanges();
                    ShowMessage("Zamówienie zostało złożone pomyślnie.", "Informacja", MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                ShowMessage("Podano niewłaściwe dane.", "Błąd", MessageBoxImage.Error);
            }
        }

        private void BtnNewOrderExit_Click(object sender, RoutedEventArgs e) => Close();
        /// <summary>
        /// Funkcja Ładująca dane do formularza z bazy danych.
        /// </summary>
        private void LoadDataFromDB()
        {
            using (var context = new BazaDanychHurtowniaEntities())
            {
                var productsInStorage = context.Storage;
                foreach (var item in productsInStorage)
                {
                    var availableProduct = context.Product.Single(x => x.ID == item.ProductID);
                    comboBoxProducts.Items.Add(availableProduct.Name);
                }

                var carriers = context.Carrier;
                foreach (var item in carriers)
                {
                    comboBoxCarrier.Items.Add(item.Name);
                }

                var recipients = context.Recipient;
                foreach (var item in recipients)
                {
                    comboBoxRecipient.Items.Add(item.Name);
                }
            }
        }
        /// <summary>
        /// Funkcja wyświetlająca informacje zwrotne dla użytkownika
        /// </summary>
        /// <param name="msg">Informacja</param>
        /// <param name="name">Nazwa okna</param>
        /// <param name="img">Ikonka</param>
        private void ShowMessage(string msg, string name, MessageBoxImage img)
        {
            MessageBox.Show(msg, name, MessageBoxButton.OK, img);
        }
    }
}
