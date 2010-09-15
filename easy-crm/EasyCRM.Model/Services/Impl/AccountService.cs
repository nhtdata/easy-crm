﻿using System.Collections.Generic;
using EasyCRM.Model.Domains;
using EasyCRM.Model.Repositories;
using EasyCRM.Model.Repositories.Entity;

namespace EasyCRM.Model.Services.Impl
{
    public class AccountService:IAccountService
    {
        private IValidationDictionary _validationDictionary;
        private IAccountRepository _repository;


        public AccountService(IValidationDictionary validationDictionary) 
            : this(validationDictionary, new EntityAccountRepository())
        {}


        public AccountService(IValidationDictionary validationDictionary, IAccountRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }


        public bool ValidateAccount(Account accountToValidate)
        {
            if (accountToValidate.Name.Trim().Length == 0)
                _validationDictionary.AddError("Name", "Name is required.");
            if (accountToValidate.Address.Trim().Length == 0)
                _validationDictionary.AddError("Address", "Address is required.");
            if (accountToValidate.Description.Trim().Length == 0)
                _validationDictionary.AddError("Description", "Description is required."); 
            return _validationDictionary.IsValid;
        }


        #region IAccountService Members

        public bool CreateAccount(Account accountToCreate)
        {
            // Validation logic
            if (!ValidateAccount(accountToCreate))
                return false;

            // Database logic
            try
            {
                _repository.Create(accountToCreate);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool EditAccount(Account accountToEdit)
        {
            // Validation logic
            if (!ValidateAccount(accountToEdit))
                return false;

            // Database logic
            try
            {
                _repository.Update(accountToEdit);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool DeleteAccount(Account accountToDelete)
        {
            try
            {
                _repository.Delete(accountToDelete);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public Account GetAccount(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<Account> ListAccounts()
        {
            return _repository.ListAll();
        }

        #endregion

    }
}