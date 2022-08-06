using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF.WebService.Database.Models
{
    [Table("devices")]
    public class Device
    {
        /// <summary>
        /// ID - Первичный ключ
        /// </summary>
        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Модель
        /// </summary>
        [Column("model")]
        public string Model { get; set; }

        /// <summary>
        /// Серийный номер
        /// </summary>
        [Column("serial_number")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Фотография
        /// </summary>
        [Column("photo_path")]
        public string PhotoPath { get; set; }

        /// <summary>
        /// Токен
        /// </summary>
        [Column("token")]
        public Guid Token { get; set; }

        /// <summary>
        /// Создано кем
        /// </summary>
        [Column("created_by")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Создано когда
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Удалено
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Удалено кем
        /// </summary>
        [Column("deleted_by")]
        public int? DeletedBy { get; set; }

        /// <summary>
        /// Удалено когда
        /// </summary>
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Клиентская версия приложения
        /// </summary>
        [Column("client_app_version")]
        public string ClientAppVersion { get; set; }

        /// <summary>
        /// Активировано
        /// </summary>
        [Column("activated")]
        public bool? Activated {get; set;}

        /// <summary>
        /// Активировано когда
        /// </summary>
        [Column("activated_at")]
        public DateTime? ActivatedAt { get; set; }
    }
}
