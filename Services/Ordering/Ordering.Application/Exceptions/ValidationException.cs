using FluentValidation.Results;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException :ApplicationException
    {
        public ValidationException()
        :base("One Or more Validation failures have occrred.")
        {
            Errors = new Dictionary<string, string[]>();
        }
          public ValidationException(IEnumerable<ValidationFailure> validationFailures)
        :this()
        {
            Errors = validationFailures.GroupBy(e=>e.PropertyName , e=>e.ErrorMessage)
                .ToDictionary(f=> f.Key, f=>f.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }

    }
}
