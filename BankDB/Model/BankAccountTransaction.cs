using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankDB.Model
{
    public partial class BankAccountTransaction
    {
        public int Id { get; set; }
        [Required]
        [Column("IBAN")]
        [StringLength(50)]
        public string Iban { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime TimeStamp { get; set; }

        [ForeignKey("Iban")]
        [InverseProperty("BankAccountTransaction")]
        public BankAccount IbanNavigation { get; set; }
    }
}
