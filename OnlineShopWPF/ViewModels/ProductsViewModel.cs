﻿using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.Command;
using OnlineShopWPF.Models;
using OnlineShopWPF.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    internal class ProductsViewModel : ViewModelBase
    {
        private readonly Context _dbContext;
        private readonly StaffStore _employeeStore;

        private readonly ProductModel _productsM = new();
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
        private Category _selectedCategory;
        private ObservableCollection<Product> _allProducts;
        public ObservableCollection<Product> ProductsDataList { get; set; } = new ObservableCollection<Product>();

        private bool _isGuest;

        public bool IsGuest
        {
            get { return _isGuest; }
            set
            {
                _isGuest = value;
                OnPropertyChanged(nameof(IsGuest));
            }
        }

        private bool _isRead;

        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                _isRead = value;
                OnPropertyChanged(nameof(IsRead));
            }
        }

        public ProductsViewModel(Context dbContext, StaffStore employeeStore)
        {
            _dbContext = dbContext;
            _employeeStore = employeeStore;

            ProductsDataList = new ObservableCollection<Product>(_dbContext.Products);

            SaveCommand = new SaveCommand();
            PropertyChanged += ProductsVM_PropertyChanged;
            _allProducts = new ObservableCollection<Product>(_dbContext.Products);

            UpdateFilteredProducts();
            LoadCategories();

            if (_employeeStore.CurrentStaff.RoleID == 1)
            {
                IsGuest = false;
            }
            else
            {
                IsGuest = true;
            }

            if (_employeeStore.CurrentStaff.RoleID == 1)
            {
                IsRead = true;
            }
            else
            {
                IsRead = false;
            }
        }

        private void LoadCategories()
        {
            Categories.Clear();
            var categories = _dbContext.Categories.ToList(); // Убедитесь что категории загружены в список

            foreach (var category in categories)
            {
                Categories.Add(category);
            }

            if (Categories.Any())
                SelectedCategory = Categories.First(); // Установить первую категорию по умолчанию
        }

        private void ProductsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Search))
            {
                UpdateFilteredProducts();
            }
            else if (e.PropertyName == nameof(SelectedCategory))
            {
                UpdateCategoryFilter();
            }
        }

        private void UpdateFilteredProducts()
        {
            ProductsDataList.Clear();

            foreach (var product in _allProducts)
            {
                if (MatchesSearch(product) && (_selectedCategory == null || product.Category == _selectedCategory))
                {
                    ProductsDataList.Add(product);
                }
            }
        }

        private void UpdateCategoryFilter()
        {
            ProductsDataList.Clear();

            foreach (var product in _allProducts)
            {
                if (MatchesSearch(product) && (product.Category == _selectedCategory || _selectedCategory == _dbContext.Categories.FirstOrDefault(c => c.ID == 1)))
                {
                    ProductsDataList.Add(product);
                }
            }
        }

        private bool MatchesSearch(Product product)
        {
            return string.IsNullOrEmpty(Search) || product.Title.Contains(Search, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Search
        {
            get { return _productsM.Search; }
            set { _productsM.Search = value; OnPropertyChanged(nameof(Search)); }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); UpdateCategoryFilter(); }
        }

        public ICommand SaveCommand { get; set; }
    }
}
