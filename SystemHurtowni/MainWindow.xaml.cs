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
using System.Data.Entity.Migrations;
using ProjektZaliczeniowyProgramowanie;

namespace SystemHurtowni
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
            btnBackProduct.IsEnabled = true; btnBackProduct.Visibility = Visibility.Visible;
        }

        private void BtnOrders_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = false; btnProducts.Visibility = Visibility.Hidden;
            btnOrders.IsEnabled = false; btnOrders.Visibility = Visibility.Hidden;
            btnStorage.IsEnabled = false; btnStorage.Visibility = Visibility.Hidden;
            btnExitApp.IsEnabled = false; btnExitApp.Visibility = Visibility.Hidden;

            btnNewOrder.IsEnabled = true; btnNewOrder.Visibility = Visibility.Visible;
            btnActualOrders.IsEnabled = true; btnActualOrders.Visibility = Visibility.Visible;
            btnBackOrders.IsEnabled = true; btnBackOrders.Visibility = Visibility.Visible;
        }

        private void BtnStorage_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = false; btnProducts.Visibility = Visibility.Hidden;
            btnOrders.IsEnabled = false; btnOrders.Visibility = Visibility.Hidden;
            btnStorage.IsEnabled = false; btnStorage.Visibility = Visibility.Hidden;
            btnExitApp.IsEnabled = false; btnExitApp.Visibility = Visibility.Hidden;

            btnAddStorageProduct.IsEnabled = true; btnAddStorageProduct.Visibility = Visibility.Visible;
            btnShowStorageProduct.IsEnabled = true; btnShowStorageProduct.Visibility = Visibility.Visible;
            btnBackStorage.IsEnabled = true; btnBackStorage.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Funkcja pokazująca aktualny czas w programie.
        /// </summary>
        public void UpdateDateTime()
        {
            DispatcherTimer clock = new DispatcherTimer();

            clock.Interval = new TimeSpan(0, 0, 1);
            clock.Tick += TimerTick;
            clock.Start();
        }
        /// <summary>
        /// Funkcja Licząca aktualną datę i godzinę
        /// </summary>
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

            using (var context = new BazaDanychHurtowniaEntities())
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

        private void BtnBackProduct_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = true; btnProducts.Visibility = Visibility.Visible;
            btnOrders.IsEnabled = true; btnOrders.Visibility = Visibility.Visible;
            btnStorage.IsEnabled = true; btnStorage.Visibility = Visibility.Visible;
            btnExitApp.IsEnabled = true; btnExitApp.Visibility = Visibility.Visible;

            btnAddNewProduct.IsEnabled = false; btnAddNewProduct.Visibility = Visibility.Hidden;
            btnShowAllProducts.IsEnabled = false; btnShowAllProducts.Visibility = Visibility.Hidden;
            btnDeleteProduct.IsEnabled = false; btnDeleteProduct.Visibility = Visibility.Hidden;
            btnBackProduct.IsEnabled = false; btnBackProduct.Visibility = Visibility.Hidden;
        }

        private void BtnAddStorageProduct_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();

            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -140),
            };

            Label labelProductName = new Label
            {
                Content = "Wybierz produkt z listy",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
                Margin = new Thickness(0, 10, 0, 0),
                Width = 300,
            };

            ComboBox productStorageList = new ComboBox
            {
                Width = 300,
                MaxDropDownHeight = 100,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };

            using (var context = new BazaDanychHurtowniaEntities())
            {
                foreach (var item in context.Product)
                {
                    productStorageList.Items.Add(item.Name);
                }
            }

            Label labelProductQuantity = new Label
            {
                Content = "Podaj ilość",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
                Margin = new Thickness(0, 10, 0, 0),
                Width = 100,
            };

            TextBox productQuantity = new TextBox
            {
                Background = Brushes.LightGray,
                MaxLength = 5,
                Width = 100,
                Height = 30,
                FontSize = 15,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            Button buttonAddStorageProduct = new Button
            {
                Content = "Dodaj",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
                Margin = new Thickness(10),
            };

            Button buttonAddStorageProductsBack = new Button
            {
                Content = "Anuluj",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
                Margin = new Thickness(10),
            };

            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 2);

            sp.Children.Add(labelProductName);
            sp.Children.Add(productStorageList);
            sp.Children.Add(labelProductQuantity);
            sp.Children.Add(productQuantity);
            sp.Children.Add(buttonAddStorageProduct);
            sp.Children.Add(buttonAddStorageProductsBack);
            mainWindow.Children.Add(sp);

            buttonAddStorageProduct.Click += AddStorageProductDelegate;
            buttonAddStorageProductsBack.Click += DeleteStorageProductCancel;

            void AddStorageProductDelegate(object o, EventArgs ev)
            {
                AddStorageProduct(productStorageList.SelectedItem, productQuantity.Text.ToString());
            }
            void DeleteStorageProductCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }

        private void BtnShowStorageProduct_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -140),
            };

            Label labelStorageList = new Label
            {
                Content = "Lista produktów na magazynie i ich dostępna ilość",
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

            Button buttonStorageProductsBack = new Button
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

            sp.Children.Add(labelStorageList);
            sp.Children.Add(richTextBoxProducts);
            sp.Children.Add(buttonStorageProductsBack);

            mainWindow.Children.Add(sp);

            using (var context = new BazaDanychHurtowniaEntities())
            {
                int counter = 0;

                foreach (var item in context.Storage)
                {
                    var product = context.Product.Where(x => x.ID == item.ProductID).First();

                    counter++;
                    richTextBoxProducts.AppendText
                    (
                        counter + "  |  " + product.Name + "  |  " + item.Quantity.ToString() + "szt\n"
                    );
                }
            }

            buttonStorageProductsBack.Click += StorageProductsCancel;

            void StorageProductsCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }

        private void BtnBackStorage_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = true; btnProducts.Visibility = Visibility.Visible;
            btnOrders.IsEnabled = true; btnOrders.Visibility = Visibility.Visible;
            btnStorage.IsEnabled = true; btnStorage.Visibility = Visibility.Visible;
            btnExitApp.IsEnabled = true; btnExitApp.Visibility = Visibility.Visible;

            btnAddStorageProduct.IsEnabled = false; btnAddStorageProduct.Visibility = Visibility.Hidden;
            btnShowStorageProduct.IsEnabled = false; btnShowStorageProduct.Visibility = Visibility.Hidden;
            btnBackStorage.IsEnabled = false; btnBackStorage.Visibility = Visibility.Hidden;
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = new NewOrderWindow();
            newOrderWindow.Show();
        }

        private void BtnActualOrders_Click(object sender, RoutedEventArgs e)
        {
            StackPanel sp = new StackPanel
            {
                Height = 300,
                Background = Brushes.Gray,
                Margin = new Thickness(40, -40, 40, -160),
            };

            Label labelOrderList = new Label
            {
                Content = "Oczekujące na realizację zamówienia",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
            };

            Label labelInfoOrder = new Label
            {
                Content = "Nazwa produktu | Ilość | Data zamówienia | Data odbioru | Przewoźnik | Odbiorca | Adres | Miasto",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
                FontWeight = FontWeights.Bold,
                FontSize = 11,
            };

            Label labelOrderPrice = new Label
            {
                Content = "Cena wybranego zamówienia",
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Background = Brushes.LightGray,
                FontWeight = FontWeights.Bold,
                Width = 200,
                Margin = new Thickness(0, 10, 0, 0),
            };

            TextBox textBoxOrderPrice = new TextBox
            {
                IsReadOnly = true,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                Width = 200,
                Height = 30,
            };

            ComboBox comboBoxOrderList = new ComboBox
            {
                MaxDropDownHeight = 100,
                Margin = new Thickness(0, 10, 0, 0),
                FontSize = 10,
            };

            using (var context = new BazaDanychHurtowniaEntities())
            {
                ComboBoxItem comboBoxOrder = null;
                var orders = context.Order;
                foreach (var item in orders)
                {
                    var product = context.Product.Single(x => x.ID == item.ProductID);
                    var carrier = context.Carrier.Single(x => x.ID == item.CarrierID);
                    var reciepient = context.Recipient.Single(x => x.ID == item.RecipientID);

                    comboBoxOrder = new ComboBoxItem
                    {
                        Content = $"{product.Name} | {item.Quantity} | {item.OrderDate.ToShortDateString()}" +
                        $" | {item.ReciveDate.ToShortDateString()} | {carrier.Name} | {reciepient.Name} | {item.Adress} | {item.City}",
                        Background = Brushes.PaleTurquoise,
                    };

                    comboBoxOrderList.Items.Add(comboBoxOrder);
                }
            }

            Button buttonSubmitOrder = new Button
            {
                Content = "Realizacja",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
                Margin = new Thickness(10),
            };

            Button buttonActualOrdersBack = new Button
            {
                Content = "Powrót",
                Padding = new Thickness(10),
                Width = 100,
                Height = 40,
                FontWeight = FontWeights.Bold,
                Background = Brushes.RoyalBlue,
                Margin = new Thickness(10),
            };

            Grid.SetColumn(sp, 1);
            Grid.SetRow(sp, 2);

            sp.Children.Add(labelOrderList);
            sp.Children.Add(labelInfoOrder);
            sp.Children.Add(comboBoxOrderList);
            sp.Children.Add(labelOrderPrice);
            sp.Children.Add(textBoxOrderPrice);
            sp.Children.Add(buttonSubmitOrder);
            sp.Children.Add(buttonActualOrdersBack);

            mainWindow.Children.Add(sp);
            /// <summary>
            /// Delegaty przechowywujące funkcję interaktywne dla użytkownika.
            /// </summary>
            comboBoxOrderList.SelectionChanged += CalculatePriceDelegate;
            buttonSubmitOrder.Click += SubmitOrderDelegate;
            buttonActualOrdersBack.Click += ActualOrdersCancel;

            void CalculatePriceDelegate(object o, EventArgs ev)
            {
                CalculateFullPriceOrder(comboBoxOrderList.SelectedItem.ToString(), ref textBoxOrderPrice);
            }
            void SubmitOrderDelegate(object o, EventArgs ev)
            {
                if (comboBoxOrderList.SelectedItem != null) SubmitOrder(comboBoxOrderList.SelectedItem.ToString());
                else ShowMessage("Nie wybrano zamówienia.", "Błąd", MessageBoxImage.Error);
            }
            void ActualOrdersCancel(object o, EventArgs ev) => mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
        }
        private void BtnBackOrders_Click(object sender, RoutedEventArgs e)
        {
            btnProducts.IsEnabled = true; btnProducts.Visibility = Visibility.Visible;
            btnOrders.IsEnabled = true; btnOrders.Visibility = Visibility.Visible;
            btnStorage.IsEnabled = true; btnStorage.Visibility = Visibility.Visible;
            btnExitApp.IsEnabled = true; btnExitApp.Visibility = Visibility.Visible;

            btnNewOrder.IsEnabled = false; btnNewOrder.Visibility = Visibility.Hidden;
            btnActualOrders.IsEnabled = false; btnActualOrders.Visibility = Visibility.Hidden;
            btnBackOrders.IsEnabled = false; btnBackOrders.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Funkcja dodająca produkt do bazy danych
        /// </summary>
        /// <param name="name">Nazwa produktu</param>
        /// <param name="price">Cane produktu</param>
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
        /// <summary>
        /// Funckja usuwająca produkt z bazdy danych
        /// </summary>
        /// <param name="name">Nazawa produktu</param>
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
                        var p = context.Storage.Single(x => x.ProductID == product.ID);
                        context.Storage.Remove(p);
                        context.Product.Remove(product);
                        context.SaveChanges();
                        ShowMessage("Produkt został usunięty.", "Informacja", MessageBoxImage.Information);
                        mainWindow.Children.RemoveAt(mainWindow.Children.Count - 1);
                    }

                }
            }
        }
        /// <summary>
        /// Funkcja dodająca produkt do magazynu
        /// </summary>
        /// <param name="value">Wybrany produkt z magazynu</param>
        /// <param name="s">Nazwa Produktu</param>
        private void AddStorageProduct(object value, string s)
        {
            string productName = value as string;
            Product productToAdd = null;
            Storage findProduct = null;

            /*
                Zasada działania: Najpierw szuka produktu na liście dostępnych w Product,
                następnie sprawdza, czy w Storage znajduje się na liście, jeśli nie to dodaje
                wraz z ilością. Jeśli natomiast istnieje to aktualizuje jego dostępną ilość.
             */

            bool isValidQuantity = int.TryParse(s, out int quantity);

            if (isValidQuantity && productName != null)
            {
                using (var context = new BazaDanychHurtowniaEntities())
                {
                    productToAdd = context.Product.Single(x => x.Name == productName);
                    findProduct = context.Storage.SingleOrDefault(x => x.ProductID == productToAdd.ID);

                    if (findProduct == null)
                    {
                        context.Storage.Add(new Storage
                        {
                            ProductID = productToAdd.ID,
                            Quantity = quantity
                        });

                        context.SaveChanges();
                        ShowMessage("Nowy produkt został dodany na magazyn.", "Informacja", MessageBoxImage.Information);
                    }

                    else
                    {
                        var product = context.Storage.Where(x => x.ProductID == productToAdd.ID).First();
                        product.Quantity += quantity;

                        context.SaveChanges();
                        ShowMessage("Ilość produktu została zwiększona.", "Informacja", MessageBoxImage.Information);
                    }
                }
            }
            else ShowMessage("Wprowadzona ilość jest nieprawidłowa.", "Błąd", MessageBoxImage.Error);
        }
        /// <summary>
        /// Funkcja obliczająca wartość zamówienia
        /// </summary>
        /// <param name="s">Wybrany produkt z magazynu</param>
        /// <param name="textBox">okienko ceny</param>
        private void CalculateFullPriceOrder(string s, ref TextBox textBox)
        {
            string[] order = s.Substring(s.IndexOf(':') + 2).Split('|');
            var productSelected = order[0].Trim();
            var productQuantity = order[1].Trim();

            using (var context = new BazaDanychHurtowniaEntities())
            {
                var product = context.Product.Single(x => x.Name == productSelected);
                decimal fullPrice = product.Price * Convert.ToInt32(productQuantity);
                textBox.Text = Math.Round(fullPrice, 2) + "zł";
            }
        }
        /// <summary>
        /// Funkcja Potwierdzająca złożenie zamówienia
        /// </summary>
        /// <param name="s">nazwa zamówienia</param>
        private void SubmitOrder(string s)
        {
            string[] order = s.Substring(s.IndexOf(':') + 2).Split('|');
            var productSelected = order[0].Trim();
            var productQuantity = order[1].Trim();
            var carrierName = order[4].Trim();
            var recipientName = order[5].Trim();
            var adressName = order[6].Trim();
            var cityName = order[7].Trim();

            using (var context = new BazaDanychHurtowniaEntities())
            {
                var productToOrder = context.Product.Single(x => x.Name == productSelected);
                var storageProduct = context.Storage.Single(x => x.ProductID == productToOrder.ID);

                if (storageProduct.Quantity < int.Parse(productQuantity))
                {
                    ShowMessage("Brak odpowiedniej ilośc produktu na magazynie do zrealizowania zamówienia.",
                        "Błąd", MessageBoxImage.Error);
                }
                else
                {
                    var productStorageUpdate = context.Storage.Single(x => x.ProductID == productToOrder.ID);
                    productStorageUpdate.Quantity -= int.Parse(productQuantity);

                    var carrierID = context.Carrier.Single(x => x.Name == carrierName);
                    var recipientID = context.Recipient.Single(x => x.Name == recipientName);

                    int quantity = int.Parse(productQuantity);

                    Order orderToDelete = context.Order.Single(x => x.ProductID == productToOrder.ID &&
                    x.Quantity == quantity && x.CarrierID == carrierID.ID &&
                    x.RecipientID == recipientID.ID && x.Adress == adressName && x.City == cityName);

                    context.Order.Remove(orderToDelete);
                    context.SaveChanges();
                    ShowMessage("Zamówienie zostało wysłane pomyślnie.", "Informacja", MessageBoxImage.Information);
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
