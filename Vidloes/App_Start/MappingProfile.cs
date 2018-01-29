﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidloes.Dtos;
using AutoMapper;
using Vidloes.Models;
using Vidloes.ViewModels;



namespace Vidloes.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();
        }
    }
}