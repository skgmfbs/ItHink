using System;

namespace DotnetCoreSample.Core.Common
{
    public abstract class EntityBase<TKey>
    {
        public TKey Id { get; set; }

        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}
