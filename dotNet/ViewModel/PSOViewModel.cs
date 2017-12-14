using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using dotNet.Helpers;
using dotNet.Model;
using dotNet.Service;
using LiveCharts;
using LiveCharts.Defaults;
using MvvmHelpers;

namespace dotNet.ViewModel
{
    // ReSharper disable once InconsistentNaming
    public class PSOViewModel : BaseViewModel
    {
        private int _variablesNumber = 10;
        public int VariablesNumber
        {
            get => _variablesNumber;
            set => SetProperty(ref _variablesNumber, value);
        }

        private double _variablesMinimum = -5;
        public double VariablesMinimum
        {
            get => _variablesMinimum;
            set => SetProperty(ref _variablesMinimum, value);
        }

        private double _variablesMaximum = 5;
        public double VariablesMaximum
        {
            get => _variablesMaximum;
            set => SetProperty(ref _variablesMaximum, value);
        }
        
        private int _maxIterations = 200;
        public int MaxIterations
        {
            get => _maxIterations;
            set => SetProperty(ref _maxIterations, value);
        }
        
        private int _polulationSize = 50;
        public int PopulationSize
        {
            get => _polulationSize;
            set => SetProperty(ref _polulationSize, value);
        }

        private double _personalCoefficient = 1.5;
        public double PersonalCoefficient
        {
            get => _personalCoefficient;
            set => SetProperty(ref _personalCoefficient, value);
        }

        private double _globalCoefficient = 2;
        public double GlobalCoefficient
        {
            get => _globalCoefficient;
            set => SetProperty(ref _globalCoefficient, value);
        }

        private double _inertiaWeight = 1;
        public double InertiaWeight
        {
            get => _inertiaWeight;
            set => SetProperty(ref _inertiaWeight, value);
        }

        private double _inertiaWeightDamp = 0.995;
        public double InertiaWeightDamp
        {
            get => _inertiaWeightDamp;
            set => SetProperty(ref _inertiaWeightDamp, value);
        }

        private readonly Dictionary<string, Func<List<double>, double>> _functions = new Dictionary<string, Func<List<double>, double>>
        {
            ["Сферическая функция"] = Model.Functions.SphereFunction,
            ["Функция Гриванка"] = Model.Functions.GriewankFunction,
            ["Функция Растригина"] = Model.Functions.RastriginFunction,
            ["Функция Розенброка"] = Model.Functions.RosenbrockFunction
        };

        private string _selectedFunction = "Сферическая функция";
        public string SelectedFunction
        {
            get => _selectedFunction;
            set => SetProperty(ref _selectedFunction, value);
        }

        public List<string> Functions => _functions.Keys.ToList();

        private ObservableCollection<string> _iterations = new ObservableCollection<string>();

        public ObservableCollection<string> Iterations
        {
            get => _iterations;
            set => SetProperty(ref _iterations, value);
        }

        private ChartValues<ObservableValue> _values = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> Values
        {
            get => _values;
            set => SetProperty(ref _values, value);
        }

        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime;
            set => SetProperty(ref _currentTime, value);
        }

        private string _minimumCost; 
        public string MinimumCost
        {
            get => _minimumCost;
            set => SetProperty(ref _minimumCost, value);
        }

        private ICommand _performSwarmOptimizationCommand; 
        public ICommand PerformSwarmOptimizationCommand => _performSwarmOptimizationCommand?? (_performSwarmOptimizationCommand = new CommandHandler(PerformSwarmOptimizationAsync, IsNotBusy));

        private readonly PSOSolver _problemSolver = new PSOSolver();
        private async void PerformSwarmOptimizationAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var start = DateTime.Now;

            var problem = new Problem
            {
                CostFunction = _functions[_selectedFunction],
                VariablesNumber = VariablesNumber,
                VariablesMinimum = VariablesMinimum,
                VariablesMaximum = VariablesMaximum
            };
            var problemConfiguration = new ProblemConfig
            {
                Problem = problem,
                MaxIterations = MaxIterations,
                PopulationSize = PopulationSize,
                PersonalCoefficient = PersonalCoefficient,
                GlobalCoefficient = GlobalCoefficient,
                InertiaWeight = InertiaWeight,
                InertiaWeightDamp = InertiaWeightDamp
            };
            
            _problemSolver.Initialize(problemConfiguration);

            Values.Clear();

            Iterations.Clear();

            for (int i = 0; i < MaxIterations; ++i)
            {
                var d = await _problemSolver.PerformIteraration();
                Values.Add(new ObservableValue(d));
                Iterations.Insert(0, $"Итерация №{i}\nМинимальное значение = {d}");
                //Iterations.Add($@"Iteration {i}: Best cost = {d}");
                CurrentTime = (DateTime.Now - start).TotalSeconds.ToString(CultureInfo.CurrentCulture);
                MinimumCost = d.ToString(CultureInfo.CurrentCulture);
            }
            
            IsBusy = false;
        }



    }
}