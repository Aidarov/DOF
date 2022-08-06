using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF.WebService.Database.Models
{
    [Table("tablet_user")]
    public class TabletUser
    {
        /// <summary>
        /// ID - Первичный ключ
        /// </summary>
        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Соль
        /// </summary>
        [Column("salt")]
        public string Salt { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Column("password")]
        public string Password { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Column("middle_name")]
        public string MiddleName { get; set; }
    }
}
