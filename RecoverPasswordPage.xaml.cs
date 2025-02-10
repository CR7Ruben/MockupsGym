namespace Mockup;

public partial class RecoverPasswordPage : ContentPage
{
    public RecoverPasswordPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Desactivar el bot�n al principio
        ContinueButton.IsEnabled = false;

        // Asignar evento al Entry para controlar la longitud del texto
        UserEntry.TextChanged += OnUserEntryTextChanged;
    }

    // Validar que solo se ingresen exactamente 6 d�gitos
    private void OnUserEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Limitar a 6 d�gitos
        if (username.Length > 6)
        {
            UserEntry.Text = username.Substring(0, 6); // Limitar a 6 d�gitos
        }

        // Validaci�n del usuario
        if (username.Length != 6)
        {
            ErrorLabel.Text = "El usuario debe tener 6 d�gitos.";
            ErrorLabel.IsVisible = true;

            // Desactivar el bot�n si no tiene 6 d�gitos
            ContinueButton.IsEnabled = false;
        }
        else
        {
            ErrorLabel.IsVisible = false;

            // Habilitar el bot�n cuando tiene 6 d�gitos
            ContinueButton.IsEnabled = true;
        }
    }

    // Navegar a la p�gina de "Ingresar C�digo"
    private async void OnContinueClicked(object sender, EventArgs e)
    {
        string username = UserEntry.Text ?? "";

        // Verificar si el campo est� vac�o o no tiene 6 d�gitos
        if (string.IsNullOrWhiteSpace(username))
        {
            ErrorLabel.Text = "Por favor ingresa un n�mero de usuario.";
            ErrorLabel.IsVisible = true;
            return;
        }

        if (username.Length != 6)
        {
            ErrorLabel.Text = "El n�mero de usuario debe tener 6 d�gitos.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Si la validaci�n es correcta, navegar a la siguiente p�gina
        await Shell.Current.GoToAsync("//EnterCodePage");
    }

    // Navegar a la p�gina de registro
    public Command GoToRegister => new Command(async () =>
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    });
}
