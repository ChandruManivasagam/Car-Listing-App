using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CarListApp.Maui.Views;
using System.Text.Json;
using SQLite;


namespace CarListApp.Maui.ViewModels
{
    public partial class CarViewModel:BaseViewModel
    {
        //private readonly CarService carService;
        public ObservableCollection<Car> Car { get; private set; } = new();

        public CarViewModel()
        {
            Title = "Car List";
            GetCarList().Wait();
           // this.carService = carService;
        }

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;


        //private bool IsLoading;
        //private readonly string Title;

        [RelayCommand]
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading=true;
                if(Car.Any()) Car.Clear();
                var Cars = App.CarService.GetCars();
                foreach (var car in Car) Car.Add(car);

                //string fileName = "carlist.json";
                //var SerializedList=JsonSerializer.Serialize(Car);
                //File.WriteAllText(fileName, SerializedList);    

                //var rawText=File.ReadAllText(fileName);
                //var carsFromText=JsonSerializer.Deserialize<List<Car>>(rawText);

                //string path = FileSystem.AppDataDirectory;

                //string folder=Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
             
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"unable to get cars:{ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrieved list of cars", "ok");
            }
            finally
            {
                 IsLoading= false;
                 IsRefreshing = false;
            }
        }
        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
           
        }

        [RelayCommand]
        async Task AddCar()
        {
            if(string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model)|| string.IsNullOrEmpty(Vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "ok");
                return;
            }
            var car = new Car
            {
                Make = Make,
            Model = Model,
            Vin = Vin,

            };

            App.CarService.AddCar(car);
            await Shell.Current.DisplayAlert("Info", App.CarService.StatusMessage, "ok");
            await GetCarList();


        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if(id==0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "ok");
                return;
            }
            var result = App.CarService.DeleteCar(id);
            if (result == 0) await Shell.Current.DisplayAlert("Failed", "Please insert valid data", "ok");
            else
            {
                await Shell.Current.DisplayAlert("Deletion successful", "Record Removed successfully", "ok");
                await GetCarList();
            }
        }

        //[RelayCommand]
        //async Task UpdateCar(int id)
        //{
        //    return;
        //}

    }
}
