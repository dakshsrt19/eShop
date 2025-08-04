using RKSoft.eShop.App.DTOs;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public async Task<EStore> CreateStoreAsync(EStore dbRecord)
        {
            return await _storeRepository.CreateAsync(dbRecord);
        }

        public async Task<bool> DeleteStoreAsync(EStore dbRecord)
        {
            return await _storeRepository.DeleteAsync(dbRecord);
        }

        public async Task<List<EStore>> GetAllStoreAsync()
        {
            return await _storeRepository.GetAllAsync();
        }
       
        
        public async Task<EStore> GetStoreByIdAsync(Expression<Func<EStore, bool>> filter, bool useNoTracking = false)
        {
            return await _storeRepository.GetByIdAsync(filter, useNoTracking);
        }

        public async Task<EStore> GetStoreByNameAsync(Expression<Func<EStore, bool>> filter)
        {
            return await _storeRepository.GetByNameAsync(filter);
        }
        public async Task<EStore> UpdateStoreAsync(EStore dbRecord)
        {
           return await _storeRepository.UpdateAsync(dbRecord);
        }
    }
}
