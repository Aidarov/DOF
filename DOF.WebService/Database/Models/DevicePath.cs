using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF.WebService.Database.Models
{
    [Table("device_paths")]
    public class DevicePath
    {
        /// <summary>
        /// ID - Первичный ключ
        /// </summary>
        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Column("longitude")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Column("latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Время превывания
        /// </summary>
        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Устройство
        /// </summary>
        [Column("device_id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// Принято когда
        /// </summary>
        [Column("accepted_at")]
        public DateTime AcceptedAt { get; set; }

        /// <summary>
        /// Уникальный идентификатор со стороны клиента
        /// </summary>
        [Column("uid")]
        public string Uid { get; set; }
    }
}
