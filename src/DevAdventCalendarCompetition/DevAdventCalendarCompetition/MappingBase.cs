using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevAdventCalendarCompetition
{
    public abstract class MappingBase<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : ModelBase
    {
        protected MappingBase()
        {
            //TODO fix this
            //HasKey(e => e.Id);
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //TODO fix or remove
            //throw new NotImplementedException();
        }
    }
}