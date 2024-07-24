using Notebook.Data;
using Notebook.Data.Interfaces;
using NotebookContext.Model;
using NotebookDesktop.Command.Base;
using OnlineShop_CL.Command;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotebookDesktop.ViewModel
{
    internal class MainVM : Base.ViewModel
    {
        readonly IContactData _contactData;

        IEnumerable<Contact> _contactDataTable;
        public IEnumerable<Contact> ContactDataTable
        {
            get => _contactDataTable;
            set => Set(ref _contactDataTable, value);
        }

        Contact? _contact;
        public Contact Contact
        {
            get => _contact;
            set
            {
                Set(ref _contact, value);
                if (_contact != null)
                {
                    FirstNameField = _contact.FirstName;
                    LastNameField = _contact.LastName;
                    MiddleNameField = _contact.MiddleName;
                    PhoneField = _contact.Phone;
                    AddressField = _contact.Address;
                    DescriptionField = _contact.Description;
                }
                else
                {
                    RefreshAll();
                }

            }
        }

        #region ContactFields
        string _lastNameField = string.Empty;
        public string LastNameField
        {
            get => _lastNameField;
            set => Set(ref _lastNameField, value);
        }

        string _firstNameField = string.Empty;
        public string FirstNameField
        {
            get => _firstNameField;
            set => Set(ref _firstNameField, value);
        }

        string _middleNameField = string.Empty;
        public string MiddleNameField
        {
            get => _middleNameField;
            set => Set(ref _middleNameField, value);
        }

        string _phoneField = string.Empty;
        public string PhoneField
        {
            get => _phoneField;
            set => Set(ref _phoneField, value);
        }

        string _addressField = string.Empty;
        public string AddressField
        {
            get => _addressField;
            set => Set(ref _addressField, value);
        }

        string _descriptionField = string.Empty;
        public string DescriptionField
        {
            get => _descriptionField;
            set => Set(ref _descriptionField, value);
        } 
        #endregion

        public MainVM()
        {
            _contactData = new ContactApi();
            ContactDataTable = _contactData.GetContactsAsync().Result;
            Create = new LambdaCommandAsync(OnCreateExectutedAsync, CanCreateExecute);
            Update = new LambdaCommandAsync(OnUpdateExecutedAsync, CanUpdateExecute);
            Delete = new LambdaCommandAsync(OnDeleteExecutedAsync, CanDeleteExecute);
            Refresh = new LambdaCommandAsync(OnRefreshExecutedAsync, CanRefreshExecute);
        }

        #region Commands

        #region Create
        public IAsyncCommand Create { get; }
        public async Task OnCreateExectutedAsync(object p)
        {
            try
            {
                _contact = new();
                AssignContactFields();
                await _contactData.CreateContactAsync(_contact);
                _contact = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool CanCreateExecute(object p)
        {
            if (_firstNameField != string.Empty &&
                _lastNameField != string.Empty &&
                _middleNameField != string.Empty)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Update
        public IAsyncCommand Update { get; }
        async Task OnUpdateExecutedAsync(object p)
        {
            try
            {
                AssignContactFields();
                await _contactData.UpdateContactAsync(_contact);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanUpdateExecute(object p)
        {
            return _contact != null;
        }

        #endregion

        #region Delete
        public IAsyncCommand Delete { get; }
        async Task OnDeleteExecutedAsync(object p)
        {
            try
            {
                await _contactData.DeleteContactAsync(_contact.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanDeleteExecute(object p)
        {
            return _contact != null;
        }
        #endregion

        #region Refresh
        public IAsyncCommand Refresh { get; }
        async Task OnRefreshExecutedAsync(object p)
        {
            ContactDataTable = await _contactData.GetContactsAsync();
            RefreshAll();
        }
        bool CanRefreshExecute(object p) => true; 
        #endregion

        #endregion

        void RefreshAll()
        {
            FirstNameField =
                        LastNameField =
                        MiddleNameField =
                        PhoneField =
                        AddressField =
                        DescriptionField = string.Empty;
        }

        void AssignContactFields()
        {
            _contact.FirstName = _firstNameField;
            _contact.LastName = _lastNameField;
            _contact.MiddleName = _middleNameField;
            _contact.Phone = _phoneField;
            _contact.Address = _addressField;
            _contact.Description = _descriptionField;
        }


    }
}
