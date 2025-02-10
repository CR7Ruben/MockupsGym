using System.Text.RegularExpressions;

namespace Mockup;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Validaci�n del usuario (solo 6 d�gitos)
    private void OnUsernameTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UsernameEntry.Text ?? "";

        if (username.Length == 6)
        {
            UsernameErrorLabel.IsVisible = false;
            RegisterButton.IsEnabled = IsValidPassword(PasswordEntry.Text) && PasswordEntry.Text == ConfirmPasswordEntry.Text;
        }
        else
        {
            UsernameErrorLabel.Text = "El usuario debe tener 6 d�gitos.";
            UsernameErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
    }

    // Validaci�n de la contrase�a y la confirmaci�n de la contrase�a
    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        if (!IsValidPassword(password))
        {
            PasswordErrorLabel.Text = "La contrase�a debe tener al menos 8 caracteres.\nUna may�scula.\nUna min�scula.\nUn n�mero y un car�cter especial.";
            PasswordErrorLabel.IsVisible = true;
            RegisterButton.IsEnabled = false;
        }
        else
        {
            PasswordErrorLabel.IsVisible = false;

            if (password != confirmPassword)
            {
                ConfirmPasswordErrorLabel.Text = "Las contrase�as no coinciden.";
                ConfirmPasswordErrorLabel.IsVisible = true;
                RegisterButton.IsEnabled = false;
            }
            else
            {
                ConfirmPasswordErrorLabel.IsVisible = false;
                RegisterButton.IsEnabled = true;
            }
        }
    }

    // M�todo para manejar el clic en "Registrarse"
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text ?? "";
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        // Validaciones antes de proceder
        if (string.IsNullOrWhiteSpace(username) || username.Length != 6)
        {
            await DisplayAlert("Error", "El usuario debe tener 6 d�gitos.", "OK");
            return;
        }

        if (!IsValidPassword(password))
        {
            await DisplayAlert("Error", "La contrase�a debe cumplir con los requisitos.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
            return;
        }

        // Mostrar mensaje de �xito antes de redirigir al LoginPage
        await DisplayAlert("�xito", "Tu registro ha sido exitoso.", "Iniciar sesi�n");

        // Navegar a LoginPage
        await Shell.Current.GoToAsync("//LoginPage");
    }

    // Expresi�n regular para validar la contrase�a
    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) return false;

        var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%-_*?&])[A-Za-z\d@$!%-_*?&]{8,}$");
        return regex.IsMatch(password);
    }
}
