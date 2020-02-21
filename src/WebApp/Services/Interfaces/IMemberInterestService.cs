using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Dtos;

namespace WebApp.Services.Interfaces
{
    public interface IMemberInterestService
    {
        Task<MemberInterestDto> GetById(string id);
        //Task<MemberInterestDto> SaveMemberInterestAsync(MemberInterestDto interest);
    }
}
