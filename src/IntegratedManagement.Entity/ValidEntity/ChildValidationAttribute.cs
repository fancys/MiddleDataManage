using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedManagement.Entity.ValidEntity
{
    /*===============================================================================================================================
	*	Create by Fancy at 2017/7/13 9:26:20
	===============================================================================================================================*/
    public class ChildValidationAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
