﻿using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Model
{
    public class UpdateUserRoleRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
