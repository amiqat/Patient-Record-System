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
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordService(IRecordRepository recordRepository, IUnitOfWork unitOfWork)
        {
            _recordRepository = recordRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecordResponse> GetById(int id)
        {
            var record = await _recordRepository.GetById(id);
            if (record == null)
            {
                return new RecordResponse("Record not found.");
            }
            return new RecordResponse(record);
        }

        public async Task<PagedList<Record>> ListAsync(QueryStringParameters queryString, Dictionary<string, Expression<Func<Record, object>>> columnsMap)
        {
            return await _recordRepository.ListAsync(queryString, columnsMap);
        }

        public async Task<RecordResponse> SaveAsync(Record record)
        {
            try
            {
                await _recordRepository.Create(record);
                await _unitOfWork.CompleteAsync();
                return new RecordResponse(record);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RecordResponse($"An error occurred when saving the record: {ex.GetAllMessages()}");
            }
        }

        public async Task<RecordResponse> UpdateAsync(int id, Record record)
        {
            var existingRecord = await _recordRepository.IsExist(id);

            if (!existingRecord)
                return new RecordResponse("Record not found.");

            try
            {
                _recordRepository.Update(record);
                await _unitOfWork.CompleteAsync();

                return new RecordResponse(record);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new RecordResponse($"An error occurred when updating the record: {ex.GetAllMessages()}");
            }
        }
    }
}