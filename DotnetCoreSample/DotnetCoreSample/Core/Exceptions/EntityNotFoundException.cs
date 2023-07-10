using System;

namespace DotnetCoreSample.Core.Exceptions
{
    public class EntityNotFoundException : BusinessException
    {
        public EntityNotFoundException(Guid id) : base($"Entity with ID: {id} not found")
        {

        }
    }
}
