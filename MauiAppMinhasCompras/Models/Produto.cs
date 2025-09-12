using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MauiAppMinhasCompras.Models
{
    public class Produto : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private string _descricao;
        private double _quantidade;
        private double _preco;
        private string _categoria;

        // Mensagens de erro para bindar no XAML
        public string ErroDescricao { get; private set; }
        public string ErroQuantidade { get; private set; }
        public string ErroPreco { get; private set; }
        public string ErroCategoria { get; private set; }

        // Dicionário de erros para INotifyDataErrorInfo
        private readonly Dictionary<string, List<string>> _erros = new();

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set
            {
                if (_descricao != value)
                {
                    _descricao = value;
                    OnPropertyChanged(nameof(Descricao));
                    ValidarDescricao();
                }
            }
        }

        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (_quantidade != value)
                {
                    _quantidade = value;
                    OnPropertyChanged(nameof(Quantidade));
                    ValidarQuantidade();
                }
            }
        }

        public double Preco
        {
            get => _preco;
            set
            {
                if (_preco != value)
                {
                    _preco = value;
                    OnPropertyChanged(nameof(Preco));
                    ValidarPreco();
                }
            }
        }

        public string Categoria
        {
            get => _categoria;
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    OnPropertyChanged(nameof(Categoria));
                    ValidarCategoria();
                }
            }
        }

        public double Total => Quantidade * Preco;

        // 🔹 Valida todos os campos de uma vez
        public void ValidateAll()
        {
            ValidarDescricao();
            ValidarQuantidade();
            ValidarPreco();
            ValidarCategoria();
        }

        #region Validações
        private void ValidarDescricao()
        {
            if (string.IsNullOrWhiteSpace(Descricao))
            {
                ErroDescricao = "Por favor, preencha a descrição!";
                AddError(nameof(Descricao), ErroDescricao);
            }
            else
            {
                ErroDescricao = string.Empty;
                ClearErrors(nameof(Descricao));
            }
            OnPropertyChanged(nameof(ErroDescricao));
        }

        private void ValidarQuantidade()
        {
            if (Quantidade <= 0)
            {
                ErroQuantidade = "A quantidade deve ser maior que zero!";
                AddError(nameof(Quantidade), ErroQuantidade);
            }
            else
            {
                ErroQuantidade = string.Empty;
                ClearErrors(nameof(Quantidade));
            }
            OnPropertyChanged(nameof(ErroQuantidade));
        }

        private void ValidarPreco()
        {
            if (Preco <= 0)
            {
                ErroPreco = "O preço deve ser maior que zero!";
                AddError(nameof(Preco), ErroPreco);
            }
            else
            {
                ErroPreco = string.Empty;
                ClearErrors(nameof(Preco));
            }
            OnPropertyChanged(nameof(ErroPreco));
        }

        private void ValidarCategoria()
        {
            if (string.IsNullOrWhiteSpace(Categoria))
            {
                ErroCategoria = "Por favor, preencha a categoria!";
                AddError(nameof(Categoria), ErroCategoria);
            }
            else
            {
                ErroCategoria = string.Empty;
                ClearErrors(nameof(Categoria));
            }
            OnPropertyChanged(nameof(ErroCategoria));
        }
        #endregion

        #region INotifyDataErrorInfo
        public bool HasErrors => _erros.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return null;

            return _erros.ContainsKey(propertyName) ? _erros[propertyName] : null;
        }

        private void AddError(string propertyName, string error)
        {
            if (!_erros.ContainsKey(propertyName))
                _erros[propertyName] = new List<string>();

            if (!_erros[propertyName].Contains(error))
            {
                _erros[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_erros.ContainsKey(propertyName))
            {
                _erros.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}