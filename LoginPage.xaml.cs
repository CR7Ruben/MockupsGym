using System.Text.RegularExpressions;
namespace Mockup
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command GoToRegister => new Command(async () => await Navigation.PushAsync(new RegisterPage()));
        public Command GoToRecoverPassword => new Command(async () => await Navigation.PushAsync(new RecoverPasswordPage()));

        // Validaci�n de los 6 d�gitos del usuario
        private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
        {
            string username = UsernameEntry.Text ?? "";

            if (username.Length > 6)
            {
                UsernameEntry.Text = username.Substring(0, 6);
            }

            if (username.Length == 6)
            {
                ErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = !string.IsNullOrWhiteSpace(PasswordEntry.Text) && IsValidPassword(PasswordEntry.Text);
            }
            else
            {
                ErrorLabel.Text = "El usuario debe tener 6 d�gitos.";
                ErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
        }

        // Validaci�n de la contrase�a
        private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
        {
            string password = PasswordEntry.Text ?? "";

            if (!IsValidPassword(password))
            {
                PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un car�cter especial.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else if (UsernameEntry.Text?.Length != 6)
            {
                PasswordErrorLabel.Text = "El usuario debe tener 6 d�gitos.";
                PasswordErrorLabel.IsVisible = true;
                LoginButton.IsEnabled = false;
            }
            else
            {
                PasswordErrorLabel.IsVisible = false;
                LoginButton.IsEnabled = true;
            }
        }

        // Expresi�n regular para validar la contrase�a
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_.*?&])[A-Za-z\d@$!%-_*.?&]{8,}$");
            return regex.IsMatch(password);
        }

        // M�todo de inicio de sesi�n (Redirige a la p�gina del dashboard)
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string userNumber = UsernameEntry.Text ?? "";

            if (userNumber.Length != 6)
            {
                await DisplayAlert("Error", "El usuario debe tener 6 d�gitos.", "OK");
                return;
            }

            // Redirigir a la pantalla del dashboard y pasar el n�mero de usuario
            await Shell.Current.GoToAsync($"//UserDashboardPage?username=Rub�n Gonz�lez&userNumber={userNumber}");
        }
    }
}
