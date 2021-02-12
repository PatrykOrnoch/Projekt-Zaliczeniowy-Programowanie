using ProjektZaliczeniwyProgramowanie;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ObslugaHurtowniiApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateDateTime();
        }

        private void BtnExitApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show(
                "Czy na pewno chcesz wyjść ?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (answer == MessageBoxResult.Yes) Close();
        }

        private void BtnProducts_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = false; btnProducts.Visibility = Visibility.Hidden;
            btnOrders.IsEnabled = false; btnOrders.Visibility = Visibility.Hidden;
            btnStorage.IsEnabled = false; btnStorage.Visibility = Visibility.Hidden;
            btnExitApp.IsEnabled = false; btnExitApp.Visibility = Visibility.Hidden;

            btnAddNewProduct.IsEnabled = true; btnAddNewProduct.Visibility = Visibility.Visible;
            btnShowAllProducts.IsEnabled = true; btnShowAllProducts.Visibility = Visibility.Visible;
            btnDeleteProduct.IsEnabled = true; btnDeleteProduct.Visibility = Visibility.Visible;
            btnBack.IsEnabled = true; btnBack.Visibility = Visibility.Visible;
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnStorage_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateDateTime()
        {
            DispatcherTimer clock = new DispatcherTimer();

            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Tick += TimerTick;
            clock.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            string day = DateTime.Now.ToString("dddd");
            string month = DateTime.Now.ToString("MMMM");

            lblDateTime.Content = "Dzisiaj jest " + DateTime.Now.Day + " " + month + " " + day +
                                  ", godzina " + DateTime.Now.ToLongTimeString();
        }

        private void BtnAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -110),
            };

            Label labelProductName = new Label
            {
                Content = "Nazwa produktu",
                Background = Brushes.AliceBlue,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            TextBox textBoxProductName = new TextBox
            {
                Background = Brushes.LightGray,
                MaxLength = 30,
                Width = 300,
                Margin = new Thickness(5),
                Height = 30,
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            Label labelProductPrice = new Label
            {
                Content = "Cena produktu (1 szt)",
                Background = Brushes.AliceBlue,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            TextBox textBoxProductPrice = new TextBox
            {
                Background = Brushes.LightGray,
                MaxLength = 8,
                Width = 80,
                Margin = new Thickness(5),
                Height = 30,
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            Button buttonAddProduct = new Button
            {
                Content = "Dodaj",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
            };

            Button buttonAddProductCancel = new Button
            {
                Content = "Anuluj",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
            };

            sp.Children.Add(labelProductName);
            sp.Children.Add(textBoxProductName);
            sp.Children.Add(labelProductPrice);
            sp.Children.Add(textBoxProductPrice);
            sp.Children.Add(buttonAddProductCancel);
            sp.Children.Add(buttonAddProduct);

            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 2);

            buttonAddProduct.Click += AddProductDelegate;
            buttonAddProductCancel.Click += AddProductCancel;
            mainWindow.Children.Add(sp);

            void AddProductDelegate(object o, EventArgs ev) => AddProduct(textBoxProductName.Text, textBoxProductPrice.Text);
            void AddProductCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -90),
            };

            Label labelProductName = new Label
            {
                Content = "Podaj nazwę produktu do usunięcia",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
            };

            Label labelProductWarning = new Label
            {
                Content = "Uwaga, produkt zostanie również usunięty z magazynu!",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Width = 300,
                Background = Brushes.DarkRed,
                Foreground = Brushes.Pink,
                FontWeight = FontWeights.Bold,
                FontSize = 10,
                Margin = new Thickness(0, 20, 0, 0),
            };

            TextBox textBoxProductName = new TextBox
            {
                MaxLength = 30,
                Width = 300,
                Height = 30,
                FontSize = 15,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 0, 0, 10),
            };

            Button buttonDeleteProduct = new Button
            {
                Content = "Usuń",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
            };

            Button buttonDeleteProductCancel = new Button
            {
                Content = "Anuluj",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
            };

            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 2);

            sp.Children.Add(labelProductName);
            sp.Children.Add(labelProductWarning);
            sp.Children.Add(textBoxProductName);
            sp.Children.Add(buttonDeleteProductCancel);
            sp.Children.Add(buttonDeleteProduct);

            buttonDeleteProduct.Click += DeleteProductDelegate;
            buttonDeleteProductCancel.Click += DeleteProductCancel;
            mainWindow.Children.Add(sp);

            void DeleteProductDelegate(object o, EventArgs ev) => DeleteProduct(textBoxProductName.Text);
            void DeleteProductCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }

        private void BtnShowAllProducts_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -140),
            };

            Label labelProductList = new Label
            {
                Content = "Lista produktów na liście wraz z ceną za 1 szt",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
            };

            RichTextBox richTextBoxProducts = new RichTextBox
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Height = 160,
                IsReadOnly = true,
                FontSize = 14,
                FontWeight = FontWeights.Bold,
            };

            Button buttonAllProductsBack = new Button
            {
                Content = "Zamknij",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
                Margin = new Thickness(10),
            };

            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 2);

            sp.Children.Add(labelProductList);
            sp.Children.Add(richTextBoxProducts);
            sp.Children.Add(buttonAllProductsBack);

            mainWindow.Children.Add(sp);

            using (var context = new BazaDanychHurtowniaEntities ())
            {
                int counter = 0;

                foreach (var item in context.Product)
                {
                    counter++;

                    richTextBoxProducts.AppendText
                    (
                        counter + "  |  " + item.Name + "  |  " + Math.Round(item.Price, 2).ToString() + "zł\n"
                    );
                }
            }

            buttonAllProductsBack.Click += AllProductsCancel;

            void AllProductsCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = true; btnProducts.Visibility = Visibility.Visible;
            btnOrders.IsEnabled = true; btnOrders.Visibility = Visibility.Visible;
            btnStorage.IsEnabled = true; btnStorage.Visibility = Visibility.Visible;
            btnExitApp.IsEnabled = true; btnExitApp.Visibility = Visibility.Visible;

            btnAddNewProduct.IsEnabled = false; btnAddNewProduct.Visibility = Visibility.Hidden;
            btnShowAllProducts.IsEnabled = false; btnShowAllProducts.Visibility = Visibility.Hidden;
            btnDeleteProduct.IsEnabled = false; btnDeleteProduct.Visibility = Visibility.Hidden;
            btnBack.IsEnabled = false; btnBack.Visibility = Visibility.Hidden;
        }

        private void AddProduct(string name, string price)
        {
            bool isValidPrice = true;
            isValidPrice = decimal.TryParse(price, out decimal productPrice);

            if (name.Length < 3)
            {
                ShowMessage("Podana nazwa produktu jest za krótka. Wymagana długość to 3 znaki.", "Błąd", MessageBoxImage.Error);
                mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
            }
            else if (!isValidPrice)
            {
                ShowMessage("Podana cena jest nieprawidłowa.", "Błąd", MessageBoxImage.Error);
                mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
            }
            else
            {
                using (var context = new BazaDanychHurtowniaEntities())
                {
                    context.Product.Add(new Product
                    {
                        Name = name,
                        Price = productPrice,
                    });

                    context.SaveChanges();
                    ShowMessage("Produkt został dodany.", "Informacja", MessageBoxImage.Information);
                    mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
                }
            }
        }

        private void DeleteProduct(string name)
        {
            if (name == "")
            {
                ShowMessage("Nie podano nazwy produktu.", "Błąd", MessageBoxImage.Error);
            }
            else
            {
                using (var context = new BazaDanychHurtowniaEntities())
                {
                    Product product = context.Product.SingleOrDefault(x => x.Name == name);

                    if (product is null)
                    {
                        ShowMessage("Nie znaleziono produktu w bazie.", "Błąd", MessageBoxImage.Error);
                    }
                    else
                    {
                        context.Product.Remove(product);
                        context.SaveChanges();
                        ShowMessage("Produkt został usunięty.", "Informacja", MessageBoxImage.Information);
                        mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
                    }

                }
            }
        }

        private void ShowMessage(string msg, string name, MessageBoxImage img)
        {
            MessageBox.Show(msg, name, MessageBoxButton.OK, img);
        }
    }
}
