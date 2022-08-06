using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Services.Measure
{
    public class MeasureCommonData
    {
        public decimal? TrubDav { get; set; }

        public decimal? TrubTemp { get; set; }

        public decimal? ZatrubDav { get; set; }

        public decimal? ZatrubTemp { get; set; }

        public decimal? TempPosShtut { get; set; }

        public decimal? DavPosShtut { get; set; }

        public decimal? DavNaMkp { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime AcceptedAt { get; set; }

        public int CreatedBy { get; set; }

        public int DeviceId { get; set; }

        public int OilWellId { get; set; }
    }
}
