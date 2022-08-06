using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DOF.WebService.Database.Models
{
    [Table("oil_wells")]
    public class OilWell
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
        /// Короткое название
        /// </summary>

        [Column("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Кодовое название
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

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
        /// Ссылка на идентификатор Oil Field
        /// </summary>
        [Column("oil_field_id")]
        public int OilFieldId { get; set; }

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
        /// Удалено
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
    }
}
