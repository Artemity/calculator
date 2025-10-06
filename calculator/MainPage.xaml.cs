using Microsoft.Maui.Controls;
namespace calculator
{
    public partial class MainPage : ContentPage
    {
        private string currentInput = "0";
        private string operand1 = "";
        private string currentOperator = "";
        private bool isNewEntry = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private void UpdateDisplay()
        {
            ResultLabel.Text = currentInput;
        }

        private void OnDigitClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            string digit = button.Text;

            if (isNewEntry)
            {
                currentInput = digit;
                isNewEntry = false;
            }
            else
            {
                if (currentInput == "0")
                    currentInput = digit;
                else
                    currentInput += digit;
            }

            UpdateDisplay();
        }

        private void OnDecimalClicked(object sender, EventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ".";
                isNewEntry = false;
                UpdateDisplay();
            }
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            string op = button.Text switch
            {
                "÷" => "/",
                "×" => "*",
                "−" => "-",
                "+" => "+",
                _ => ""
            };

            if (!isNewEntry && currentOperator != "")
            {
                OnEqualsClicked(sender, e);
            }

            operand1 = currentInput;
            currentOperator = op;
            isNewEntry = true;
        }

        private void OnEqualsClicked(object sender, EventArgs e)
        {
            if (operand1 == "" || currentOperator == "") return;

            try
            {
                double num1 = double.Parse(operand1);
                double num2 = double.Parse(currentInput);
                double result = currentOperator switch
                {
                    "+" => num1 + num2,
                    "-" => num1 - num2,
                    "*" => num1 * num2,
                    "/" => num2 != 0 ? num1 / num2 : throw new DivideByZeroException(),
                    _ => num2
                };

                // Форматируем результат без лишних нулей
                currentInput = result % 1 == 0 ? result.ToString("F0") : result.ToString();
            }
            catch
            {
                currentInput = "Error";
            }

            operand1 = "";
            currentOperator = "";
            isNewEntry = true;
            UpdateDisplay();
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            currentInput = "0";
            operand1 = "";
            currentOperator = "";
            isNewEntry = true;
            UpdateDisplay();
        }

        private void OnPlusMinusClicked(object sender, EventArgs e)
        {
            if (currentInput != "0")
            {
                if (currentInput.StartsWith("-"))
                    currentInput = currentInput.Substring(1);
                else
                    currentInput = "-" + currentInput;
                UpdateDisplay();
            }
        }

        private void OnBackspaceClicked(object sender, EventArgs e)
        {
            if (currentInput.Length > 1)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1);
            }
            else
            {
                currentInput = "0";
            }
            UpdateDisplay();
        }
    }
}
