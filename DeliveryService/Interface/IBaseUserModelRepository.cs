using DeliveryService.DTOs.UserDTOs;
using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Interface
{
    public interface IBaseUserModelRepository
    {
        List<UserDTO> GetAll();
        UserDTO GetById(Guid id);
        UserDTO Add(UserCreateDTO userModel);
        UserConfirmDTO Update(Guid id, UserDTO userModel);
        void Delete(Guid id);
    }
}
