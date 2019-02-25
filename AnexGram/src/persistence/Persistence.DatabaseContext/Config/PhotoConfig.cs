using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.DatabaseContext.Config
{
    public class PhotoConfig
    {
        public PhotoConfig(EntityTypeBuilder<Photo> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Url).IsRequired().HasMaxLength(100);

        }
    }
}
