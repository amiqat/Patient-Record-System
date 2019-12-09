using AutoMapper;
using PatientRecordSystem.Domain.Models;
using PatientRecordSystem.Resources;
using System.Linq;

namespace PatientRecordSystem.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API Resource
            CreateMap<MetaData, MetaDataResource>();
            CreateMap<Record, RecordResource>()
                .ForMember(x => x.PatientName, opt => opt.MapFrom(x => x.Patient.PatientName));
            CreateMap<Record, SaveRecordResource>();
            CreateMap<Patient, SavePatientResource>();

            // API Resource to Domain
            CreateMap<MetaDataResource, MetaData>();

            CreateMap<SaveRecordResource, Record>();

            CreateMap<SavePatientResource, Patient>().ForMember(v => v.MetaData, opt => opt.Ignore())
              .AfterMap((vr, v) =>
              {
                  var removedMetaData = v.MetaData.Where(f => !vr.MetaData.Select(x => x.Key).Contains(f.Key)).ToList();
                  foreach (var f in removedMetaData)
                      v.MetaData.Remove(f);

                  var addedFeatures = vr.MetaData.Where(m => !v.MetaData.Any(f => f.Key == m.Key)).Select(m => new MetaData { Key = m.Key, Value = m.Value, PatientId = vr.Id }).ToList();
                  foreach (var f in addedFeatures)
                      v.MetaData.Add(f);
              });
        }
    }
}