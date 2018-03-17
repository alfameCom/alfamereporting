using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public partial class YammerHashtags
    {
        public int Id { get; set; }
        public long MessageId { get; set; }
        public long GroupId { get; set; }
        public long? ThreadId { get; set; }
        public long? SenderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
        public string MessageUrl { get; set; }
        public string Recipient { get; set; }
        public string Comment { get; set; }
        public string Hashtag { get; set; }
        public string BonusValue { get; set; }
        public decimal? BonusAmount { get; set; }
        public bool? BonusAccepted { get; set; }
        public bool? BonusPaid { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Quarter { get; set; }
        public int? Week { get; set; }
    }
}
