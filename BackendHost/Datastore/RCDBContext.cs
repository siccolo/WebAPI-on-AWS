using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using Microsoft.EntityFrameworkCore;

using DataModels;

namespace Datastore
{
    public sealed class RCDBContext: DbContext
    {
            public DbSet<RedeemCodeDbEntry> RedeemCodeDbEntry { get; set; }
            public DbSet<RedeemLogDbEntry> RedeemLogDbEntry { get; set; }

            public DbSet<RedeemCodeRedeemResultDbEntry> RedeemCodeRedeemResultDbEntry { get; set; }

            public DbQuery<vwRedeemCode> vwRedeemCode { get;set;}
            
            public RCDBContext(DbContextOptions<RCDBContext> options) : base(options) 
            {
            }

            protected override void OnModelCreating(ModelBuilder mb)
            {
                //-- contains a list of all codes
                mb.Entity<RedeemCodeDbEntry>().ToTable("RedeemCodeDbEntry");
                //current status lookup is based on the view:
                mb.Query<vwRedeemCode>().ToView("vwRedeemCode");

                //-- log lookup attempt
                mb.Entity<RedeemLogDbEntry>().ToTable("RedeemLogDbEntry");

                //--log redeem attempt
                mb.Entity<RedeemCodeRedeemResultDbEntry>().ToTable("RedeemCodeRedeemResultDbEntry");

            }
    }
}

