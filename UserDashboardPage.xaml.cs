using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace Mockup
{
    [QueryProperty(nameof(Username), "username")]
    [QueryProperty(nameof(UserNumber), "userNumber")]
    public partial class UserDashboardPage : ContentPage
    {
        private string _username;
        private string _userNumber;
        private bool _isRoutinePickerVisible;
        private bool _isDatePickerVisible;

        public ObservableCollection<string> RoutineOptions { get; } = new ObservableCollection<string>
        {
            "Pecho", "B�ceps", "Tr�ceps", "Espalda", "Pierna"
        };

        private string _selectedRoutine;
        public string SelectedRoutine
        {
            get => _selectedRoutine;
            set
            {
                _selectedRoutine = value;
                OnPropertyChanged(nameof(SelectedRoutine));
                UpdateDetailedRoutines();  // Actualizar las rutinas detalladas
                IsRoutineVisible = !string.IsNullOrEmpty(value); // Hacer visible el Picker de rutinas detalladas
            }
        }

        private ObservableCollection<string> _detailedRoutines = new ObservableCollection<string>();
        public ObservableCollection<string> DetailedRoutines
        {
            get => _detailedRoutines;
            set
            {
                _detailedRoutines = value;
                OnPropertyChanged(nameof(DetailedRoutines));
            }
        }

        private string _selectedDetailedRoutine;
        public string SelectedDetailedRoutine
        {
            get => _selectedDetailedRoutine;
            set
            {
                _selectedDetailedRoutine = value;
                OnPropertyChanged(nameof(SelectedDetailedRoutine));
                ShowRoutineDetailModal(); // Mostrar el modal cuando se selecciona una rutina
            }
        }

        private DateTime _selectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        // Esto controla la visibilidad del Picker de rutinas detalladas
        private bool _isRoutineVisible;
        public bool IsRoutineVisible
        {
            get => _isRoutineVisible;
            set
            {
                _isRoutineVisible = value;
                OnPropertyChanged(nameof(IsRoutineVisible));
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string UserNumber
        {
            get => _userNumber;
            set
            {
                _userNumber = value;
                OnPropertyChanged(nameof(UserNumber));
            }
        }

        public UserDashboardPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Obtener la zona horaria de Ciudad Obreg�n, Sonora, M�xico (UTC-7)
            TimeZoneInfo sonoraTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Hermosillo");

            // Obtener la fecha y hora actual en la zona horaria de Sonora
            DateTime sonoraNow = TimeZoneInfo.ConvertTime(DateTime.Now, sonoraTimeZone);

            // Establecer la fecha m�xima del DatePicker a la fecha actual en la zona horaria de Sonora
            ActivityDatePicker.MaximumDate = sonoraNow.Date;
        }

        // Mostrar y ocultar el Picker de rutinas
        private void OnShowRoutinePicker(object sender, EventArgs e)
        {
            // Si ya se ha seleccionado una rutina, desmarcarla y mostrar el picker nuevamente
            if (!string.IsNullOrEmpty(SelectedRoutine))
            {
                SelectedRoutine = null;  // Borra la rutina seleccionada
            }

            _isRoutinePickerVisible = !_isRoutinePickerVisible;
            RoutinePicker.IsVisible = _isRoutinePickerVisible;
        }

        // Mostrar y ocultar el DatePicker para la actividad
        private void OnShowDatePicker(object sender, EventArgs e)
        {
            _isDatePickerVisible = !_isDatePickerVisible;
            ActivityDatePicker.IsVisible = _isDatePickerVisible;
        }

        // Actualizar las rutinas detalladas seg�n el m�sculo seleccionado
        private void UpdateDetailedRoutines()
        {
            if (SelectedRoutine == "Pecho")
            {
                DetailedRoutines = new ObservableCollection<string>
                {
                    "Press inclinado con barra (4x8-10 reps)",
                    "Press inclinado con mancuernas (3x10-12 reps)",
                    "Aperturas en banco inclinado (3x12-15 reps)",
                    "Flexiones en banco inclinado (3x15-20 reps, hasta el fallo)",
                    "Press de banca plano con barra (4x8 reps)",
                    "Fondos en paralelas (dips) (4x12 reps)",
                    "Crossover en poleas (aperturas) (3x12-15 reps)",
                    "Press declinado con mancuernas o barra (3x10-12 reps)"
                };
            }
            else if (SelectedRoutine == "Tr�ceps")
            {
                DetailedRoutines = new ObservableCollection<string>
                {
                    "Fondos en banco (bench dips) (3x10-12 reps)",
                    "Extensi�n de tr�ceps por encima de la cabeza con mancuerna (3x10-12 reps)",
                    "Jalones de tr�ceps en polea (agarre recto) (3x10-12 reps)",
                    "Flexiones cerradas (tipo diamante) (3x8-12 reps)",
                    "Press franc�s con barra EZ (4x10-12 reps)",
                    "Extensiones de tr�ceps en polea (agarre cuerda) (4x12-15 reps)",
                    "Press cerrado con barra (4x8 reps)"
                };
            }
            else if (SelectedRoutine == "B�ceps")
            {
                DetailedRoutines = new ObservableCollection<string>
                {
                    "Curl con barra EZ (peso pesado) (4x6-8 reps)",
                    "Curl inclinado con mancuernas (3x10 reps)",
                    "Curl martillo con cuerda (polea) (3x12-15 reps)",
                    "Curl concentrado (3x12 reps por brazo)",
                    "Curl con barra recta (ligero) (3x12-15 reps)",
                    "Curl en predicador (banco Scott) (3x10-12 reps)",
                    "Curl en polea baja (agarre supino) (3x12-15 reps)"
                };
            }
            else if (SelectedRoutine == "Espalda")
            {
                DetailedRoutines = new ObservableCollection<string>
                {
                    "Dominadas (agarre amplio) (4x8-10 reps)",
                    "Jal�n al pecho en polea alta (agarre estrecho) (3x12 reps)",
                    "Remo en m�quina (agarre neutro) (3x10-12 reps)",
                    "Remo con mancuerna (un brazo) (3x10 reps por lado)",
                    "Pullovers con mancuerna (3x15 reps)",
                    "Remo con barra T (agarre cerrado) (4x10 reps)",
                    "Encogimientos con mancuernas (trapecio) (3x12 reps)",
                    "Remo en polea baja (agarre recto) (4x12 reps)"
                };
            }
            else if (SelectedRoutine == "Pierna")
            {
                DetailedRoutines = new ObservableCollection<string>
                {
                    "Peso muerto convencional o sumo (4x6 reps)",
                    "Prensa de piernas (peso pesado) (4x8-10 reps)",
                    "Zancadas con barra o mancuernas (4x12 reps por pierna)",
                    "Elevaciones de talones con peso (4x20 reps)",
                    "Hip thrust con barra (peso moderado) (4x12-15 reps)",
                    "Curl de pierna acostado (m�quina) (3x15 reps)",
                    "Elevaciones de pantorrillas (con peso adicional) (4x20 reps)",
                    "Sentadilla b�lgara con mancuernas (3x10-12 reps por pierna)"
                };
            }
        }

        // Mostrar un modal con el detalle de la rutina
        private async void ShowRoutineDetailModal()
        {
            // Mostrar el modal con un bot�n OK
            await DisplayAlert("Rutina seleccionada", $"Has seleccionado: {SelectedDetailedRoutine}", "OK");
        }

        // Evento para la selecci�n de la fecha en el DatePicker
        private async void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            // Sucursal de ejemplo
            string branch = "\nPuerto de Mazatl�n 4602, M�xico, 85190 Cd. Obreg�n, Son.";

            // Obtener la fecha y hora
            string formattedDate = e.NewDate.ToString("yyyy-MMDDTHH:mm:ss") + "S";

            // Modal con la fecha seleccionada y la sucursal
            await DisplayAlert("Actividad:",
                $"Fecha de entrada:\n {e.NewDate.ToString("D")}\nSucursal: {branch}",
                "OK");
        }
    }
}
