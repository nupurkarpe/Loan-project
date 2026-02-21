using AutoMapper;
using PaymentService.Application.DTO;
using PaymentService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Mapping
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LoanPayment,CreatePaymentRequest>().ReverseMap();
            CreateMap<LoanPayment, PaymentResponse>().ReverseMap();
        }
    }
}
