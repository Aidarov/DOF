using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Database.Models
{
    [Table("user_sessions")]
    public class UserSession
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("token")]
        public Guid Token { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [Column("ip_address")]
        public string IpAddress { get; set; }

        [Column("is_blocked")]
        public bool IsBlocked { get; set; }
    }
}
