using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Database.Models
{
    [Table("users")]
    public class User
    {
        /// <summary>
        /// ID - Первичный ключ
        /// </summary>
        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Login - 
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Password - 
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// Salt - 
        /// </summary>
        [Column("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("created_by")]
        public int? CreatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_blocked")]
        public bool IsBlocked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("role_id")]
        public Role Role { get; set; }

    }

    public enum Role : int
    {
        Admin = 1,
        Moderator = 2,
        TabletUser = 3
    }
}
