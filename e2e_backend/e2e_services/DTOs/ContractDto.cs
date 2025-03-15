using e2e_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace e2e_services.DTOs
{
    public class CreateContractDto
    {
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime DateOfEmployment { get; set; }
        public string ContractType { get; set; }
        public int DurationOfContract { get; set; } 
        public string Department { get; set; }
    }
    public class ContractDto : CreateContractDto
    {
        public int Id { get; set; }
    }
    public class UpdateContractDto
    {
        public DateTime? DateOfEmployment { get; set; }
        public string? ContractType { get; set; }
        public int? DurationOfContract { get; set; }
        public string? Department { get; set; }
    }
}
