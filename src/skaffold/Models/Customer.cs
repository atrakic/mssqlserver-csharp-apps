using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace skaffold.Models;

[Keyless]
[Table("customers")]
[Index("CustomerId", Name = "idx_customer_id")]
public partial class Customer
{
    [Column("tabkey")]
    public int? Tabkey { get; set; }

    [Column("customer_id")]
    [StringLength(10)]
    public string? CustomerId { get; set; }

    [Column("customer_information")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? CustomerInformation { get; set; }
}
