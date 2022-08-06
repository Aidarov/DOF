using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF.WebService.Database.Models
{
    [Table("measures")]
    public class Measure
    {
        /// <summary>
        /// Id - Первичный ключ
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Трубное давление
        /// </summary>
        [Column("trub_dav")]
        public decimal? TrubDav { get; set; }

        /// <summary>
        /// Трубная температура
        /// </summary>
        [Column("trub_temp")]
        public decimal? TrubTemp { get; set; }

        /// <summary>
        /// Затрубное давление
        /// </summary>
        [Column("zatrub_dav")]
        public decimal? ZatrubDav { get; set; }

        /// <summary>
        /// Затрубная температура
        /// </summary>
        [Column("zatrub_temp")]
        public decimal? ZatrubTemp { get; set; }

        /// <summary>
        /// Температура после штуцера
        /// </summary>
        [Column("temp_pos_shtut")]
        public decimal? TempPosShtut { get; set; }

        /// <summary>
        /// Давление после штуцера
        /// </summary>
        [Column("dav_pos_shtut")]
        public decimal? DavPosShtut { get; set; }

        /// <summary>
        /// Давление на МКП
        /// </summary>
        [Column("dav_na_mkp")]
        public decimal? DavNaMkp { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Column("latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Column("longitude")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// Создано когда
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Принято когда
        /// </summary>
        [Column("created_at")]
        public DateTime AcceptedAt { get; set; }

        /// <summary>
        /// Создано кем
        /// </summary>
        [Column("created_by")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// Устройство
        /// </summary>
        [Column("device_id")]
        public int DeviceId { get; set; }

        /// <summary>
        /// Скважина
        /// </summary>
        [Column("oil_well_id")]
        public int OilWellId { get; set; }
    }
}
