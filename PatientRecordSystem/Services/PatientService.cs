using AutoMapper;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Domain.Repositories;
using PatientRecordSystem.Domain.Services;
using PatientRecordSystem.Domain.Services.Communication;
using PatientRecordSystem.Extensions;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PatientRecordSystem.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IMapper mapper, IPatientRepository patientRepository, IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PatientResponse> GetById(int id)
        {
            var patient = await _patientRepository.GetById(id);
            if (patient == null)
            {
                return new PatientResponse("patient not found.");
            }
            return new PatientResponse(patient);
        }

        public async Task<PagedList<PatientResource>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<PatientResource, object>>> columnsMap)
        {
            return await _patientRepository.ListAsync(queryString, columnsMap);
        }

        public async Task<PatientResponse> SaveAsync(Patient patient)
        {
            try
            {
                await _patientRepository.Create(patient);
                await _unitOfWork.CompleteAsync();
                return new PatientResponse(patient);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PatientResponse($"An error occurred when saving the Patient: {ex.GetAllMessages()}");
            }
        }

        public async Task<PatientResponse> UpdateAsync(int id, SavePatientResource savePatient)
        {
            var existingPatient = await _patientRepository.GetById(id);

            if (existingPatient == null)
                return new PatientResponse("Patient not found.");
            _mapper.Map<SavePatientResource, Patient>(savePatient, existingPatient);
            try
            {
                _patientRepository.Update(existingPatient);
                await _unitOfWork.CompleteAsync();

                return new PatientResponse(existingPatient);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new PatientResponse($"An error occurred when updating the patient: {ex.GetAllMessages()}");
            }
        }

        public async Task<bool> IsExist(int Oid, int? id)
        {
            return await _patientRepository.IsExist(Oid, id);
        }

        public async Task<PatientReportResource> GetPatientReport(int id)
        {
            return await _patientRepository.GetPatientReport(id);
        }
    }
}