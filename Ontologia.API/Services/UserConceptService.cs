﻿using Ontologia.API.Domain.Models;
using Ontologia.API.Domain.Persistence.Repositories;
using Ontologia.API.Domain.Services;
using Ontologia.API.Domain.Services.Communications;

namespace Ontologia.API.Services
{
    public class UserConceptService : IUserConceptService
    {
        public readonly IUserConceptRepository _userConceptRepository;
        public readonly IUnitOfWork _unitOfWork;

        public UserConceptService(IUserConceptRepository userConceptRepository, IUnitOfWork unitOfWork)
        {
            _userConceptRepository = userConceptRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserConceptResponse> AssignUserConcept(int userId, int userConceptId)
        {
            try
            {
                await _userConceptRepository.AssingUserConcept(userId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while assigning userConcept to user: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> Delete(int userConceptId)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null)
                return new UserConceptResponse("UserConcept not found");
            try
            {
                _userConceptRepository.Remove(existingUserConcept);
                await _unitOfWork.CompleteAsync();
                return new UserConceptResponse(existingUserConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while deleting userConcept: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> GetById(int userConceptId)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null)
                return new UserConceptResponse("UserConcept not found");
            return new UserConceptResponse(existingUserConcept);
        }

        public async Task<IEnumerable<UserConcept>> ListAsync()
        {
            return await _userConceptRepository.ListAsync();
        }

        public async Task<IEnumerable<UserConcept>> ListByUserId(int userId)
        {
            return await _userConceptRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserConceptResponse> SaveAsync(UserConcept userConcept)
        {
            try
            {
                await _userConceptRepository.AddAsync(userConcept);
                await _unitOfWork.CompleteAsync();
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error while saving userConcept:{ex.Message}");
            }
        }

        public async Task<UserConceptResponse> UnassignUserConcept(int userId, int userConceptId)
        {
            try
            {
                await _userConceptRepository.UnassingUserConcept(userId, userConceptId);
                await _unitOfWork.CompleteAsync();
                UserConcept userConcept = await _userConceptRepository.GetById(userConceptId);
                return new UserConceptResponse(userConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error ocurrend while unassigning userConcept to user: {ex.Message}");
            }
        }

        public async Task<UserConceptResponse> Update(int userConceptId, UserConcept userConcept)
        {
            var existingUserConcept = await _userConceptRepository.GetById(userConceptId);
            if (existingUserConcept == null)
                return new UserConceptResponse("UserConcept not found");

            existingUserConcept.Title = userConcept.Title;
            existingUserConcept.Description = userConcept.Description;
            existingUserConcept.Url = userConcept.Url;
            existingUserConcept.IsActive = userConcept.IsActive;
            existingUserConcept.CreatedOn = userConcept.CreatedOn;
            existingUserConcept.ModifiedOn = userConcept.ModifiedOn;

            try
            {
                _userConceptRepository.Update(existingUserConcept);
                await _unitOfWork.CompleteAsync();
                return new UserConceptResponse(existingUserConcept);
            }
            catch (Exception ex)
            {
                return new UserConceptResponse($"An error while updating userConcept: {ex.Message}");
            }
        }
    }
}
