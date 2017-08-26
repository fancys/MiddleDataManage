using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.ValidEntity
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/6/13 11:16:37
    *	验证model层实体数据
	===============================================================================================================================*/
    public class ValidationHelper
    {
        public static EntityValidationResult ValidateEntity<T>(T entity)
        where T : class
        {
            return new EntityValidator<T>().Validate(entity);
        }
    }

    public class EntityValidator<T> where T : class
    {
        public EntityValidationResult Validate(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var vc = new ValidationContext(entity, null, null);
            var isValid = Validator.TryValidateObject
                    (entity, vc, validationResults, true);

            return new EntityValidationResult(validationResults);
        }
    }

    public class EntityValidationResult
    {
        public IList<ValidationResult> Errors { get; private set; }
        public bool HasError
        {
            get { return Errors.Count > 0; }
        }

        public EntityValidationResult(IList<ValidationResult> errors = null)
        {
            Errors = errors ?? new List<ValidationResult>();
        }
    }
}
