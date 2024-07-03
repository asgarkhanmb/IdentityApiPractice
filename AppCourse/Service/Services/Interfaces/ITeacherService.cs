using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task Create(TeacherCreateDto model );
        Task<IEnumerable<TeacherDto>> GetAll();
        Task EditAsync(int? id, TeacherEditDto model);
        Task DeleteAsync(int? id);
        Task<TeacherDto> GetById(int id);
        Task<IEnumerable<TeacherDto>> SearchAsync(string name);
    }
}
