using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;


namespace EmployeeProject.Helper
{
    public class ModelStateHelper
    {
        public static string GetErrors(List<ModelError> errors)
        {
            var errorMessage = "";
            foreach (var error in errors)
            {
                if (errorMessage != "")
                    errorMessage += ", ";

                errorMessage += error.ErrorMessage;
            }
            return errorMessage;
        }
    }
}
