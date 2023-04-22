
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NCalc;


namespace CalculatorFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            displayCursorAtStart();
            listBoxResults.ItemsSource = Results;


        }

        private ObservableCollection<string> _Results = new ObservableCollection<string>();
        public ObservableCollection<string> Results
        {
            get { return _Results; }
            set { _Results = value; }
        }

        private void textToClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Display.Text += button.Content.ToString();
            displayCursorAtStart();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 0)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
            displayCursorAtStart();

        }

        private void displayCursorAtStart()
        {
            Display.Focus();
            Display.SelectionStart = Display.Text.Length;


        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "";
            displayCursorAtStart();

        }

        // Handles Invalid Char
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e) =>
            e.Handled = !e.Text.Any(x => Char.IsDigit(x));

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            double dblResult = double.NaN;
            try
            {
                dblResult = Evaluate(Display.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Message:\n" + ex.Message);
                return;
            }
            Results.Insert(0, Display.Text + "\t=" + dblResult.ToString("N9"));
        }



        public static double Evaluate(String input)
        {
            NCalc.Expression e = new NCalc.Expression(input);
            var result = e.Evaluate();
            return Convert.ToDouble(result.ToString());
        }
    }
}
