using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Zadanie2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Order order;

        public MainWindow()
        {
            InitializeComponent();
            order = new Order();
        }

        private void DisplayFinalSummary()
        {
            TextBox_Summary.Text = order.ToString();
        }

        #region Buttons
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Zamówienie zostało przyjęte", "Zrobione", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region Checkboxes
        private void ColorPaper_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = ((CheckBox)sender).IsChecked ?? false;
            ComboBox_PaperColors.IsEnabled = isChecked;
            order.BonusOptionsMultiplier += isChecked ? 0.5 : -0.5;
            DisplayFinalSummary();
        }

        private void BonusOptions_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            order.BonusOptionsMultiplier += cb.IsChecked == true ? double.Parse(cb.Tag.ToString()) : -double.Parse(cb.Tag.ToString());
            DisplayFinalSummary();
        }
        #endregion

        #region RadioButtons
        private void Grammage_Change(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Tag)
            {
                case "80":
                    order.GrammageChangeMultiplier = 1;
                    break;
                case "120":
                    order.GrammageChangeMultiplier = 2;
                    break;
                case "200":
                    order.GrammageChangeMultiplier = 2.5;
                    break;
                case "240":
                    order.GrammageChangeMultiplier = 3;
                    break;
            }
            order.Grammary = ((RadioButton)sender).Tag + " g/m²";
            DisplayFinalSummary();
        }

        private void LeadTime_Change(object sender, RoutedEventArgs e)
        {
            order.LeadTimeAddedCost = int.Parse(((RadioButton)sender).Tag.ToString());
            order.LeadTime = ((RadioButton)sender).Tag.ToString() == "0" ? "Standard": "Ekspresowy";
            DisplayFinalSummary();
        }
        #endregion

        private void PaperColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            order.PaperColor = cmb.SelectedItem.ToString();
            DisplayFinalSummary();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            order.BasePrice = 0.2;
            for (int i = 0; i < Math.Round(((Slider)sender).Value); i++)
                order.BasePrice *= 2.5;
            Label_Format.Content = "A" + (5 - Math.Round(((Slider)sender).Value)) + " - cena " + order.BasePrice * 100 + "gr/szt.";
            DisplayFinalSummary();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                order.Count = int.Parse(textBox.Text);
                DisplayFinalSummary();
            }
            else
            {
                TextBox_Summary.Text = "";
            }
        }

        private void ValidateText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
